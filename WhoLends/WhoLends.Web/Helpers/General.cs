using System.Drawing;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using WhoLends.Data;
using WhoLends.ViewModels;
using WhoLends.Web.DAL;

namespace WhoLends.Web.Helpers
{
    public static class General
    {
        public static User GetCurrentUser(IUserRepository _userRepository)
        {
            ApplicationUser Auser = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());
            var dbUser = _userRepository.GetUserByEmail(Auser.Email);

            return dbUser;
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}