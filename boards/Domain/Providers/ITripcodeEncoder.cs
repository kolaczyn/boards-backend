namespace boards.Domain.Providers;

public interface ITripcodeEncoder
{
    public string? Encode(string tripcode);
}