namespace ToolStats.Web;

/// <summary>
/// Client for accessing weather forecast data from a web API.
/// </summary>
/// <param name="httpClient">The HTTP client used to make API requests.</param>
public class WeatherApiClient(HttpClient httpClient)
{
    /// <summary>
    /// Retrieves weather forecasts from the API.
    /// </summary>
    /// <param name="maxItems">The maximum number of forecast items to return.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An array of <see cref="WeatherForecast"/> objects.</returns>
    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<WeatherForecast>? forecasts = null;

        // Enumerate weather forecasts from the API endpoint as an async stream.
        await foreach (WeatherForecast? forecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken).ConfigureAwait(false))
        {
            // Stop if the maximum number of items is reached.
            if (forecasts?.Count >= maxItems)
            {
                break;
            }

            // Add non-null forecasts to the list.
            if (forecast is not null)
            {
                forecasts ??= [];
                forecasts.Add(forecast);
            }
        }

        // Return the collected forecasts as an array, or an empty array if none were found.
        return forecasts?.ToArray() ?? [];
    }
}
