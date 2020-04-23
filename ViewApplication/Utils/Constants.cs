using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Utils
{
    public class Constants
    {
        public const string ClientNoToken = "webapi_no_token";
        public const string ClientWithToken = "webapi_with_token";
        public const string HumanDateFormat = "dd MMMM, yyyy";
        public const string SystemDateFormat = "dd/MM/yyyy";
        public class Keys
        {
            public const string JwtBearerSection = "Authentication:JwtBearer";
            public const string ApiBaseUrl = "App:ApiBaseUrl";
            public const string Alerts = "_Alerts";
        }
        public class URL
        {
            public const string Default = "/home/index";
            public const string LoginPath = "/account/login";
            public const string AccessDeniedPath = "/error/accessdenied";
        }
        public class Routes
        {

            //Account
            public const string AccountGetProfile = "/api/account/getprofile";
            public const string AccountGetClaims = "/api/account/GetCurrentUserClaims";
            public const string AccountUpdateProfile = "/api/account/updateprofile";
            public const string AccountChangePassword = "/api/account/changepassword";
            public const string AccountResetPassword = "/api/account/ForgotPassword";
            public const string AccountResetNewPassword = "/api/account/ResetPassword";

            //Cars
            public const string Car = "/api/cars";
            public const string GetCarsList = "/api/Cars/GetAll";
            public const string AddCar = "/api/Cars/Add";
            public const string DeleteCar = "/api/Cars/Remove{0}";
            public const string UpdateCar = "/api/Cars/Update/{0}";
            public const string GetCarById = "/api/Cars/GetCarById/{0}";
            public const string CalculateVAT = "/api/Cars/CalculateVAT/{0}";
            public const string GetLeased = "/api/cars/GetLeasedCars";
            public const string GetSold = "/api/cars/GetSoldCars";
            public const string GetAvailable = "/api/cars/GetAvailableCars";
            public const string GetDeleted = "/api/cars/Deleted";
            public const string CarSearch = "/api/cars/CarSearch";
            public const string Sale = "/api/cars/Sale";
            public const string Lease = "/api/cars/Lease";
            public const string GetSalesHistory = "/api/cars/GetSalesHistory";
            public const string GetLeaseHistory = "/api/cars/GetLeaseHistory";

            //Location

            public const string GetAllLocations = "/api/Location/GetAll";
            public const string GetAllLocations2 = "/api/Location/GetAll2";

            //Car Manufacturer 

            public const string GetAllCarManuacturer = "/api/CarManufacturers/GetCarManList";

            //Token
            public const string Token = "/api/token";
            public const string RefreshToken = "/api/token/refreshtoken";
            
        }
    }
}
