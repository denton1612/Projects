package ubb.scs.socialnetworkgui.domain;

import java.time.LocalDateTime;

public class Message extends Entity<Long> {
    private long from;
    private long to;
    private String text;
    private final LocalDateTime timestamp;
    private final Long repliedMessage;

    public Message(Long id, Long from, Long to, String text, LocalDateTime timestamp, Long repliedMessage) {
        super.setId(id);
        this.from = from;
        this.to = to;
        this.text = text;
        this.timestamp = timestamp;
        this.repliedMessage = repliedMessage;
    }

    public Message(Long from, Long to, String text, Long repliedMessage) {
        this(null, from, to, text, LocalDateTime.now(), repliedMessage);
    }

    public Long getTo() {
        return to;
    }

    public void setTo(Long to) {
        this.to = to;
    }

    public Long getFrom() {
        return from;
    }

    public void setFrom(Long from) {
        this.from = from;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public LocalDateTime getTimestamp() {
        return timestamp;
    }

    public Long getRepliedMessage() {
        return repliedMessage;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Message message = (Message) o;
        return message.getId().equals(getId());
    }

    @Override
    public int hashCode() {
        return getId().hashCode();
    }
}
