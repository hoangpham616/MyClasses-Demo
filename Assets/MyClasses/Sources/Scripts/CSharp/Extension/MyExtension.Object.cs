/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyExtension.Object (version 1.1)
 */

namespace MyClasses
{
    public static partial class MyExtension
    {
        /// <summary>
        /// Log info.
        /// </summary>
        [System.Diagnostics.Conditional("MY_LOGGER_ALL"), System.Diagnostics.Conditional("MY_LOGGER_INFO")]
        public static void LogInfo(this System.Object obj, string methodName, string log, ELogColor color = ELogColor.DEFAULT)
        {
            MyLogger.Info(obj.GetType().Name, methodName, log, color);
        }

        /// <summary>
        /// Log info.
        /// </summary>
        [System.Diagnostics.Conditional("MY_LOGGER_ALL"), System.Diagnostics.Conditional("MY_LOGGER_INFO")]
        public static void LogInfo(this System.Object obj, string log, ELogColor color = ELogColor.DEFAULT)
        {
            MyLogger.Info(obj.GetType().Name, log, color);
        }

        /// <summary>
        /// Log warning.
        /// </summary>
        [System.Diagnostics.Conditional("MY_LOGGER_ALL"), System.Diagnostics.Conditional("MY_LOGGER_WARNING")]
        public static void LogWarning(this System.Object obj, string methodName, string log, ELogColor color = ELogColor.DEFAULT)
        {
            MyLogger.Warning(obj.GetType().Name, methodName, log, color);
        }

        /// <summary>
        /// Log warning.
        /// </summary>
        [System.Diagnostics.Conditional("MY_LOGGER_ALL"), System.Diagnostics.Conditional("MY_LOGGER_WARNING")]
        public static void LogWarning(this System.Object obj, string log, ELogColor color = ELogColor.DEFAULT)
        {
            MyLogger.Warning(obj.GetType().Name, log, color);
        }

        /// <summary>
        /// Log error.
        /// </summary>
        [System.Diagnostics.Conditional("MY_LOGGER_ALL"), System.Diagnostics.Conditional("MY_LOGGER_ERROR")]
        public static void LogError(this System.Object obj, string methodName, string log, ELogColor color = ELogColor.DEFAULT)
        {
            MyLogger.Error(obj.GetType().Name, methodName, log, color);
        }

        /// <summary>
        /// Log error.
        /// </summary>
        [System.Diagnostics.Conditional("MY_LOGGER_ALL"), System.Diagnostics.Conditional("MY_LOGGER_ERROR")]
        public static void LogError(this System.Object obj, string log, ELogColor color = ELogColor.DEFAULT)
        {
            MyLogger.Error(obj.GetType().Name, log, color);
        }
    }
}