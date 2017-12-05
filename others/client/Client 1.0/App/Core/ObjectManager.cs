using Genting.Infrastructure.CommonServices.Client.Core.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public class ObjectManager : IObjectManager
    {
        private static ObjectManager _intance;
        public static ObjectManager Instance
        {
            get
            {
                if (_intance == null)
                    _intance = new ObjectManager();

                return _intance;
            }
        }

        private ObjectConfiguration _config;

        private ObjectManager()
        {
            _config = ObjectConfiguration.Instance;
        }

        public void Resolve()
        {
            if (_interpreters == null)
            {
                _interpreters = new Dictionary<string, IInterpreter>();

                foreach (ObjectItem item in _config.Interpreters)
                {
                    IInterpreter interpreter = ObjectHelper.CreateInstance<IInterpreter>(item.Type);
                    interpreter.SetMessenger(this.Messenger);

                    ISetting setting = (interpreter as ISetting);
                    if (setting != null)
                        loadSetting(setting, item.Name);

                    _interpreters.Add(item.Name, interpreter);
                }
            }
        }

        private IMessenger _messenger;
        public IMessenger Messenger 
        {
            get
            {
                if(_messenger == null)
                {
                    _messenger = ObjectHelper.CreateInstance<IMessenger>(_config.Messenger.Type);

                    ISetting setting = (_messenger as ISetting);
                    if (setting != null)
                        loadSetting(setting, _config.Messenger.Name);
                }

                return _messenger;
            }
        }

        private Dictionary<string, IInterpreter> _interpreters;
        public Dictionary<string, IInterpreter> Interpreters
        {
            get
            {
                if (_interpreters == null)
                    this.Resolve();

                return _interpreters;
            }
        }

        private void loadSetting(ISetting setting, string name)
        {
            var sett = _config.Settings.FirstOrDefault(item => item.Name == name);
            if (sett != null)
            {
                setting.Set(sett.Data);
                setting.Init();
            }
        }
    }
}
