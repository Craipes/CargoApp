namespace CargoApp;

public static class HttpRequestExtensions
{
    public static bool IsAjax(this HttpRequest request)
    {
        if (request != null && request.Headers != null)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        return false;
    }
}
