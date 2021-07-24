using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secure_privacy
{
    public class Configs
    {
        static string configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Configs.ini");

        public static string connectionString = "mongodb://16.162.26.14:12345";
        public static string dbName = "SecurePrivacy";

        public static void LoadConfig()
        {
           if (!File.Exists(configFilePath))
            {
                SaveConfig();
                return;
            }

            var configText = File.ReadAllText(configFilePath);
            var configs = JsonConvert.DeserializeObject<dynamic>(configText);

            if (configs.connectionString != null) connectionString = configs.connectionString;
            if (configs.dbName != null) dbName = configs.dbName;

            SaveConfig();
        }

        public static bool SaveConfig()
        {
            try
            {
                dynamic configs = new ExpandoObject();

                configs.connectionString = connectionString;
                configs.dbName = dbName;

                string configText = JsonConvert.SerializeObject(configs, Formatting.Indented);

                File.WriteAllBytes(configFilePath, ASCIIEncoding.UTF8.GetBytes(configText));

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("SaveConfigs", ex);
            }

            return false;
        }
    }
}
