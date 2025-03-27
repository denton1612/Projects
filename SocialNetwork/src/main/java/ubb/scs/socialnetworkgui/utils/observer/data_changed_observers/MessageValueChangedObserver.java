package ubb.scs.socialnetworkgui.utils.observer.data_changed_observers;

import ubb.scs.socialnetworkgui.domain.Message;
import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;
import ubb.scs.socialnetworkgui.utils.observer.Observer;

public interface MessageValueChangedObserver extends Observer<DataChangedEvent<Long, Message>> {

}
