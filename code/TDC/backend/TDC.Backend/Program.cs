using Microsoft.EntityFrameworkCore;
using TDC.Backend.Database;
using TDC.Backend.DataRepository;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDomain;

public class Program
{
    public static void Main(string[] args)
    {
        StartUp(args);
    }

    private static void StartUp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TdcDbContext>(options =>
                                                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        RunServiceSetup(builder.Services);
        BuildApp(builder);
    }

    private static void BuildApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        var connectionString = builder.Configuration.GetConnectionString("Sql")!;
        MigrationService.UseEvolveMigration(connectionString);

        app.Run();
    }

    private static void RunServiceSetup(IServiceCollection services)
    {
        AddDatabaseInjections(services);
        AddDomainInjections(services);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void AddDatabaseInjections(IServiceCollection services)
    {
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IListItemRepository, ListItemRepository>();
        services.AddTransient<IListMemberRepository, ListMemberRepository>();
        services.AddTransient<IListRepository, ListRepository>();
    }

    private static void AddDomainInjections(IServiceCollection services)
    {
        services.AddTransient<IToDoListHandler, ToDoListHandler>();
        services.AddTransient<IAccountHandler, AccountHandler>();
    }
}

