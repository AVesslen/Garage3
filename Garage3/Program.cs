using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage3.Data;
using Garage3.Data.Data;
using Garage3.AutoMapper;

namespace Garage3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Garage3Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Garage3Context") ?? throw new InvalidOperationException("Connection string 'Garage3Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(MapperProfile));

            var app = builder.Build();

            // Seed data

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Garage3Context>();

                //db.Database.EnsureDeleted();    // Kommentera bort dessa två rader för att använda databasens befintliga data 
                //db.Database.Migrate();

                try
                {
                    await SeedData.InitAsync(db);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}