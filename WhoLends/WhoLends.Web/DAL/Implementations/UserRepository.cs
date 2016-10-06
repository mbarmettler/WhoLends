using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private Entities context;
        private bool disposed = false;
        public UserRepository(Entities context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return this.context.User.ToList();
        }
        
        public User GetUserById(int userId)
        {
            return this.context.User.Find(userId);
        }

        public void UpdateUser(User user)
        {
            this.context.Entry(user).State = EntityState.Modified;
        }

        public void InsertUser(User user)
        {
            this.context.User.Add(user);
        }
        
        public void DeleteUser(int userId)
        {
            User user = this.context.User.Find(userId);
            this.context.User.Remove(user);
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
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}