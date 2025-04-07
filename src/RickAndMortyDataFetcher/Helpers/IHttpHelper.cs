namespace RickAndMortyDataFetcher.Helpers;

public interface IHttpHelper
{
    Task<HttpResponseMessage> SendGetRequestAsync(string endpoint, HttpClient httpClient, CancellationToken cancellationToken = default);
}
