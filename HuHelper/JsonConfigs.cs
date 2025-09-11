using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuHelper
{
    public class JsonConfigs
    {
        public JsonConfigs() { }

        private readonly static Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        private readonly static string configPath = Path.Combine(Environment.CurrentDirectory, "Config");

        public static T GetGasConfig<T>(string name) where T : new()
        {
            try
            {
                string jsonStr = string.Empty;
                if (keyValuePairs.ContainsKey(name))
                {
                    jsonStr = keyValuePairs[name];
                }
                else
                {
                    string path = Path.Combine(configPath , name + ".json");
                    if (!File.Exists(path))
                    {
                        T config = new T();
                        string output = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                        byte[] rs = Encoding.UTF8.GetBytes(output);
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8, FileOptions.WriteThrough))
                        {
                            fs.Write(rs, 0, rs.Length);
                            fs.Flush();
                        }
                        jsonStr = keyValuePairs[name];
                    }
                    else
                    {
                        jsonStr = File.ReadAllText(path);
                        keyValuePairs.Add(name, jsonStr);
                    }
                }
                T jsonObj = JsonConvert.DeserializeObject<T>(jsonStr);
                return jsonObj;
            }
            catch (Exception ex)
            {
                throw new Exception($"获取配置文件{name}失败:{ex.Message}");
            }
        }

        public static void SaveConfig<T>(T config)
        {
            try
            {
                string output = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                if (string.IsNullOrEmpty(output) || output.Length < 10) return;
                keyValuePairs[typeof(T).Name] = output;
                byte[] rs = Encoding.UTF8.GetBytes(output);
                string path = Path.Combine(configPath, typeof(T).Name + ".json");
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8, FileOptions.WriteThrough))
                {
                    fs.Write(rs, 0, rs.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
               throw new Exception ($"保存配置文件{typeof(T).Name}失败:{ex.Message}");
            }
        }
    }
}
