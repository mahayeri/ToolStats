namespace ToolStats.Web;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// Gets the temperature in degrees Fahrenheit, calculated from <see cref="TemperatureC"/>.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
