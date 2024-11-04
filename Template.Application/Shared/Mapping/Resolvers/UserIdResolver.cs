using AutoMapper;
using Template.Application.Users;

namespace Template.Application.Shared.Mapping.Resolvers;

public class UserIdResolver<TSource, TDestination>(IUserContext userContext)
    : IValueResolver<TSource, TDestination, string>
{
    public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
    {
        return userContext.GetCurrentUser()!.Id;
    }
}