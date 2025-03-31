package ubb.scs.socialnetworkgui.repository.database;

import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.domain.validator.UserValidator;
import ubb.scs.socialnetworkgui.domain.validator.Validator;
import ubb.scs.socialnetworkgui.repository.Repository;

import javax.xml.transform.Result;
import java.sql.*;
import java.util.HashSet;
import java.util.Optional;
import java.util.Set;

public class UserDBRepository implements Repository<Long, User> {
    private String url;
    private String username;
    private String password;
    private final Validator<User> validator;

    public UserDBRepository(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
        this.validator = UserValidator.getInstance();
    }

    @Override
    public Optional<User> find(Long aLong) {
        User user = null;
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("select * from public.users where id = ?")) {
            preparedStatement.setLong(1, aLong);
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                user = new User(resultSet.getLong("id"), resultSet.getString("username"), resultSet.getString("email"), resultSet.getString("password"));
            }
        }
        catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.ofNullable(user);
    }

    @Override
    public Optional<User> add(User entity) {
        int result = -1;
        validator.validate(entity);
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("insert into public.users values (?, ?, ?)")) {
            preparedStatement.setString(1, entity.getUsername());
            preparedStatement.setString(2, entity.getEmail());
            preparedStatement.setString(3, entity.getPassword());
            result = preparedStatement.executeUpdate();
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        if (result > 0) {
            return Optional.empty();
        }
        return Optional.of(entity);
    }

    @Override
    public Optional<User> update(User entity) {
        int result = -1;
        validator.validate(entity);
        try (Connection connection = DriverManager.getConnection(url, username, password);
            PreparedStatement preparedStatement = connection.prepareStatement("update public.users set username = ?, email = ?, password = ? where id = ?")) {
            preparedStatement.setString(1, entity.getUsername());
            preparedStatement.setString(2, entity.getEmail());
            preparedStatement.setString(3, entity.getPassword());
            preparedStatement.setLong(4, entity.getId());
            result = preparedStatement.executeUpdate();

        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        if (result > 0) {
            return Optional.empty();
        }
        return Optional.of(entity);
    }

    @Override
    public Optional<User> delete(Long aLong) {
        Optional<User> entity = find(aLong);
        try (Connection connection = DriverManager.getConnection(url, username, password);
            PreparedStatement preparedStatement = connection.prepareStatement("delete from public.users where id = ?")) {
            preparedStatement.setLong(1, aLong);
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        return entity;
    }

    @Override
    public Set<User> findAll() {
        Set<User> users = new HashSet<>();
        try (Connection connection = DriverManager.getConnection(url, username, password);
            PreparedStatement preparedStatement = connection.prepareStatement("select * from public.users");
            ResultSet resultSet = preparedStatement.executeQuery()) {
            while (resultSet.next()) {
                users.add(new User(resultSet.getLong("id"), resultSet.getString("username"), resultSet.getString("email"), resultSet.getString("password")));
            }
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        return users;
    }

    public Optional<User> findByUsername(String username) {
        User user = null;
        try (Connection connection = DriverManager.getConnection(url, this.username, password);
            PreparedStatement preparedStatement = connection.prepareStatement("select * from public.users where username = ?")) {
            preparedStatement.setString(1, username);
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                user = new User(resultSet.getLong("id"), resultSet.getString("username"), resultSet.getString("email"), resultSet.getString("password"));
            }
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        return Optional.ofNullable(user);
    }

    public Optional<User> findByEmail(String email) {
        User user = null;
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("select * from public.users where email = ?")) {
            preparedStatement.setString(1, email);
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                user = new User(resultSet.getLong("id"), resultSet.getString("username"), resultSet.getString("email"), resultSet.getString("password"));
            }
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        return Optional.ofNullable(user);
    }

    public Optional<User> findByUserAndPassword(String username, String password) {
        User user = null;
        try (Connection connection = DriverManager.getConnection(url, this.username, this.password);
             PreparedStatement preparedStatement = connection.prepareStatement("select * from public.users where username = ? and password = ?")) {
            preparedStatement.setString(1, username);
            preparedStatement.setString(2, password);
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                user = new User(resultSet.getLong("id"), resultSet.getString("username"), resultSet.getString("email"), resultSet.getString("password"));
            }
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }
        return Optional.ofNullable(user);
    }
}

