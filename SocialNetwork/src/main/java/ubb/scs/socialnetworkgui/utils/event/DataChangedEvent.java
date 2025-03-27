package ubb.scs.socialnetworkgui.utils.event;

import ubb.scs.socialnetworkgui.domain.Entity;

public class DataChangedEvent<ID, E extends Entity<ID>> implements Event {
    private final EventTypes type;
    private final E data;
    private E oldData;

    public DataChangedEvent(EventTypes type, E data, E oldData) {
        this.type = type;
        this.data = data;
        this.oldData = oldData;
    }

    public DataChangedEvent(EventTypes type, E data) {
        this.type = type;
        this.data = data;
    }

    public EventTypes getType() {
        return type;
    }

    public E getData() {
        return data;
    }

    public E getOldData() {
        return oldData;
    }
}
