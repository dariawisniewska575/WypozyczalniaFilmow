using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieRentApp.Context;
using MovieRentApp.MVVM.ViewModels;
using MovieRentApp.MVVM.Views;
using MovieRentApp.Repository;
using System.IO;
using System.Windows;

namespace MovieRentApp;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        var _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                });
                services.AddScoped<IRepository, Repository.Repository>();
                services.AddScoped<MainViewModel>();
                services.AddTransient<MainWindow>();
                services.AddScoped<MovieViewModel>();
                services.AddScoped<MovieView>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.DataContext = AppHost.Services.GetRequiredService<MainViewModel>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
