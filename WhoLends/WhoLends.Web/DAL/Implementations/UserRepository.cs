using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class UserRepository : IUserRepository
    {
        private Entities context;
        private bool disposed;
        public UserRepository(Entities context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.User.ToList();
        }
        
        public User GetUserById(int userId)
        {
            return context.User.Find(userId);
        }

        public Role GetUserRoleByUserId(int userId)
        {
            var userroleId = context.User.Find(userId).RoleId;
            return context.Role.Find(userroleId);
        }

        public User GetUserByEmail(string email)
        {
            return GetUsers().Where(user => user.Email.Equals(email)).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void InsertUser(User user)
        {
            context.User.Add(user);
            Save();
        }
        
        public void DeleteUser(int userId)
        {
            User user = context.User.Find(userId);
            context.User.Remove(user);
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Role> GetRoles()
        {
            return context.Role.ToList();
        }
    }
}