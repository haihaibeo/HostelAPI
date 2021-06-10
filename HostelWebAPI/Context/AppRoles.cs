using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HostelWebAPI
{
    public static class AppRoles
    {
        public const string User = "User";
        public const string Owner = "Owner";
        public const string Admin = "Admin";
    }

    public static class PropertyStatusConst
    {
        public const string OnValidation = "1";
        public const string IsActive = "2";
        public const string IsClosed = "3";
        public const string IsRejected = "4";
    }

    public static class ReservationStatus
    {
        public const string OnReserved = "1";
        public const string Active = "2";
        public const string Completed = "3";
    }

    public static class ServicesDefault
    {
        public class ServiceModel
        {
            public ServiceModel(string id, string name, string description = null)
            {
                Id = id;
                Name = name;
                this.Description = description;
            }
            public string Id { get; set; }
            public string Name { get; set; }

            public string Description { get; set; }
        }

        public static readonly ServiceModel Wifi = new ServiceModel("1", "Free Wifi");
        public static readonly ServiceModel FreeParking = new ServiceModel("2", "Free Parking");
        public static readonly ServiceModel PetAllowed = new ServiceModel("3", "Pet Allowed");
        public static readonly ServiceModel FreeBreakfast = new ServiceModel("4", "Free Breakfast");
        public static readonly ServiceModel AirConditioner = new ServiceModel("5", "Air Conditioner");
        public static readonly ServiceModel Kitchen = new ServiceModel("6", "Kitchen Included");
        public static List<ServiceModel> GetAllServices()
        {
            var res = new List<ServiceModel>();
            foreach (FieldInfo field in typeof(ServicesDefault).GetFields())
            {
                var fieldType = field.GetValue(null).GetType();
                if (fieldType == typeof(ServiceModel))
                {
                    res.Add((ServiceModel)field.GetValue(null));
                }
            }
            return res;
        }
    }
    public enum PaymentStatus
    {
        Paid = 1,
        UnPaid = 2
    }

    public static class AppPolicies
    {
        public const string RequiredOwnerRole = "RequiredOwnerRole";
        public const string RequiredAdminRole = "RequiredAdminRole";
        public const string CanDeleteReservation = "CanDeleteReservation";
        public const string OwnerDeleteProperty = "OwnerDeleteProperty";
    }
}
;