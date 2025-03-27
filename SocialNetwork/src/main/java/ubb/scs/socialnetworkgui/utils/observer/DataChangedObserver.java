package ubb.scs.socialnetworkgui.utils.observer;

import ubb.scs.socialnetworkgui.domain.Entity;
import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;

public abstract class DataChangedObserver<ID, E extends Entity<ID>> implements Observer<DataChangedEvent<ID, E>> {
}
