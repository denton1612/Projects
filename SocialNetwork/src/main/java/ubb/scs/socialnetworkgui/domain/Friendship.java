package ubb.scs.socialnetworkgui.domain;

import java.nio.file.Watchable;
import java.time.LocalDate;

public class Friendship extends Entity<Tuple<Long, Long>> {
    private final LocalDate createdAt;
    private Status status;

    public Friendship(Long firstID, Long secondID) {
        super.setId(new Tuple<>(firstID, secondID));
        createdAt = LocalDate.now();
        status = Status.WAITING;
    }

    public Friendship(Long firstID, Long secondID, LocalDate createdAt) {
        super.setId(new Tuple<>(firstID, secondID));
        this.createdAt = createdAt;
        status = Status.WAITING;
    }

    public Friendship(Long firstID, Long secondID, LocalDate createdAt, Status status) {
        this(firstID, secondID, createdAt);
        this.status = status;
    }

    public LocalDate getCreatedAt() {
        return createdAt;
    }

    public Long getFirst() {
        return getId().getFirst();
    }

    public Long getSecond() {
        return getId().getSecond();
    }

    public Status getStatus() {
        return status;
    }

    @Override
    public String toString() {
        return getFirst() + " - " + getSecond();
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Friendship friendship = (Friendship) o;
        return getId().equals(friendship.getId());
    }

    @Override
    public int hashCode() {
        return getId().hashCode();
    }
}
