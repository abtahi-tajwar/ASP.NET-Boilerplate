using ASPBoilerplate;
using ASPBoilerplate.Modules.User;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Service;

class UserGraphqlQuery
{

    private readonly AppDbContext _dbContext;
    public UserGraphqlQuery(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public GetRestrictedUserDetailsResponseDto? GetUser([GraphQLName("email")] string email)
    {
        var user = _dbContext.RestrictedUsers.FirstOrDefault(u => u.Email == email);
        if (user == null) return null;
        return RestrictedUserBinder.GetUserDetailsEntityToDto(user);
    }


    public List<GetRestrictedUserDetailsResponseDto> GetUsers()
    {
        var users = _dbContext.RestrictedUsers.ToList();
        var convertUsers = users.Select(u => RestrictedUserBinder.GetUserDetailsEntityToDto(u)).ToList();
        return convertUsers;
    }
}