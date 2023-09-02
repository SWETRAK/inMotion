using IMS.Friends.Domain;
using IMS.Friends.Domain.Consts;
using IMS.Friends.Domain.Entities;
using IMS.Friends.IDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMS.Friends.DAL.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly ImsFriendsDbContext _context;
    private bool _disposed = false;

    public FriendshipRepository(ImsFriendsDbContext context)
    {
        _context = context;
    }

    public async Task<Friendship> GetById(Guid id)
    {
        return await _context.Friendships.FirstOrDefaultAsync(f => f.Id.Equals(id));
    }

    public async Task<Friendship> GetByUsersId(Guid firstUserId, Guid secondUserId)
    {
        return await _context
            .Friendships
            .FirstOrDefaultAsync(f =>
                (f.FirstUserId.Equals(firstUserId) && f.SecondUserId.Equals(secondUserId)) ||
                (f.FirstUserId.Equals(secondUserId) && f.SecondUserId.Equals(firstUserId))
            );
    }

    public async Task<List<Friendship>> GetRequested(Guid id)
    {
        return await _context
            .Friendships
            .Where(f => f.SecondUserId.Equals(id) && f.Status == FriendshipStatus.Waiting)
            .ToListAsync();
    }

    public async Task<List<Friendship>> GetInvitation(Guid id)
    {
        return await _context
            .Friendships
            .Where(f => f.FirstUserId.Equals(id) && f.Status == FriendshipStatus.Waiting)
            .ToListAsync();
    }
    
    public async Task<List<Friendship>> GetAccepted(Guid id)
    {
        return await _context
            .Friendships
            .Where(f => (f.SecondUserId.Equals(id) || f.FirstUserId.Equals(id)) && f.Status.Equals(FriendshipStatus.Accepted))
            .ToListAsync();
    }

    public async Task<List<Friendship>> GetRejected(Guid id)
    {
        return await _context
            .Friendships
            .Where(f => (f.FirstUserId.Equals(id) || f.SecondUserId.Equals(id)) && f.Status.Equals(FriendshipStatus.Accepted))
            .ToListAsync();
    }

    public void RemoveAsync(Friendship friendship)
    {
        _context.Friendships.Remove(friendship);
    }

    public async Task InsertAsync(Friendship friendship)
    {
        await _context.Friendships.AddAsync(friendship);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}