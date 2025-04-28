namespace MuseMarket.Services
{
    public interface IImageComparisonService
    {
        Task<string> CompareImageAsync(byte[] imageBytes);
    }
}
