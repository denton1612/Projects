package ubb.scs.socialnetworkgui.repository.paging;

import javafx.scene.layout.Pane;
import ubb.scs.socialnetworkgui.domain.Entity;
import ubb.scs.socialnetworkgui.repository.Repository;
import ubb.scs.socialnetworkgui.utils.paging.Page;
import ubb.scs.socialnetworkgui.utils.paging.Pageable;

public interface PagingRepository<ID, E extends Entity<ID>> extends Repository<ID, E> {
    public Page<E> findAllOnPage(Pageable pageable);
}
