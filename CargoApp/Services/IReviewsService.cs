namespace CargoApp.Services;

public interface IReviewsService
{
    public Task<bool> CanCreateReviewAsync(string userId, string receiverId);

    public Task<List<Review>> NoTrackingReceivedReviewsAsync(string userId);

    public Task<List<Review>> NoTrackingSentReviewsAsync(string userId);

    public Task<bool> CreateReviewAsync(Review review);

    public Task DeleteReviewAsync(string senderId, string receiverId);
}
