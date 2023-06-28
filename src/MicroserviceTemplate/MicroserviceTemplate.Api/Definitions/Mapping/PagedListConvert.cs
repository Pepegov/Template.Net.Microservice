using AutoMapper;
using Pepegov.UnitOfWork.Entityes;
using Pepegov.UnitOfWork.EntityFramework.Repository;

namespace MicroserviceTemplate.Api.Definitions.Mapping;

public class PagedListConvert<TMapFrom, TMapTo> : ITypeConverter<IPagedList<TMapFrom>, IPagedList<TMapTo>>
{
    public IPagedList<TMapTo> Convert(IPagedList<TMapFrom> source, IPagedList<TMapTo> destination, ResolutionContext context) =>
        source == null
            ? PagedList.Empty<TMapTo>()
            : PagedList.From(source, items => context.Mapper.Map<IEnumerable<TMapTo>>(items));
}