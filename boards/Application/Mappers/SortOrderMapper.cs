using boards.Application.Dto;
using boards.Domain.Queries;

namespace boards.Application.Mappers;

public static class SortOrderMapper
{
    public static ThreadSortOrder toDomain(this ThreadSortOrderDto dto)
    {
        return dto switch
        {
            ThreadSortOrderDto.CreationDate => ThreadSortOrder.CreationDate,
            ThreadSortOrderDto.ReplyCount => ThreadSortOrder.ReplyCount,
            // this should never happen, but just in case, the fallback is to sort by bump order
            // this should be the same as the default sort order in GetThreadsQueryDto
            _ => ThreadSortOrder.ReplyCount
        };
    }
    
}