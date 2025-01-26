using ASPBoilerplate;
using ASPBoilerplate.Modules.User;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Modules.User.Service;

namespace ASPBoilerplate.Services;

public class QueryResolver
{
    private readonly UserGraphqlQuery _query;

    public QueryResolver(AppDbContext dbContext)
    {
        _query = new UserGraphqlQuery(dbContext);
    }
    public GetRestrictedUserDetailsResponseDto? GetUser(string email)
    {
        return _query.GetUser(email);
    }

    public List<GetRestrictedUserDetailsResponseDto> GetUsers()
    {
        return _query.GetUsers();
    }
}