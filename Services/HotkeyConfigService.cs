// 作者: mkx
// 日期: 2025-09-16
// 说明: 热键配置存储服务，负责热键配置的保存和加载。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。

using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using LOLGameMate.Models;

namespace LOLGameMate.Services
{
    /// <summary>
    /// 热键配置存储服务，使用JSON格式存储热键配置。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public class HotkeyConfigService
    {
        private readonly string _configFilePath;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 构造函数，初始化配置文件路径
        /// </summary>
        public HotkeyConfigService()
        {
            // 配置文件存储在应用程序数据目录
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(appDataPath, "LOLGameMate");
            
            // 确保目录存在
            Directory.CreateDirectory(appFolder);
            
            _configFilePath = Path.Combine(appFolder, "hotkey_config.json");
            
            // JSON序列化选项
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// 保存热键配置到文件
        /// 修改记录: 2025-09-16 mkx 实现配置保存功能
        /// </summary>
        /// <param name="config">要保存的热键配置</param>
        /// <returns>保存是否成功</returns>
        public bool SaveConfig(HotkeyConfig config)
        {
            try
            {
                if (config == null)
                {
                    throw new ArgumentNullException(nameof(config));
                }

                var validation = config.Validate();
                if (!validation.IsValid)
                {
                    throw new ArgumentException($"热键配置无效: {validation.ErrorMessage}");
                }

                var json = JsonSerializer.Serialize(config, _jsonOptions);
                File.WriteAllText(_configFilePath, json);
                
                return true;
            }
            catch (Exception ex)
            {
                // 记录错误（这里简单输出到控制台，实际项目中可能需要更完善的日志系统）
                Console.WriteLine($"保存热键配置失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 从文件加载热键配置
        /// 修改记录: 2025-09-16 mkx 实现配置加载功能
        /// </summary>
        /// <returns>加载的热键配置，如果失败则返回默认配置</returns>
        public HotkeyConfig LoadConfig()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    // 配置文件不存在，返回默认配置
                    return GetDefaultConfig();
                }

                var json = File.ReadAllText(_configFilePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return GetDefaultConfig();
                }

                var config = JsonSerializer.Deserialize<HotkeyConfig>(json, _jsonOptions);
                if (config == null)
                {
                    return GetDefaultConfig();
                }

                // 验证加载的配置
                var validation = config.Validate();
                if (!validation.IsValid)
                {
                    Console.WriteLine($"加载的热键配置无效: {validation.ErrorMessage}，使用默认配置");
                    return GetDefaultConfig();
                }

                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载热键配置失败: {ex.Message}，使用默认配置");
                return GetDefaultConfig();
            }
        }

        /// <summary>
        /// 获取默认热键配置（Ctrl+Alt+Q）
        /// 修改记录: 2025-09-16 mkx 设置默认热键为Ctrl+Alt+Q
        /// </summary>
        /// <returns>默认热键配置</returns>
        public HotkeyConfig GetDefaultConfig()
        {
            return new HotkeyConfig(
                HotkeyManager.Modifiers.Control | HotkeyManager.Modifiers.Alt,
                Keys.Q
            );
        }

        /// <summary>
        /// 重置为默认配置并保存
        /// 修改记录: 2025-09-16 mkx 实现重置配置功能
        /// </summary>
        /// <returns>重置是否成功</returns>
        public bool ResetToDefault()
        {
            try
            {
                var defaultConfig = GetDefaultConfig();
                return SaveConfig(defaultConfig);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"重置热键配置失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查配置文件是否存在
        /// </summary>
        /// <returns>配置文件是否存在</returns>
        public bool ConfigFileExists()
        {
            return File.Exists(_configFilePath);
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <returns>配置文件完整路径</returns>
        public string GetConfigFilePath()
        {
            return _configFilePath;
        }

        /// <summary>
        /// 删除配置文件
        /// 修改记录: 2025-09-16 mkx 实现删除配置文件功能
        /// </summary>
        /// <returns>删除是否成功</returns>
        public bool DeleteConfigFile()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    File.Delete(_configFilePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除配置文件失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 备份当前配置文件
        /// 修改记录: 2025-09-16 mkx 实现配置备份功能
        /// </summary>
        /// <returns>备份是否成功</returns>
        public bool BackupConfig()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    return false;
                }

                var backupPath = _configFilePath + $".backup.{DateTime.Now:yyyyMMdd_HHmmss}";
                File.Copy(_configFilePath, backupPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"备份配置文件失败: {ex.Message}");
                return false;
            }
        }
    }
}
