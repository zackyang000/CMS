using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AtomLab.Core
{
    /// <summary>
    ///  IoC容器生成类
    /// </summary>
    public static class UnityContainerHelper
    {
        /// <summary>
        /// 创建IoC容器
        /// </summary>
        /// <param name="containerConfigPath">配置文件相对路径，如：App.config，config/unity.config</param>
        /// <param name="containerName">装载容器名称，若为空或null将使用默认容器</param>
        /// <param name="sectionName">容器节点名称，若为空或null将默认使用unity节点</param>
        /// <returns></returns>
        public static IUnityContainer Create(string containerConfigPath, string containerName = null, string sectionName = null)
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                sectionName = UnityConfigurationSection.SectionName;
            }

            IUnityContainer container = new UnityContainer();
            var configuration = GetConfig(containerConfigPath, sectionName);

            if (string.IsNullOrEmpty(containerName))
            {
                configuration.Configure(container);
            }
            else
            {
                configuration.Configure(container, containerName);
            }
            return container;
        }

       /// <summary>
       /// 读取节点信息
       /// </summary>
        /// <param name="containerConfigPath">配置文件相对路径，如：App.config，config/unity.config</param>
        /// <param name="sectionName">容器节点名称</param>
       /// <returns></returns>
        private static UnityConfigurationSection GetConfig(string containerConfigPath, string sectionName)
        {
            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + containerConfigPath
            };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var configuration = (UnityConfigurationSection)config.GetSection(sectionName);
            return configuration;
        }
    }
}