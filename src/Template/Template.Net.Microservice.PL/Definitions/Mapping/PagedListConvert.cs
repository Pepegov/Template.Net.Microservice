using AutoMapper;
using Pepegov.UnitOfWork.Entityes;

namespace Template.Net.Microservice.PL.Definitions.Mapping;

/// <inheritdoc />
public class PagedListConvert<TMapFrom, TMapTo> : ITypeConverter<IPagedList<TMapFrom>, IPagedList<TMapTo>>
{
    /// <inheritdoc />
    public IPagedList<TMapTo> Convert(IPagedList<TMapFrom>? source, IPagedList<TMapTo> destination, ResolutionContext context) =>
        source == null
            ? PagedList.Empty<TMapTo>()
            : PagedList.From(source, items => context.Mapper.Map<IEnumerable<TMapTo>>(items));
}