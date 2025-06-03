import numpy as np

def sigmoid(x):
    return 1 / (1 + np.exp(-x))

def sigmoid_derivative(x):
    s = sigmoid(x)
    return s * (1 - s)

def relu(x):
    return np.maximum(0, x)

def relu_deriv(x):
    return (x > 0).astype(float)

def binary_cross_entropy(y_true, y_pred):
    eps = 1e-8
    return -np.mean(y_true * np.log(y_pred + eps) + (1 - y_true) * np.log(1 - y_pred + eps))


class ANN:
    def __init__(self, hidden_layers, learning_rate=0.01, max_iter=1000):
        self.hidden_layers = hidden_layers
        self.learning_rate = learning_rate
        self.max_iter = max_iter
        self.weights = []
        self.biases = []

    def _initialize_weights(self, input_size, output_size):
        layer_sizes = [input_size] + list(self.hidden_layers) + [output_size]
        self.weights = []
        self.biases = []
        for i in range(len(layer_sizes) - 1):
            w = np.random.randn(layer_sizes[i], layer_sizes[i + 1]) * np.sqrt(2 / layer_sizes[i])
            b = np.zeros((1, layer_sizes[i + 1]))
            self.weights.append(w)
            self.biases.append(b)

    def fit(self, X, y):
        X = X.astype(np.float32)
        y = y.reshape(-1, 1).astype(np.float32)
        input_size = X.shape[1]
        output_size = 1
        self._initialize_weights(input_size, output_size)

        for iteration in range(self.max_iter):
            activations = [X]
            for i in range(len(self.weights)):
                z = np.dot(activations[-1], self.weights[i]) + self.biases[i]
                a = sigmoid(z)
                activations.append(a)

            y_pred = activations[-1]
            loss = binary_cross_entropy(y, y_pred)
            print(f'Iteration {iteration + 1}, loss: {loss}')

            error = activations[-1] - y
            deltas = [error * sigmoid_derivative(activations[-1])]

            for i in reversed(range(len(self.weights) - 1)):
                delta = np.dot(deltas[-1], self.weights[i + 1].T) * sigmoid_derivative(activations[i + 1])
                deltas.append(delta)

            deltas.reverse()
            for i in range(len(self.weights)):
                self.weights[i] -= self.learning_rate * np.dot(activations[i].T, deltas[i])
                self.biases[i] -= self.learning_rate * np.sum(deltas[i], axis=0, keepdims=True)

    def predict(self, X):
        a = X.astype(np.float32)
        for w, b in zip(self.weights, self.biases):
            z = np.dot(a, w) + b
            a = sigmoid(z)
        return (a > 0.5).astype(int)


class ConvLayer:
    def __init__(self, num_filters, filter_size):
        self.num_filters = num_filters
        self.filter_size = filter_size
        self.filters = None

    def iterate_regions(self, image):
        c, h, w = image.shape
        for i in range(h - self.filter_size + 1):
            for j in range(w - self.filter_size + 1):
                region = image[:, i:i+self.filter_size, j:j+self.filter_size]
                yield i, j, region

    def forward(self, input):
        if input.ndim == 3:
            c, h, w = input.shape
        else:
            raise ValueError("Input to ConvLayer must be a 3D tensor (channels, height, width)")

        if self.filters is None:
            self.filters = np.random.randn(self.num_filters, c, self.filter_size, self.filter_size) / (self.filter_size * self.filter_size)

        self.last_input = input
        output_dim = h - self.filter_size + 1
        output = np.zeros((self.num_filters, output_dim, output_dim))
        for f in range(self.num_filters):
            for i, j, region in self.iterate_regions(input):
                output[f, i, j] = np.sum(region * self.filters[f])
        self.last_output = output
        return output

    def backward(self, d_L_d_out, learning_rate):
        d_L_d_filters = np.zeros_like(self.filters)
        d_L_d_input = np.zeros_like(self.last_input)

        for f in range(self.num_filters):
            for i, j, region in self.iterate_regions(self.last_input):
                d_L_d_filters[f] += d_L_d_out[f, i, j] * region
                d_L_d_input[:, i:i+self.filter_size, j:j+self.filter_size] += d_L_d_out[f, i, j] * self.filters[f]

        self.filters -= learning_rate * d_L_d_filters
        return d_L_d_input


class MaxPoolLayer:
    def __init__(self, size=2):
        self.size = size

    def iterate_regions(self, image):
        h, w = image.shape
        new_h = h // self.size
        new_w = w // self.size
        for i in range(new_h):
            for j in range(new_w):
                region = image[i*self.size:(i+1)*self.size, j*self.size:(j+1)*self.size]
                yield i, j, region

    def forward(self, input):
        self.last_input = input
        d, h, w = input.shape
        output = np.zeros((d, h // self.size, w // self.size))
        self.max_indices = {}

        for i in range(d):
            for j, k, region in self.iterate_regions(input[i]):
                max_val = np.max(region)
                output[i, j, k] = max_val
                max_pos = np.argwhere(region == max_val)[0]
                self.max_indices[(i, j, k)] = (max_pos[0], max_pos[1])
        return output

    def backward(self, d_L_d_out):
        d, h, w = self.last_input.shape
        d_L_d_input = np.zeros_like(self.last_input)
        pool_h = h // self.size
        pool_w = w // self.size

        for i in range(d):
            for j in range(pool_h):
                for k in range(pool_w):
                    r, c = self.max_indices[(i, j, k)]
                    d_L_d_input[i, j * self.size + r, k * self.size + c] = d_L_d_out[i, j, k]
        return d_L_d_input


class FullyConnected:
    def __init__(self, input_len, output_len):
        self.weights = np.random.randn(input_len, output_len) / input_len
        self.biases = np.zeros(output_len)

    def forward(self, input):
        self.last_input = input
        self.last_z = np.dot(input, self.weights) + self.biases
        return sigmoid(self.last_z)

    def backward(self, d_L_d_out, learning_rate):
        grad_z = d_L_d_out * sigmoid_derivative(self.last_z)
        d_L_d_w = np.dot(self.last_input[:, None], grad_z[None])
        d_L_d_input = grad_z @ self.weights.T

        self.weights -= learning_rate * d_L_d_w
        self.biases -= learning_rate * grad_z
        return d_L_d_input


class CNN:
    def __init__(self, learning_rate, max_iter):
        self.conv = ConvLayer(4, 3)
        self.pool = MaxPoolLayer(2)
        self.fc = FullyConnected(4 * 31 * 31, 1)  # pentru input 64x64

        self.lr = learning_rate
        self.epochs = max_iter

    def forward(self, image):
        if image.ndim == 3 and image.shape[2] in [1, 3]:
            image = image.transpose(2, 0, 1)  # (H, W, C) -> (C, H, W)
        out = self.conv.forward(image)
        out = relu(out)
        out = self.pool.forward(out)
        self.flattened = out.flatten()
        return self.fc.forward(self.flattened)

    def train(self, X, y):
        for epoch in range(self.epochs):
            loss = 0
            for i in range(len(X)):
                out = self.forward(X[i])
                loss += binary_cross_entropy(y[i], out)

                d_loss = out - y[i]
                d_out = self.fc.backward(d_loss, self.lr)

                x_trans = X[i].transpose(2, 0, 1)
                conv_out = self.conv.forward(x_trans)
                relu_out = relu(conv_out)
                pool_out = self.pool.forward(relu_out)

                d_out = d_out.reshape(pool_out.shape)
                d_out = self.pool.backward(d_out)
                d_out = d_out * relu_deriv(conv_out)
                self.conv.backward(d_out, self.lr)

            print(f"Iteration {epoch+1}, Loss: {loss / len(X):.6f}")

    def predict(self, X):
        preds = []
        for img in X:
            output = self.forward(img)
            preds.append(int(output > 0.5))
        return np.array(preds)