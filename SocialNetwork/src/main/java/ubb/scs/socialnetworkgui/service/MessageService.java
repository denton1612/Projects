package ubb.scs.socialnetworkgui.service;

import ubb.scs.socialnetworkgui.domain.Message;
import ubb.scs.socialnetworkgui.repository.database.MessageDBRepository;
import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;
import ubb.scs.socialnetworkgui.utils.event.EventTypes;
import ubb.scs.socialnetworkgui.utils.observer.Observable;
import ubb.scs.socialnetworkgui.utils.observer.Observer;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.Set;
import java.util.function.Predicate;
import java.util.stream.Collectors;

public class MessageService implements Observable<DataChangedEvent<Long, Message>> {
    private final MessageDBRepository messageDBRepository;
    private final List<Observer<DataChangedEvent<Long, Message>>> observers;

    public MessageService(MessageDBRepository messageDBRepository) {
        this.messageDBRepository = messageDBRepository;
        this.observers = new ArrayList<>();
    }

    public void addMessage(long from, long to, String text, Long repliedID) {
        Message message = new Message(from, to, text, repliedID);
        Optional<Message> optionalMessage = messageDBRepository.add(message);
        if (optionalMessage.isEmpty()) {
            notifyObservers(new DataChangedEvent<>(EventTypes.ADD_MESSAGE, message));
        }
    }

    public Message find(long id) {
        return messageDBRepository.find(id).get();
    }

    public List<Message> getConversation(Long firstID, Long secondID) {
        Predicate<Message> sentMessage = message -> message.getFrom().equals(firstID) && message.getTo().equals(secondID);
        Predicate<Message> receivedMessage = message -> message.getFrom().equals(secondID) && message.getTo().equals(firstID);
        return messageDBRepository.findAll().stream()
                .filter(sentMessage.or(receivedMessage))
                .collect(Collectors.toList());
    }

    @Override
    public void addObserver(Observer<DataChangedEvent<Long, Message>> observer) {
        observers.add(observer);
    }

    @Override
    public void removeObserver(Observer<DataChangedEvent<Long, Message>> observer) {
        observers.remove(observer);
    }

    @Override
    public void notifyObservers(DataChangedEvent<Long, Message> event) {
        observers.forEach(observer -> observer.update(event));
    }
}
