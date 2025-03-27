package ubb.scs.socialnetworkgui.domain;

public class Tuple<E1, E2> {
    private E1 first;
    private E2 second;

    public Tuple(E1 first, E2 second) {
        this.first = first;
        this.second = second;
    }

    public E1 getFirst() {
        return first;
    }

    public E2 getSecond() {
        return second;
    }

    public void setFirst(E1 first) {
        this.first = first;
    }

    public void setSecond(E2 second) {
        this.second = second;
    }

    @Override
    public String toString() {
        return "Tuple [first=" + first + ", second=" + second + "]";
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Tuple<?, ?> tuple = (Tuple<?, ?>) o;
        return (first.equals(tuple.first) && second.equals(tuple.second) || (first.equals(tuple.second) && second.equals(tuple.getFirst())));
    }

    @Override
    public int hashCode() {
        return first.hashCode() + second.hashCode();
    }
}
