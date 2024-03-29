﻿using Core;
using Core.CommunityClasses;
using Core.NewsClasses;
using Core.UserClasses;
using DataBaseConnection;
using DataBaseConnection.Migrations;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _dataContext;
        private UserServiceMappers _mappers;

        public UsersService(DataContext dataContext)
        {
            _dataContext = dataContext;
            _mappers = new UserServiceMappers();
        }

        public List<UserDTO> GetUsers()
        {
            List<User> users = _dataContext.Users
                .Include(u => u.PostHistory)
                    .ThenInclude(p => p.Comments)
                .Include(u => u.EventsAttended)
                .Include(u => u.Comments)
                .ToList();

            return _mappers.MapUsersToUserDTOs(users);
        }

        public UserDTO GetUserByEmail(string email)
        {
            User? user = _dataContext.Users
                .Where(u => u.Email == email)
                .Include(a => a.PostHistory)
                .Include(a => a.EventsAttended)
                .Include(a => a.Comments)
                .FirstOrDefault();

            return _mappers.MapUserToUserDTO(user);
        }

        public List<GuestDTO> GetGuests()
        {
            List<Guest> guests = _dataContext.Guests
                .Include(g => g.PostHistory)
                .Include(g => g.EventsAttended)
                .Include(g => g.Comments)
                    .ThenInclude(c => c.Post)
                        .ThenInclude(b => b.User)
                .ToList();

            return _mappers.MapGuestsToGuestDTOs(guests);
        }

        public List<AdminDTO> GetAdmins() 
        {
            List<Admin> admins = _dataContext.Admins
                .Include(a => a.PostHistory)
                .Include(a => a.EventsAttended)
                .Include(a => a.Comments)
                    .ThenInclude(c => c.Post)
                        .ThenInclude(b => b.User)
                .ToList();

            return _mappers.MapAdminsToAdminDTOs(admins);
        }

        public List<ModeratorDTO> GetModerators()
        {
            List<Moderator> moderators = _dataContext.Moderators
                .Include(m => m.PostHistory)
                .Include(m => m.EventsAttended)
                .Include(m => m.Comments)
                    .ThenInclude(c => c.Post)
                        .ThenInclude(b => b.User)
                .ToList();

            return _mappers.MapModeratorsToModeratorDTOs(moderators);
        }

        public void AddUser(UserDTO user)
        {
            _dataContext.Users.Add(_mappers.MapUserDTOToUser(user));
            _dataContext.SaveChanges();
        }

public bool UpdateUser(UserDTO userDTO)
{

    User? existingUser = _dataContext.Users.Find(userDTO.UserId);

            bool userExists = false;

    if (existingUser != null)
    {
        if (userDTO.UserName != null)
            existingUser.UserName = userDTO.UserName;
        if (userDTO.ProfilePicturePath != null)
            existingUser.ProfilePicturePath = userDTO.ProfilePicturePath;
        if (userDTO.Description != null)
            existingUser.Description = userDTO.Description;
        
        _dataContext.SaveChanges();
        userExists = true;
    }

    return userExists;
}
        public bool DeleteUser(string userId)//////Is this a good approach with the bool????????
        {
            var userToDelete = _dataContext.Users.Find(userId);
            bool userExists = false;

            if (userToDelete != null)
            {
                _dataContext.Users.Remove(userToDelete);
                _dataContext.SaveChanges();
                userExists = true;
            }

            return userExists;
        }

    }
}
