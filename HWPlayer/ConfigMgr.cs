// 2023-07-01, Bruce

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HWPlayer
{
    internal class ConfigMgr
    {
        private Configuration? m_configObject;

        public ConfigMgr()
        {
            string config_path = System.AppDomain.CurrentDomain.BaseDirectory + "HWPlayer.dll.config";
            Init(config_path);
        }

        //指定配置文件路径
        public bool Init(string configPath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configPath;
            m_configObject = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            if (m_configObject.HasFile)
                return true;
            else
                return false;
        }

        public bool LoadConfig(ref ConfigObj config)
        {
            try
            {
                config.win_left = Int32.Parse(GetConfig("win_left"));
                config.win_top = Int32.Parse(GetConfig("win_top"));
                config.win_width = Int32.Parse(GetConfig("win_width"));
                config.win_height = Int32.Parse(GetConfig("win_height"));
                config.open_url = GetConfig("open_url");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        //设置键值
        public bool SetConfig(string key, string value)
        {
            if (m_configObject == null)
                return false;
            
            try
            {
                if (!m_configObject.AppSettings.Settings.AllKeys.Contains(key))
                    m_configObject.AppSettings.Settings.Add(key, value);
                else
                    m_configObject.AppSettings.Settings[key].Value = value;
                m_configObject.Save(ConfigurationSaveMode.Modified);
                return true;
            }
            catch { return false; }
        }

        //获取键值
        public string GetConfig(string key)
        {
            if (m_configObject == null)
                return "";

            string val = string.Empty;
            if (m_configObject.AppSettings.Settings.AllKeys.Contains(key))
                val = m_configObject.AppSettings.Settings[key].Value;
            return val;
        }
    }
}