﻿using Microsoft.Extensions.Configuration;
using System;

namespace Exico.Shopify.Web.Core.Modules
{
    /// <summary>
    /// Sometimes we need to use Config.Bind().
    /// But the porblem is that this Bind() method is an extension method and hard or not possible to
    /// mock using mocking framework. Thats why this interface is created for so that we can solve this 
    /// above issue.
    /// </summary>
    public interface IAppSettingsAccessor
    {

        /// <summary>
        /// This wuld read the appsettings file using the IConfiguration and
        /// bind the section (identified by the key) and return correct 
        /// object identified by T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="config"></param>
        /// <returns></returns>        
        T BindObject<T>(string key,IConfiguration config);
    }


    public class AppSettingsAccessor : IAppSettingsAccessor
    {
        public const string DB_CON_STRING_NAME = "DefaultConnection";
        public const string DB_DROP_RECREATE_IN_DEV = "DbDropRecreateInDev";
        public const string IDENTITY_CORE_AUTH_COOKIE_NAME = ".aspnetcore.exicoauthcookie";
        public const string USES_EMBEDED_SDK = "UsesEmbededSdk";

        public T BindObject<T>(string key, IConfiguration config)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            config.Bind(key, obj);
            return obj;
        }

        public static bool IsUsingEmbededSdk(IConfiguration config)
        {
            var value = config[USES_EMBEDED_SDK];
            if (string.IsNullOrEmpty(value)) return false;
            else return value.Equals("1");
        }
    }
}
