package ubb.scs.socialnetworkgui.repository;

import ubb.scs.socialnetworkgui.domain.Entity;
import ubb.scs.socialnetworkgui.domain.validator.RepositoryException;

import java.util.Optional;

public interface Repository<ID, E extends Entity<ID>> {

    /**
     * search for the entity with the given id
     * @param id must not be null
     * @return Optional with searched entity if it exists, OptionalEmpty else
     * @throws IllegalArgumentException if the id is null
     */
    Optional<E> find(ID id);

    /**
     * add the entity given as parameter
     * @param entity must not be null
     * @return OptionalEmpty if entity was added, else Optional with the entity
     * @throws RepositoryException if the entity attributes are not valid
     * @throws IllegalArgumentException if the entity is null
     */
    Optional<E> add(E entity);

    /**
     *
     * @param entity must not be null
     * @return OptionalEmpty if the entity was updated, else Optional with the entity (it doesn't exist entity with same ID)
     * @throws RepositoryException if the entity attributes are not valid
     * @throws IllegalArgumentException if the entity is null
     */
    Optional<E> update(E entity);

    /**
     *
     * @param id must not be null
     * @return Optional with the deleted entity mapped to the id, else OptionalEmpty (entity doesn't exist)
     * @throws IllegalArgumentException if the entity is null
     */
    Optional<E> delete(ID id);


    Iterable<E> findAll();
}
