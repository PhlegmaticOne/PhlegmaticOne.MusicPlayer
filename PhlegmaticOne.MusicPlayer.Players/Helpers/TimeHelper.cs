namespace PhlegmaticOne.MusicPlayer.Players.Helpers;

public static class TimeHelper
{
    /// <summary>
    /// Parses time from string
    /// </summary>
    /// <param name="time">Time in "hh:mm:ss" format</param>
    /// <returns>Time parsed from string</returns>
    public static TimeSpan ToTimeSpan(string time)
    {
        var times = time.Split(':').Select(int.Parse).ToArray();
        return times.Length switch
        {
            2 => new TimeSpan(0, times[0], times[1]),
            3 => new TimeSpan(times[0], times[1], times[2]),
            _ => TimeSpan.Zero
        };
    }
}