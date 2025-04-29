using System.Collections.Generic;
using UnityEngine;

namespace NKLogger
{
    public static class DebugPro
    {
#if UNITY_EDITOR
        private static Dictionary<string, string> CachedColors = new ();
        private static DebugProSettings _settings;
        
        private static DebugProSettings Settings => _settings ??= Resources.Load<DebugProSettings>("DebugProSettings");
#endif

        public static void Log(string message,
            object caller = null,
            string prefix = null,
            Object context = null,
            bool editorOnly = false,
            Color colorText = default,
            [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            Log(LogType.Log, message, caller, prefix, context, editorOnly, colorText, callerMemberName);
        }

        public static void LogWarning(string message,
            object caller = null,
            string prefix = null,
            Object context = null,
            bool editorOnly = false,
            Color colorText = default,
            [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            Log(LogType.Warning, message, caller, prefix, context, editorOnly, colorText, callerMemberName);
        }

        public static void LogError(string message,
            object caller = null,
            string prefix = null,
            Object context = null,
            bool editorOnly = false,
            Color colorText = default,
            [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            Log(LogType.Error, message, caller, prefix, context, editorOnly, colorText, callerMemberName);
        }

        public static void Reset()
        {
            CachedColors = new Dictionary<string, string>();
        } 


        private static void Log(LogType logType,
            string message,
            object caller = null,
            string prefix = null,
            Object context = null,
            bool editorOnly = false,
            Color colorText = default,
            [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
#if !UNITY_EDITOR
            if(editorOnly)
                return;
#endif

            string prefixResult;
            if(caller != null)
                prefixResult = caller.GetType().Name;
            else if(string.IsNullOrEmpty(prefix))
                prefixResult = Settings.prefix;
            else
                prefixResult = prefix;

#if UNITY_EDITOR
            var color = colorText == default ? GetPrefixColor(prefixResult) : ColorUtility.ToHtmlStringRGB(colorText);
            if(Settings.isFullColorized)
                message = $"<color=#{color}>[{prefixResult}] <b>\"{callerMemberName}\"</b>. {message}</color>";
            else
                message = $"<color=#{color}>[{prefixResult}]</color> <b>\"{callerMemberName}\"</b>. {message}";
#else
            message = $"[{prefixResult}] \"{memberName}\". {message}";
#endif

            if(context)
                Debug.unityLogger.Log(logType, (object)message, context);
            else
                Debug.unityLogger.Log(logType, message);
        }

        private static string GetPrefixColor(string channel)
        {
            if(string.IsNullOrEmpty(channel))
                return null;

            if(CachedColors.TryGetValue(channel, out var existingColor))
                return existingColor;

            var hash = channel.GetHashCode();
            var uHash = unchecked((uint)hash);
            const uint max = 0xFFFFFF;
            var hue = (uHash & max) / (float)max;
            var color = Color.HSVToRGB(hue, Settings.saturation, Settings.value);
            var colorString = ColorUtility.ToHtmlStringRGB(color);

            CachedColors.Add(channel, colorString);
            return colorString;
        }
    }
}