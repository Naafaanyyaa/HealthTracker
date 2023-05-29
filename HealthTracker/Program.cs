using System.Configuration;
using System.Windows.Forms.Design;
using Autofac;
using HealthTracker.Interfaces;
using HealthTracker.RabbitMQ;
using HealthTracker.Services;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Collections.Specialized;

namespace HealthTracker
{
    internal static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var service = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = service.Build();


            var builder = new ContainerBuilder();
            builder.RegisterType<RequestToServerService>().As<IRequestToServerService>();
            builder.RegisterType<RabbitMqService>().As<IRabbitMqService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            var container = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(container));
        }
    }
}