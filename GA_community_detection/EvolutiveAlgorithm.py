import math
import random


class Chromosome:
    def __init__(self, genes = None):
        self.genes = genes
        self.fitness = None

    def mutate(self, mutation_rate = 0.2):
        new_genes = self.genes[:]
        current_comms = set(new_genes)
        max_comm = max(current_comms)
        n = len(new_genes)

        for i in range(n):
            if random.random() < mutation_rate:
                if random.random() < 0.3:
                    # Creează o comunitate nouă
                    max_comm += 1
                    new_genes[i] = max_comm
                else:
                    # Mută într-o comunitate existentă (alta decât cea curentă)
                    possible = list(current_comms - {new_genes[i]})
                    if possible:
                        new_genes[i] = random.choice(possible)

        return Chromosome(new_genes)

    # uniform crossover
    @staticmethod
    def crossover(parent1, parent2):
        n = len(parent1.genes)
        child_genes = []

        for i in range(n):
            chosen = parent1.genes[i] if random.random() < 0.5 else parent2.genes[i]
            child_genes.append(chosen)

        mapping = {}
        new_val = 1
        for i in range(n):
            if child_genes[i] not in mapping:
                mapping[child_genes[i]] = new_val
                new_val += 1
            child_genes[i] = mapping[child_genes[i]]

        return Chromosome(child_genes)

    def __lt__(self, other):
        return self.fitness < other.fitness

    def __gt__(self, other):
        return self.fitness > other.fitness

    def __eq__(self, other):
        return self.fitness == other.fitness

class GA:
    def __init__(self, param, problem_param):
        self.param = param
        self.problem_param = problem_param
        self.population = []

    def initialization(self):
        for _ in range(self.param['popSize']):
            k_max = max(2, int(math.sqrt(self.problem_param['size'])))  # sau chiar n // 2
            genes = [random.randint(1, k_max) for _ in range(self.problem_param['size'])]
            self.population.append(Chromosome(genes))

    def evaluation(self):
        for chromosome in self.population:
            chromosome.fitness = self.problem_param['fitness'](self.problem_param['data'], chromosome.genes)

    def best_chromosome(self, population):
        if self.problem_param['maximize']:
            return max(population)
        return min(population)

    def worst_chromosome(self, population):
        if self.problem_param['maximize']:
            return min(population)
        return max(population)

    def selection_tournament(self, k = 3):
        tournament = random.sample(self.population, k)
        return self.best_chromosome(tournament)

    def next_generation(self):
        new_pop = []
        for _ in range(self.param['popSize']):
            parent1 = self.selection_tournament()
            parent2 = self.selection_tournament()
            child = Chromosome.crossover(parent1, parent2)
            child.mutate()
            new_pop.append(child)
        self.population = new_pop
        self.evaluation()

    def next_generation_elitism(self):
        new_pop = [self.best_chromosome(self.population)]
        for _ in range(self.param['popSize'] - 1):
            parent1 = self.selection_tournament()
            parent2 = self.selection_tournament()
            child = Chromosome.crossover(parent1, parent2)
            child.mutate()
            new_pop.append(child)
        self.population = new_pop
        self.evaluation()

    def solution(self, elitism = True):

        self.initialization()
        self.evaluation()

        best_fitness_per_gen = [self.best_chromosome(self.population).fitness]
        average_fitness_per_gen = [sum(map(lambda c: c.fitness, self.population))/len(self.population)]
        for _ in range(self.param['noGen']):
            if elitism:
                self.next_generation_elitism()
            else:
                self.next_generation()
            best_fitness_per_gen.append(self.best_chromosome(self.population).fitness)
            average_fitness_per_gen.append(sum(map(lambda c: c.fitness, self.population))/len(self.population))

        return self.best_chromosome(self.population).genes, best_fitness_per_gen, average_fitness_per_gen