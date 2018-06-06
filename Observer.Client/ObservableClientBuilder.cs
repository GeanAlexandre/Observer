﻿using System.Collections.Generic;
using System.Linq;
using Observer.Core.Client;
using Observer.Core.Factories;
using Observer.Core.Server;

namespace Observer.Client
{
    public class ObservableClientBuilder : IObservableClientBuilder
    {
        public string Instance { get; private set; }
        public string Address { get; private set; }

        public readonly List<string> _observerServersAddress;
        public IEnumerable<IObserverServer> ObserverServers { get; private set; }

        public ObservableClientBuilder(string instance)
        {
            Instance = instance;
            _observerServersAddress = new List<string>();
        }

        public IObservableClient Build(IObserverServerFactory observerServerFactory)
        {
            ObserverServers = _observerServersAddress.Select(observerServerFactory.FromAddress);
            return new ObservableClient(this);
        }

        public IObservableClientBuilder UseUrls(string clientAddress)
        {
            Address = clientAddress;
            return this;
        }

        public IObservableClientBuilder AddObserver(string observerAddress)
        {
            _observerServersAddress.Add(observerAddress);
            return this;
        }
    }
}
