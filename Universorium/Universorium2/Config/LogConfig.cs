using Universorium.Config.DataTypes;

namespace Universorium.Config
{
    public class LogConfig
    {
        private static LogConfig? instance = null;
        public static LogConfig GetInstance()
        {
            if (instance == null)
                instance = new LogConfig();
            return instance;
        }
        List<LogConfigItem> items;
        private LogConfig() 
        {
#if DEBUG
            string data = "[]";
            if (File.Exists("config/log.config.local"))
            {
                data = File.ReadAllText("config/log.config.local");
                items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LogConfigItem>>(data)!;
                return;
            }
#endif
            if (File.Exists("config/log.config"))
                data = File.ReadAllText("config/log.config");
            items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LogConfigItem>>(data)!;
        }

    }
}
