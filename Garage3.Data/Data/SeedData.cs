using Bogus;
using Bogus.Extensions.Sweden;
using Garage3.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Garage3.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
       
        public static async Task InitAsync(Garage3Context db)
        {
            if (await db.Member.AnyAsync()) return;

            faker = new Faker("sv");

            var members = GenerateMembers(20);
            await db.AddRangeAsync(members);           

            await db.SaveChangesAsync();
        }

        private static IEnumerable<Member> GenerateMembers(int numberOfMembers)
        {
            var rnd = new Random();
            var members = new List<Member>();

            for (int i = 0; i < numberOfMembers; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var memberNo = faker.Random.Number(1, 10000).ToString();
                var personalNo = faker.Person.Personnummer();
                var vehicles = GenerateVehicles(rnd.Next(1, 4));

                var member = new Member
                {
                    FirstName = fName,
                    LastName = lName,
                    MemberNo = memberNo,
                    PersonalNo = personalNo,
                    Vehicles = vehicles.ToList(),                    
                };

                members.Add(member);
            }

            return members;
        }


        private static IEnumerable<Vehicle> GenerateVehicles(int numberOfVehicles)
        {
            var vehicles = new List<Vehicle>();

            for (int i = 0; i < numberOfVehicles; i++)
            {
                var regNo = faker.Vehicle.Vin().Substring(0,6);
                var brand = faker.Vehicle.Manufacturer();
                var vehicleType = faker.Vehicle.Type();
                var vehicleModel = faker.Vehicle.Model();
                var color = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.Color());
                var arrivalTime = faker.Date.Recent();
                var isParked = faker.Random.Bool();

                var vehicle = new Vehicle
                {
                    RegNo = regNo,
                    Brand = brand,
                    VehicleModel = vehicleModel,
                    Color = color,
                    ArrivalTime = arrivalTime,
                    IsParked = isParked,

                    VehicleType = new Core.VehicleType
                    {
                        Type = vehicleType
                    }
                };

                vehicles.Add(vehicle);
            }

            return vehicles;
        }














    }
}
