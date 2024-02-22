namespace EpochFlow.ApiClient.Models
{
    [Flags]
    public enum Seasonality
    {
        None = 0,
        Day = 1,
        Week = 2,
        Month = 4,
        Year = 8
    }
}