package ubb.scs.socialnetworkgui.domain.validator;

public interface Validator<T> {
    void validate(T t) throws RepositoryException;
}
