using boards.Domain;
using Microsoft.Extensions.Options;

namespace boards.Infrastructure.Providers;

public class CheckPassword : ICheckPassword
{
    private readonly AppSettings _appSettings;

    public CheckPassword(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public CheckPasswordResult Validate(string password)
    {
        var correctPassword = _appSettings.Password;
        return password == correctPassword ? CheckPasswordResult.Ok : CheckPasswordResult.WrongPassword;
    }
}