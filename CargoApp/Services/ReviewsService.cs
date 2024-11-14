using Microsoft.Extensions.Localization;

namespace CargoApp.Services;

public class ReviewsService : ServiceBase
{
    public ReviewsService(IHttpContextAccessor contextAccessor, UserManager<User> userManager, CargoAppContext context,
        IStringLocalizer<AnnotationsSharedResource> stringLocalizer) : base(contextAccessor, userManager, context, stringLocalizer)
    {
    }

    public async Task<bool> CanCreateReviewAsync(string userId, string receiverId)
    {
        return !await _context.Reviews.AnyAsync(r => r.SenderId == userId && r.ReceiverId == receiverId);
    }

    public async Task<List<Review>> NoTrackingReceivedReviewsAsync(string userId)
    {
        return await _context.Reviews
            .AsNoTracking()
            .Where(r => r.ReceiverId == userId)
            .Include(r => r.Sender)
            .ToListAsync();
    }

    public async Task<List<Review>> NoTrackingSentReviewsAsync(string userId)
    {
        return await _context.Reviews
            .AsNoTracking()
            .Where(r => r.SenderId == userId)
            .Include(r => r.Receiver)
            .ToListAsync();
    }

    public async Task<bool> CreateReviewAsync(Review review)
    {
        if (!TryGetUserId(out var userId)) return false;

        if (await _context.Reviews.AnyAsync(r => r.SenderId == userId && r.ReceiverId == r.ReceiverId)) return false;
        review.SenderId = userId!;
        review.AddTime = DateTime.UtcNow;

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteReviewAsync(string senderId, string receiverId)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.SenderId == senderId && r.ReceiverId == receiverId);
        if (review == null) return;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }
}
