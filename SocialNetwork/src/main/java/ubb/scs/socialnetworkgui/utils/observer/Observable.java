package ubb.scs.socialnetworkgui.utils.observer;


import ubb.scs.socialnetworkgui.utils.event.Event;

public interface Observable<E extends Event> {
    void addObserver(Observer<E> observer);
    void removeObserver(Observer<E> observer);
    void notifyObservers(E event);
}
