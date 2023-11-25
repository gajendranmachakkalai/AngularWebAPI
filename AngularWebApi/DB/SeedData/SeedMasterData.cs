using AngularWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Dp.Lll.Infrastrucutre.Model.SeedData
{
    public static class SeedMasterData
    {
        public static void SeedMaster(this ModelBuilder modelBuilder)
        {
            #region ApplicationMaster
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   UserId = 1,
                   UserName = "Gajendran",
                   EmailId = "gajendran@gmail.com",
                   Password = "Test@123$",
                   Address = "Chennai",
                   PhoneNumber = "8956231478"
               },
               new User
               {
                   UserId = 2,
                   UserName = "Gobi",
                   EmailId = "gobi@gmail.com",
                   Password = "Test@123$",
                   Address = "Coimbator",
                   PhoneNumber = "7894561237"
               },
               new User
               {
                   UserId = 3,
                   UserName = "vijeykumar",
                   EmailId = "vijey@gmail.com",
                   Password = "Test@123$",
                   Address = "Thirvanmiur",
                   PhoneNumber = "4567892589"
               },
               new User
               {
                   UserId = 4,
                   UserName = "Rahul",
                   EmailId = "rahul@gmail.com",
                   Password = "Test@123$",
                   Address = "Vadapalani",
                   PhoneNumber = "9856237415"
               }
            );
            #endregion

        }
    }
}
