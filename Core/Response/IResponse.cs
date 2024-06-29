

namespace Core.Response
{
    public interface IResponse
    {
        string Message { get; init; }
        bool Success { get; }
    }
}
