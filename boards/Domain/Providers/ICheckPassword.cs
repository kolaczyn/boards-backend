namespace boards.Domain.Providers;

public enum CheckPasswordResult
{
    Ok,
    WrongPassword,
}

public interface ICheckPassword
{
    public CheckPasswordResult Validate(string password);
    
}