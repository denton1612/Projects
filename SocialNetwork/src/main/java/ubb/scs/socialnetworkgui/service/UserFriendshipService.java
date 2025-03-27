package ubb.scs.socialnetworkgui.service;

import ubb.scs.socialnetworkgui.domain.Friendship;
import ubb.scs.socialnetworkgui.domain.Status;
import ubb.scs.socialnetworkgui.domain.Tuple;
import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.domain.dto.FriendshipDTOFilter;
import ubb.scs.socialnetworkgui.repository.database.FriendshipDBRepository;
import ubb.scs.socialnetworkgui.repository.database.UserDBRepository;
import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;
import ubb.scs.socialnetworkgui.utils.event.EventTypes;
import ubb.scs.socialnetworkgui.utils.observer.Observable;
import ubb.scs.socialnetworkgui.utils.observer.Observer;
import ubb.scs.socialnetworkgui.utils.paging.Page;
import ubb.scs.socialnetworkgui.utils.paging.Pageable;

import java.util.*;
import java.util.function.Predicate;
import java.util.stream.Collectors;

public class UserFriendshipService implements Observable<DataChangedEvent<Long, User>> {
    private final UserDBRepository userRepository;
    private final FriendshipDBRepository friendshipRepository;
    private final List<Observer<DataChangedEvent<Long, User>>> observers;
    private final List<List<User>> communities;

    public UserFriendshipService(UserDBRepository userRepository, FriendshipDBRepository friendshipRepository) {
        this.userRepository = userRepository;
        this.friendshipRepository = friendshipRepository;
        this.communities = new ArrayList<>();
        this.observers = new ArrayList<>();
        loadFriends();
    }

    private void loadFriends() {
        for (User user : getAllUsers()) {
            user.setFriends(friendshipRepository.getFriendsByID(user.getId()));
        }
    }

    private void loadCommunities() {
        Iterable<User> users = userRepository.findAll();
        communities.clear();
        Set<User> visited = new HashSet<>();
        Stack<User> stack = new Stack<>();
        for (User user : users) {
            if (!visited.contains(user)) {
                communities.add(new ArrayList<>());
                stack.push(user);
                visited.add(user);
            }
            while (!stack.isEmpty()) {
                User u = stack.pop();
                communities.getLast().add(u);
                for (Long friendID : u.getFriends()) {
                    User friend = findUser(friendID);
                    if (!visited.contains(friend)) {
                        stack.push(friend);
                        visited.add(friend);
                    }
                }
            }
        }
    }

    public void addUser(String username, String email, String password) {
        if (userRepository.findByUsername(username).isPresent() || userRepository.findByEmail(email).isPresent()) {
            throw new ServiceException("Username or email are already in use");
        }
        Optional<User> optionalUser = userRepository.add(new User(username, email, password));
        if (optionalUser.isEmpty()) {
            notifyObservers(new DataChangedEvent<>(EventTypes.ADD_USER, new User(username, email, password)));
        }
    }

    public void deleteUser(Long id) {
        if (userRepository.delete(id).isEmpty()) {
            throw new ServiceException("User does not exist");
        }
    }

    public void updateUsername(Long id, String newUsername) {
        if (userRepository.find(id).isEmpty()) {
            throw new ServiceException("User does not exist");
        }
        userRepository.update(new User(id, newUsername, userRepository.find(id).get().getEmail(), userRepository.find(id).get().getPassword()));
    }

    public void updatePassword(Long id, String newPassword) {
        if (userRepository.find(id).isEmpty()) {
            throw new ServiceException("User does not exist");
        }
        userRepository.update(new User(id, userRepository.find(id).get().getUsername(), userRepository.find(id).get().getEmail(), newPassword));
    }

    public User findUser(Long id) {
        if (userRepository.find(id).isEmpty()) {
            throw new ServiceException("User does not exist");
        }
        return userRepository.find(id).get();
    }

    public User validateAccount(String username, String password) {
        if (userRepository.findByUserAndPassword(username, password).isEmpty()) {
            throw new ServiceException("This account does not exist");
        }
        return userRepository.findByUsername(username).get();
    }

    public int numberOfCommunities() {
        loadCommunities();
        return communities.size();
    }

    private Tuple<User, Integer> furthestFriend(User startUser) {
        Queue<Tuple<User, Integer>> queue = new LinkedList<>();
        Set<User> visited = new HashSet<>();
        queue.add(new Tuple<>(startUser, 0));
        visited.add(startUser);

        User furthestFriend = startUser;
        int maxDistance = 0;

        while (!queue.isEmpty()) {
            Tuple<User, Integer> currentUser = queue.remove();
            for (Long friendID : currentUser.getFirst().getFriends()) {
                User friend = findUser(friendID);
                if (!visited.contains(friend)) {
                    queue.add(new Tuple<>(friend, currentUser.getSecond() + 1));
                    visited.add(friend);
                    furthestFriend = friend;
                    maxDistance = currentUser.getSecond() + 1;
                }
            }
        }

        return new Tuple<>(furthestFriend, maxDistance);
    }

    private int LongestPathInCommunity(User startUser) {
        Tuple<User, Integer> result1 = this.furthestFriend(startUser);
        User furthestFriend = result1.getFirst();
        return furthestFriend(furthestFriend).getSecond();
    }

    public List<User> mostSociableCommunity() {
        loadCommunities();
        Optional<List<User>> mostSociable = communities.stream()
                .reduce((community1, community2) -> LongestPathInCommunity(community1.getFirst()) > LongestPathInCommunity(community2.getFirst()) ? community1 : community2);
        return mostSociable.get();
    }

    public void sendFriendRequest(Long idFirst, Long idSecond) {
        Optional<Friendship> optionalFriendship = friendshipRepository.add(new Friendship(idFirst, idSecond));
        if (optionalFriendship.isPresent()) {
            throw new ServiceException("Request already exists");
        }
        notifyObservers(new DataChangedEvent<>(EventTypes.ADD_REQUEST, findUser(idFirst), findUser(idSecond)));
    }

    public void acceptFriendRequest(Long idFirst, Long idSecond) {
        Friendship request = findFriendship(idFirst, idSecond);
        Friendship friendship = new Friendship(idFirst, idSecond, request.getCreatedAt(), Status.ACCEPTED);
        Optional<Friendship> optionalFriendship = friendshipRepository.update(friendship);
        if (optionalFriendship.isEmpty()) {
            notifyObservers(new DataChangedEvent<>(EventTypes.UPDATE_FRIENDSHIP, findUser(idFirst), findUser(idSecond)));
        }
    }

    public void declineFriendRequest(Long idFirst, Long idSecond) {
        Optional<Friendship> optionalFriendship = friendshipRepository.delete(new Tuple<>(idFirst, idSecond));
        if (optionalFriendship.isEmpty()) {
            throw new ServiceException("Request does not exist");
        }
        notifyObservers(new DataChangedEvent<>(EventTypes.DELETE_REQUEST, findUser(idFirst), findUser(idSecond)));
    }

    public void deleteFriendship(Long idFirst, Long idSecond) {
        Optional<Friendship> optionalFriendship = friendshipRepository.delete(new Tuple<>(idFirst, idSecond));
        if (optionalFriendship.isEmpty()) {
            throw new ServiceException("Friendship does not exist");
        }
        notifyObservers(new DataChangedEvent<>(EventTypes.DELETE_FRIENDSHIP, findUser(idFirst), findUser(idSecond)));
    }

    public Set<Friendship> getReceivedRequests(long id) {
        Set<Friendship> requests = getAllFriendships();
        Predicate<Friendship> isRequest = (request -> request.getStatus() == Status.WAITING);
        Predicate<Friendship> isReceived = (request -> request.getSecond() == id);
        requests = requests.stream()
                .filter(isRequest.and(isReceived))
                .collect(Collectors.toSet());
        return requests;
    }

    public Page<Friendship> findAllOnPage(Pageable pageable) {
        return friendshipRepository.findAllOnPage(pageable);
    }

    public Page<Friendship> findAllOnPage(Pageable pageable, FriendshipDTOFilter filter) {
        return friendshipRepository.findAllOnPage(pageable, filter);
    }

    public Set<Friendship> getSentRequests(long id) {
        Set<Friendship> requests = getAllFriendships();
        Predicate<Friendship> isRequest = (request -> request.getStatus() == Status.WAITING);
        Predicate<Friendship> isSent = (request -> request.getFirst() == id);
        requests = requests.stream()
                .filter(isRequest.and(isSent))
                .collect(Collectors.toSet());
        return requests;
    }

    public Set<Friendship> getFriendships(long id) {
        Set<Friendship> friendships = getAllFriendships();
        Predicate<Friendship> isFriendship = (request -> request.getStatus() == Status.ACCEPTED);
        Predicate<Friendship> isMine = (request -> request.getSecond() == id || request.getFirst() == id);
        friendships = friendships.stream()
                .filter(isFriendship.and(isMine))
                .collect(Collectors.toSet());
        return friendships;
    }

    public Set<User> getOthers(long id) {
        Set<User> others = new HashSet<>();
        for (User user : getAllUsers()) {
            Optional<Friendship> optional = friendshipRepository.find(new Tuple<>(id, user.getId()));
            if (optional.isEmpty() && user.getId() != id) {
                others.add(user);
            }
        }
        return others;
    }

    public Set<User> getFriends(long id) {
        List<Long> friends = friendshipRepository.getFriendsByID(id);
        return friends.stream()
                .map(this::findUser)
                .collect(Collectors.toSet());
    }

    public Friendship findFriendship(Long firstID, Long secondID) {
        Optional<Friendship> optionalFriendship = friendshipRepository.find(new Tuple<>(firstID, secondID));
        if (optionalFriendship.isPresent()) {
            return optionalFriendship.get();
        }
        throw new ServiceException("Friendship does not exist");
    }

    public Set<User> getAllUsers() {return userRepository.findAll();}

    public Set<Friendship> getAllFriendships() {
        return friendshipRepository.findAll();
    }

    @Override
    public void addObserver(Observer<DataChangedEvent<Long, User>> observer) {
        observers.add(observer);
    }

    @Override
    public void removeObserver(Observer<DataChangedEvent<Long, User>> observer) {
        observers.remove(observer);
    }

    @Override
    public void notifyObservers(DataChangedEvent<Long, User> event) {
        observers.forEach(observer -> observer.update(event));
    }
}
