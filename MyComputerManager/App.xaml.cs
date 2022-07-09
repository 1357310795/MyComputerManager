using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyComputerManager.Models;
using MyComputerManager.Services;
using MyComputerManager.Services.Contracts;
using MyComputerManager.ViewModels;
using MyComputerManager.Views;
using Wpf.Ui.Demo.Models;
using Wpf.Ui.Demo.Services;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;


namespace MyComputerManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Wpf.Ui.Appearance.Accent.Apply(Color.FromRgb(15, 123, 210));

            _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            })
            .ConfigureServices(ConfigureServices)
            .Build();

            _host.Start();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // App Host
            services.AddHostedService<ApplicationHostService>();

            // Theme manipulation
            services.AddSingleton<IThemeService, ThemeService>();

            // Taskbar manipulation
            services.AddSingleton<ITaskBarService, TaskBarService>();

            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Service containing navigation, same as INavigationWindow... but without window
            services.AddSingleton<INavigationService, NavigationService>();
            

            services.AddSingleton<ISnackBarService, SnackBarService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IDialogService, DialogService>();

            // Main window container with navigation
            services.AddScoped<INavigationWindow, MainWindow>();
            //services.AddScoped<ContainerViewModel>();

            // Views and ViewModels
            services.AddScoped<MainPage>();
            services.AddScoped<MainPageViewModel>();

            services.AddTransient<DetailPage>();
            services.AddTransient<DetailPageViewModel>();

            services.AddSingleton<AboutPage>();

            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _host.StopAsync().ConfigureAwait(false);
            _host.Dispose();
            _host = null;

        }
    }
}
