package ubb.scs.socialnetworkgui.repository.paging;

import ubb.scs.socialnetworkgui.domain.Friendship;
import ubb.scs.socialnetworkgui.domain.Tuple;
import ubb.scs.socialnetworkgui.domain.dto.FriendshipDTOFilter;
import ubb.scs.socialnetworkgui.utils.paging.Page;
import ubb.scs.socialnetworkgui.utils.paging.Pageable;

public interface FriendshipsPagingRepository extends PagingRepository<Tuple<Long, Long>, Friendship> {
    public Page<Friendship> findAllOnPage(Pageable pageable, FriendshipDTOFilter filter);
}
