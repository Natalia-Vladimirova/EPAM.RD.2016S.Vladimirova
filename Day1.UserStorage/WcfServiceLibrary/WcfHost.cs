﻿using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using UserStorage.Interfaces.Services;

namespace WcfServiceLibrary
{
    /// <summary>
    /// Represents wcf host for user service.
    /// </summary>
    public class WcfHost : MarshalByRefObject
    {
        private ServiceHost host;
        private IUserService userService;

        /// <summary>
        /// Starts wcf host on given address.
        /// </summary>
        /// <param name="address">
        /// Address which wcf host will start on.
        /// </param>
        /// <param name="userService">
        /// Service that will work inside wcf host.
        /// </param>
        public void Start(string address, IUserService userService)
        {
            Uri baseAddress = new Uri(address);
            var service = new WcfUserService(userService);
            host = new ServiceHost(service, baseAddress);
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };
            host.Description.Behaviors.Add(smb);

            (userService as IServiceLoader)?.Load();
            (userService as IListener)?.ListenForUpdates();
            this.userService = userService;

            host.Open();

            Console.WriteLine($"Domain: {AppDomain.CurrentDomain.FriendlyName}");
            Console.WriteLine($"The service is ready at {baseAddress}");
            Console.WriteLine();
        }

        /// <summary>
        /// Closes wcf host.
        /// </summary>
        public void Close()
        {
            host.Close();

            (host as IDisposable)?.Dispose();
        }
    }
}
