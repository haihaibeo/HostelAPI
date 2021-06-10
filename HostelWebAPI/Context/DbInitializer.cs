using System.Collections.Generic;
using System.Threading.Tasks;
using HostelWebAPI;
using HostelWebAPI.Models;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void Initialize(HostelDBContext context)
    {
        context.Database.Migrate();

        SeedPropertyStatusAsync(context).Wait();
        SeedAppRolesAsync(context).Wait();
        SeedReservationStatusAsync(context).Wait();
        SeedServiceName(context).Wait();
    }

    private static async Task SeedPropertyStatusAsync(HostelDBContext context)
    {
        var status = await context.PropertyStatus.ToListAsync();
        if (status.Count > 0) return;
        var defaultStt = new List<PropertyStatus>()
        {
            new PropertyStatus(){
                PropertyStatusId = PropertyStatusConst.OnValidation,
                Status = nameof(PropertyStatusConst.OnValidation)
            },
            new PropertyStatus(){
                PropertyStatusId = PropertyStatusConst.IsActive,
                Status = nameof(PropertyStatusConst.IsActive)
            },
            new PropertyStatus(){
                PropertyStatusId = PropertyStatusConst.IsClosed,
                Status = nameof(PropertyStatusConst.IsClosed)
            },
            new PropertyStatus(){
                PropertyStatusId = PropertyStatusConst.IsRejected,
                Status = nameof(PropertyStatusConst.IsRejected)
            },
        };
        context.PropertyStatus.AddRange(defaultStt);
        await context.SaveChangesAsync();
    }

    private static async Task SeedServiceName(HostelDBContext context)
    {
        var services = await context.Services.ToListAsync();
        if (services.Count > 0) return;

        var initServices = ServicesDefault.GetAllServices();
        foreach (var s in initServices)
            context.Services.Add(new Service() { ServiceId = s.Id, ServiceName = s.Name });

        await context.SaveChangesAsync();
    }

    private static Task SeedReservationStatusAsync(HostelDBContext context)
    {
        //TODO
        return Task.CompletedTask;
    }

    private static Task SeedAppRolesAsync(HostelDBContext context)
    {
        //TODO
        return Task.CompletedTask;
    }
}