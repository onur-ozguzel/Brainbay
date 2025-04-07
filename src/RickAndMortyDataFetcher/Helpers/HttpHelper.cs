namespace RickAndMortyDataFetcher.Helpers;

public class HttpHelper : IHttpHelper
{
    public async Task<HttpResponseMessage> SendGetRequestAsync(string endpoint, HttpClient httpClient, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(endpoint, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(
                $"Request failed with status code {(int)response.StatusCode} ({response.ReasonPhrase}). " +
                $"Response: {errorContent}");
        }

         return response;
    }
}
