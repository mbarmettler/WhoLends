﻿using System;
using System.Collections.Generic;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();

        //Role specific Interfaces
        Role GetUserRoleByUserId(int userId);
        IEnumerable<Role> GetRoles();
    }
}
