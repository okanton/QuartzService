namespace QuartzService.HttpService
{
    public interface IHttpService
    {
        T GetRequestResult<T>(string urlController, string param);
    }
}