using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;
using System.Linq;

namespace MusicQuizAPI.Services
{
    public class FriendshipService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserRepository _userRepo;
        private readonly FriendshipRepository _friendshipRepository;

        public FriendshipService(ILogger<UserService> logger, UserRepository userRepo,
            FriendshipRepository friendshipRepository)
        {
            _logger = logger;
            _userRepo = userRepo;
            _friendshipRepository = friendshipRepository;
        }

        public bool Send(User user, int id)
        {
            if (id >= 0)
            {
                User potentialFriend = _userRepo.Get(id);

                if (potentialFriend != null)
                {
                    if (!_friendshipRepository.Exist(user.UserID, potentialFriend.UserID))
                    {
                        return _friendshipRepository.Add(new Friendship
                        {
                            RequestedUserID = user.UserID,
                            AcceptedUserID = potentialFriend.UserID,
                            StartDate = DateTime.Now
                        }) > 0;
                    }
                }
            }

            return false;
        }

        public bool Accept(User user, int id)
        {
            if (id >= 0)
            {
                Friendship fs = _friendshipRepository.Get(user.UserID, id);

                if (fs != null)
                {
                    fs.IsAccepted = true;
                    return _friendshipRepository.Update(fs) > 0;
                }
            }

            return false;
        }

        public bool Remove(User user, int id)
        {
            if (id >= 0)
            {
                Friendship fs = _friendshipRepository.Get(user.UserID, id);

                if (fs != null)
                {
                    return _friendshipRepository.Remove(fs) > 0;
                }
            }

            return false;
        }

        public List<User> GetFriends(User user, int limit)
            => _friendshipRepository.GetFriends(user.UserID)
                .Take(limit)
                .Select(u => {u.IsFriend = true; return u; })
                .ToList();
    }
}