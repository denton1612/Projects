package ubb.scs.socialnetworkgui.repository.database;

import ubb.scs.socialnetworkgui.domain.Message;
import ubb.scs.socialnetworkgui.domain.validator.MessageValidator;
import ubb.scs.socialnetworkgui.domain.validator.Validator;
import ubb.scs.socialnetworkgui.repository.Repository;

import java.sql.*;
import java.time.LocalDateTime;
import java.util.HashSet;
import java.util.Optional;
import java.util.Set;

public class MessageDBRepository implements Repository<Long, Message> {

    private final String url;
    private final String username;
    private final String password;
    private final Validator<Message> messageValidator;

    public MessageDBRepository(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
        messageValidator = MessageValidator.getInstance();
    }

    @Override
    public Optional<Message> find(Long aLong) {
        Message message = null;
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement statement = connection.prepareStatement("SELECT * FROM messages WHERE id = ?")) {
            statement.setLong(1, aLong);
            ResultSet resultSet = statement.executeQuery();
            while (resultSet.next()) {
                message = new Message(resultSet.getLong("id"), resultSet.getLong("sender"), resultSet.getLong("receiver"), resultSet.getString("message"), resultSet.getObject("date", LocalDateTime.class), resultSet.getLong("replied_id"));
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return Optional.ofNullable(message);
    }

    @Override
    public Optional<Message> add(Message entity) {
        messageValidator.validate(entity);
        int result;
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement statement = connection.prepareStatement("INSERT INTO messages (sender, receiver, message, date, replied_id) VALUES (?, ?, ?, ?, ?)")) {
            statement.setLong(1, entity.getFrom());
            statement.setLong(2, entity.getTo());
            statement.setString(3, entity.getText());
            statement.setTimestamp(4, Timestamp.valueOf(LocalDateTime.now()));
            if (entity.getRepliedMessage() != null) {
                statement.setLong(5, entity.getRepliedMessage());
            } else {
                statement.setNull(5, Types.BIGINT);
            }
            result = statement.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        if (result > 0) {
            return Optional.empty();

        }
        return Optional.of(entity);
    }

    @Override
    public Optional<Message> update(Message entity) {
        return Optional.empty();
    }

    @Override
    public Optional<Message> delete(Long aLong) {
        return Optional.empty();
    }

    @Override
    public Set<Message> findAll() {
        Set<Message> messages = new HashSet<>();
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("SELECT * FROM messages ORDER BY date ASC");
             ResultSet resultSet = preparedStatement.executeQuery()) {
            while (resultSet.next()) {
                Long id = resultSet.getLong("id");
                Long from = resultSet.getLong("sender");
                Long to = resultSet.getLong("receiver");
                String text = resultSet.getString("message");
                LocalDateTime date = resultSet.getObject("date", LocalDateTime.class);
                Long repliedId = resultSet.getObject("replied_id", Long.class);
                if (repliedId != null) {
                    repliedId = repliedId.longValue();
                }
                else {
                    repliedId = null;
                }
                messages.add(new Message(id, from, to, text, date, repliedId));
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return messages;
    }
}

