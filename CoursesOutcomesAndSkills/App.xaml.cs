﻿using CoursesOutcomesAndSkills.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace CoursesOutcomesAndSkills
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;

        public App()
        {
            host = CreateHostBuilder(Environment.GetCommandLineArgs())
                .Build();

            //TODO: Look into https://github.com/dapplo/Dapplo.Microsoft.Extensions.Hosting to see if there's a better way to be doing this
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<ISettingsManager<MySettings>, LocalFileSettingsManager<MySettings>>();
                services.AddSingleton<IDatabaseManagementService, DatabaseManagementService>();
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<ViewModelLocator>();
                services.AddSingleton<ICourseInfoRepository, PostgresCourseRepository>();
            });

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await host.StartAsync();

            Messenger.Default.Register<MessageBoxMessage>(this, (msg) =>
            {
                MessageBox.Show(msg.Message, msg.Title);
            });

            Resources.Add("Locator", host.Services.GetService<ViewModelLocator>());

            var mainWindow = host.Services.GetService<MainWindow>();
            mainWindow.Show();


            var handle = GetConsoleWindow();

            // Show
            ShowWindow(handle, SW_SHOW);
            // Hide
            ShowWindow(handle, SW_HIDE);
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            using(host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
    }
}
