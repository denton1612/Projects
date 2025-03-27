package ubb.scs.socialnetworkgui.utils.observer;


import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;
import ubb.scs.socialnetworkgui.utils.event.Event;

public interface Observer<E extends Event> {
    void update(E event);
}
