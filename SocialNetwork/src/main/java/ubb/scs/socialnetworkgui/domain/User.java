package ubb.scs.socialnetworkgui.domain;

import java.util.ArrayList;
import java.util.List;

public class User extends Entity<Long> {
    private String username;
    private String email;
    private String password;
    private List<Long> friends;

    public User(Long id, String username, String email, String password) {
        super.setId(id);
        this.username = username;
        this.email = email;
        this.password = password;
        friends = new ArrayList<>();
    }

    public User(String username, String email, String password) {
        this.username = username;
        this.email = email;
        this.password = password;
        friends = new ArrayList<>();
    }

    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }

    public String getEmail() {
        return email;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public List<Long> getFriends() {
        return friends;
    }

    public void setFriends(List<Long> friends) {
        this.friends = friends;
    }

    public void addFriend(Long userID) {
        friends.add(userID);
    }

    public void removeFriend(Long userID) {
        friends.remove(userID);
    }

    @Override
    public String toString() {
        return getId() + ". " + getUsername();
    }
}
