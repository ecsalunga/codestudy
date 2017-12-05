using System.Collections.Generic;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using Genting.Infrastructure.CommonServices.Client.Core;

namespace Genting.Infrastructure.CommonServices.Client.Configurations.Autofac
{
    public class AutofacObjectManager: IObjectManager
    {
        private static AutofacObjectManager _instance;
        public static AutofacObjectManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AutofacObjectManager();

                return _instance;
            }
        }

        public AutofacObjectManager()
        {
            this.Interpreters = new Dictionary<string, IInterpreter>();
        }

        public Dictionary<string, IInterpreter> Interpreters { get; set; }

        public IMessenger Messenger { get; set; }

        public void Resolve()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            var Container = builder.Build();
            using (var scope = Container.BeginLifetimeScope())
            {
                this.Messenger = scope.Resolve<IMessenger>();
                ISetting messSett = (this.Messenger as ISetting);
                if (messSett != null)
                    messSett.Init();

                IInterpreter[] interpreters = scope.Resolve<IInterpreter[]>();
                foreach (IInterpreter item in interpreters)
                {
                    item.SetMessenger(this.Messenger);
                    ISetting setting = (item as ISetting);
                    if (setting != null)
                        setting.Init();

                    this.Interpreters.Add(item.InterpreterType, item);
                }
            }
        }
    }
}
