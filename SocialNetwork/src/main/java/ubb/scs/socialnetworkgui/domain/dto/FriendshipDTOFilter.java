package ubb.scs.socialnetworkgui.domain.dto;

import ubb.scs.socialnetworkgui.domain.Status;

import java.util.Optional;

public class FriendshipDTOFilter {
    private Optional<Status> status = Optional.empty();
    private Optional<Long> senderID = Optional.empty();
    private Optional<Long> receiverID = Optional.empty();

    public Optional<Status> getStatus() {
        return status;
    }

    public void setStatus(Optional<Status> status) {
        this.status = status;
    }

    public Optional<Long> getSenderID() {
        return senderID;
    }

    public void setSenderID(Optional<Long> senderID) {
        this.senderID = senderID;
    }

    public Optional<Long> getReceiverID() {
        return receiverID;
    }

    public void setReceiverID(Optional<Long> receiverID) {
        this.receiverID = receiverID;
    }

}
