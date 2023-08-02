using boards.Application.Dto;
using boards.Domain.Queries;

namespace boards.Application.Mappers;

public static class SortOrderMapper
{
    public static ThreadSortOrder ToDomain(this ThreadSortOrderDto dto) =>
        dto switch
        {
            ThreadSortOrderDto.Bump => ThreadSortOrder.Bump,
            ThreadSortOrderDto.CreationDate => ThreadSortOrder.CreationDate,
            ThreadSortOrderDto.ReplyCount => ThreadSortOrder.ReplyCount,
        };
}