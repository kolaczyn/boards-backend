using boards.Application.Dto;
using boards.Domain.Queries;

namespace boards.Application.Mappers;

public static class SortOrderMapper
{
    public static ThreadSortOrder ToDomain(this ThreadSortOrderDto dto) =>
        dto switch
        {
            ThreadSortOrderDto.BumpOrder => ThreadSortOrder.BumpOrder,
            ThreadSortOrderDto.CreationDate => ThreadSortOrder.CreationDate,
            ThreadSortOrderDto.ReplyCount => ThreadSortOrder.ReplyCount,
        };
}