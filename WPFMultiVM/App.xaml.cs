using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Net.Http;
using System.Windows;

using WPFMultiVM.Models;
using WPFMultiVM.Services;
using WPFMultiVM.ViewModels;
using WPFMultiVM.Views;

namespace WPFMultiVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging => logging.AddEventLog(eventLogSettings =>
                {
                    eventLogSettings.LogName = "Application"; //Default is Application
                    //eventLogSettings.MachineName = "MyComputer"; //Default is env:ComputerName
                    eventLogSettings.SourceName = "WPFMultiVM"; //Default is ".NET Runtime"
                }))
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddTransient<IAssignmentsService, AssignmentsService>();
                    //services.AddTransient<IPeopleService, PeopleService>();
                    //services.AddTransient<IWorkloadsService, WorkloadsService>();

                    services.AddHttpClient<AssignmentsService>(ConfigureHttpOptions);
                    services.AddHttpClient<PeopleService>(ConfigureHttpOptions);
                    services.AddHttpClient<WorkloadsService>(ConfigureHttpOptions);

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<AssignmentsViewModel>();
                    services.AddSingleton<PeopleViewModel>();
                    services.AddSingleton<WorkloadsViewModel>();

                    services.AddTransient<MainWindow>();
                })
                .Build();

            ServiceProvider = host.Services;
        }

        private void ConfigureHttpOptions(IServiceProvider provider, HttpClient client)
        {
            var configuration = provider.GetService<IConfiguration>();
            MyHttpOptions options = new MyHttpOptions();
            configuration.GetSection("HttpOptions").Bind(options);
            client.BaseAddress = new Uri(options.BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", options.Accept);
            client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Connection.AccessToken);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}