package ubb.scs.socialnetworkgui.repository.database;

import com.almasb.fxgl.scene3d.Cone;
import kotlin.Pair;
import ubb.scs.socialnetworkgui.domain.Friendship;
import ubb.scs.socialnetworkgui.domain.Status;
import ubb.scs.socialnetworkgui.domain.Tuple;
import ubb.scs.socialnetworkgui.domain.dto.FriendshipDTOFilter;
import ubb.scs.socialnetworkgui.domain.validator.FriendshipValidator;
import ubb.scs.socialnetworkgui.domain.validator.Validator;
import ubb.scs.socialnetworkgui.repository.paging.FriendshipsPagingRepository;
import ubb.scs.socialnetworkgui.utils.paging.Page;
import ubb.scs.socialnetworkgui.utils.paging.Pageable;

import java.sql.*;
import java.sql.Date;
import java.time.LocalDate;
import java.util.*;

public class FriendshipDBRepository implements FriendshipsPagingRepository {
    private final String url;
    private final String username;
    private final String password;
    private final Validator<Friendship> validator;

    public FriendshipDBRepository(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
        this.validator = FriendshipValidator.getInstance();
    }

    @Override
    public Optional<Friendship> find(Tuple<Long, Long> longLongTuple) {
        Friendship friendship = null;
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("SELECT * FROM friendships WHERE (first_id = ? and second_id = ?) or (first_id = ? and second_id = ?)")
        ) {
            preparedStatement.setLong(1, longLongTuple.getFirst());
            preparedStatement.setLong(2, longLongTuple.getSecond());
            preparedStatement.setLong(3, longLongTuple.getSecond());
            preparedStatement.setLong(4, longLongTuple.getFirst());
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                friendship = new Friendship(resultSet.getLong("first_id"), resultSet.getLong("second_id"), resultSet.getDate("date").toLocalDate());
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.ofNullable(friendship);
    }

    @Override
    public Optional<Friendship> add(Friendship entity) {
        int result = -1;
        validator.validate(entity);
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("INSERT INTO friendships (first_id, second_id, date, status) VALUES (?, ?, ?, 'WAITING')")) {
            preparedStatement.setLong(1, entity.getFirst());
            preparedStatement.setLong(2, entity.getSecond());
            preparedStatement.setDate(3, Date.valueOf(entity.getCreatedAt()));
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
    public Optional<Friendship> update(Friendship entity) {
        int result = -1;
        validator.validate(entity);
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("UPDATE friendships SET status = 'ACCEPTED', date = ? WHERE (first_id = ? and second_id = ?) or (first_id = ? and second_id = ?)")) {
            preparedStatement.setDate(1, Date.valueOf(LocalDate.now()));
            preparedStatement.setLong(2, entity.getFirst());
            preparedStatement.setLong(3, entity.getSecond());
            preparedStatement.setLong(4, entity.getSecond());
            preparedStatement.setLong(5, entity.getFirst());
            result = preparedStatement.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        if (result > 0) {
            return Optional.empty();
        }
        return Optional.of(entity);
    }

    @Override
    public Optional<Friendship> delete(Tuple<Long, Long> longLongTuple) {
        Optional<Friendship> friendship = find(longLongTuple);
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("DELETE FROM friendships WHERE (first_id = ? and second_id = ?) or (first_id = ? and second_id = ?)")) {
            preparedStatement.setLong(1, longLongTuple.getFirst());
            preparedStatement.setLong(2, longLongTuple.getSecond());
            preparedStatement.setLong(3, longLongTuple.getSecond());
            preparedStatement.setLong(4, longLongTuple.getFirst());
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return friendship;
    }

    public List<Long> getFriendsByID(Long userID) {
        List<Long> friends = new ArrayList<>();
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatementFirst = connection.prepareStatement("SELECT first_id FROM friendships WHERE second_id = ? and STATUS = 'ACCEPTED'");
             PreparedStatement preparedStatementSecond = connection.prepareStatement("SELECT second_id FROM friendships WHERE first_id = ? and STATUS = 'ACCEPTED'");) {
            preparedStatementFirst.setLong(1, userID);
            preparedStatementSecond.setLong(1, userID);
            ResultSet resultSetFirst = preparedStatementFirst.executeQuery();
            while (resultSetFirst.next()) {
                friends.add(resultSetFirst.getLong("first_id"));
            }
            ResultSet resultSetSecond = preparedStatementSecond.executeQuery();
            while (resultSetSecond.next()) {
                friends.add(resultSetSecond.getLong("second_id"));
            }
            return friends;
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Set<Friendship> findAll() {
        Set<Friendship> friendships = new HashSet<>();
        try (Connection connection = DriverManager.getConnection(url, username, password);
             PreparedStatement preparedStatement = connection.prepareStatement("SELECT * FROM friendships");
             ResultSet resultSet = preparedStatement.executeQuery()) {
            while (resultSet.next()) {
                friendships.add(new Friendship(resultSet.getLong("first_id"), resultSet.getLong("second_id"), resultSet.getDate("date").toLocalDate(), Status.valueOf(resultSet.getString("status"))));
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return friendships;
    }

    private Pair<String, List<Object>> getSql(FriendshipDTOFilter filter) {
        if (filter == null) {
            return new Pair<>("", Collections.emptyList());
        }
        List<String> conditions = new ArrayList<>();
        List<Object> params = new ArrayList<>();
        filter.getStatus().ifPresent(status -> {
            conditions.add("status = ?");
            params.add(status);
        });
        filter.getSenderID().ifPresent(senderID -> {
            conditions.add("first_id = ?");
            params.add(senderID);
        });
        filter.getReceiverID().ifPresent(receiverID -> {
            conditions.add("second_id = ?");
            params.add(receiverID);
        });
        String conds = String.join(" AND ", conditions);
        return new Pair<>(conds, params);
    }

    private int count(FriendshipDTOFilter filter) {
        String sql = "select count(*) as count from friendships";
        Pair<String, List<Object>> sqlFilter = getSql(filter);
        if (!sqlFilter.getFirst().isEmpty()) {
            sql += " where " + sqlFilter.getFirst();
        }
        try (Connection connection = DriverManager.getConnection(url, username, password);
            PreparedStatement preparedStatement = connection.prepareStatement(sql)) {
            int parameterIndex = 1;
            for (Object param : sqlFilter.getSecond()) {
                if (param instanceof Status) {
                    preparedStatement.setObject(parameterIndex++, param.toString(), Types.VARCHAR);
                }
                else
                    preparedStatement.setObject(parameterIndex++, param);
            }
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            return resultSet.getInt("count");
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Page<Friendship> findAllOnPage(Pageable pageable, FriendshipDTOFilter filter) {
        String sql = "select * from friendships";
        Pair<String, List<Object>> sqlFilter = getSql(filter);
        if (!sqlFilter.getFirst().isEmpty()) {
            sql += " where " + sqlFilter.getFirst();
        }
        sql += " limit ? offset ?";
        try (Connection connection = DriverManager.getConnection(url, username, password);
            PreparedStatement preparedStatement = connection.prepareStatement(sql)) {
            Set<Friendship> friendships = new HashSet<>();
            int parameterIndex = 1;
            for (Object param : sqlFilter.getSecond()) {
                if (param instanceof Status) {
                    preparedStatement.setObject(parameterIndex++, param.toString(), Types.VARCHAR);
                }
                else
                    preparedStatement.setObject(parameterIndex++, param);
            }
            preparedStatement.setInt(parameterIndex++, pageable.getPageSize());
            preparedStatement.setInt(parameterIndex, (pageable.getPageNumber() - 1) * pageable.getPageSize());
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                friendships.add(new Friendship(resultSet.getLong("first_id"), resultSet.getLong("second_id"), resultSet.getDate("date").toLocalDate(), Status.valueOf(resultSet.getString("status"))));
            }
            return new Page<>(friendships, count(filter));
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Page<Friendship> findAllOnPage(Pageable pageable) {
        return findAllOnPage(pageable, null);
    }
}
