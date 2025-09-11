using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;

namespace HuHelper
{
    public class FileConfigs
    {
        private readonly static Dictionary<string, Dictionary<string, string>> keyValuePairs = new Dictionary<string, Dictionary<string, string>>();
        private readonly static string configPath = Path.Combine(Environment.CurrentDirectory, "Config");

        /// <summary>
        /// 添加config文件
        /// </summary>
        /// <param name="configName"></param>
        private static void AddConfig(string configName)
        {
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            FileStream newfile = null;
            if (!File.Exists(configName))
            {
                newfile = new FileStream(configName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 8, FileOptions.WriteThrough);
            }
            else
            {
                newfile = new FileStream(configName, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite, 8, FileOptions.WriteThrough);
            }
            var sw = new StreamWriter(newfile);
            string s = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\n" + "<configuration>" + "\n" + "<appSettings> " + "\n" + " </appSettings>" + "\n" + "</configuration>";
            sw.Write(s);
            sw.Close();
            newfile.Close();
        }

        /// <summary>
        /// 获取config文件参数
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetConfig(string configName, string key, string val = "0")
        {
            string path = Path.Combine(configPath, configName);
            if (keyValuePairs.TryGetValue(configName, out var keyValues))
            {
                if (keyValues.TryGetValue(key, out var value))
                {
                    return value;
                }
            }
            if (File.Exists(path))
            {
                var ecf = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = path
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                var keys = config.AppSettings.Settings.AllKeys.ToList();
                if (keys == null || keys.Count == 0)
                    return val;
                if (keys.Contains(key))
                {
                    val = config.AppSettings.Settings[key].Value.ToString();
                }
                else
                {
                    config.AppSettings.Settings.Add(key, val);
                    config.Save(ConfigurationSaveMode.Modified);
                }
                var pairs = new Dictionary<string, string>();
                foreach (var item in keys)
                {
                    pairs.Add(item.ToString(), config.AppSettings.Settings[item.ToString()].Value.ToString());
                }
                if (keyValuePairs.ContainsKey(configName))
                {
                    keyValuePairs[configName] = pairs;
                }
                else
                {
                    keyValuePairs.Add(configName, pairs);
                }
            }
            else
            {
                if (!File.Exists(path)) AddConfig(path);
                var ecf = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = path
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add(key, val);
                config.Save(ConfigurationSaveMode.Modified);
            }
            if (!keyValuePairs.ContainsKey(configName))
            {
                var pair = new Dictionary<string, string>
                {
                    { key, val }
                };
                keyValuePairs.Add(configName, pair);
            }
            else if (keyValuePairs.ContainsKey(configName))
            {
                if (!keyValuePairs[configName].ContainsKey(key))
                {
                    keyValuePairs[configName].Add(key, val);
                }
            }
            return val;
        }

        /// <summary>
        /// 设置config文件参数
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetConfig(string configName, string key, string value = "1")
        {
            string path = Path.Combine(configPath, configName);
            if (keyValuePairs.ContainsKey(configName) && keyValuePairs[configName].ContainsKey(key))
            {
                if (keyValuePairs[configName][key] == value)
                    return;
            }

            if (File.Exists(path))
            {
                var ecf = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = path
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);

                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }
                config.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                if (!File.Exists(path)) AddConfig(path);
                var ecf = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = path
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
            }
            if (!keyValuePairs.ContainsKey(configName))
            {
                var pair = new Dictionary<string, string>
                {
                    { key, value }
                };
                keyValuePairs.Add(configName, pair);
            }
            else if (keyValuePairs.ContainsKey(configName))
            {
                if (!keyValuePairs[configName].ContainsKey(key))
                {
                    keyValuePairs[configName].Add(key, value);
                }
                else
                {
                    keyValuePairs[configName][key] = value;
                }
            }
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
