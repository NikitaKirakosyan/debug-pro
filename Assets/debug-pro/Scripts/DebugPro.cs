using System.Collections.Concurrent;
using UnityEngine;

namespace NKLogger
{
    public static class DebugPro
    {
#if UNITY_EDITOR
        private static ConcurrentDictionary<string, string> CachedColors = new ();
        private static DebugProSettings _settings;

        private static DebugProSettings Settings => _settings ??= Resources.Load<DebugProSettings>("DebugProSettings");
#endif

        public static void Log(object message,
            object caller = null,
            string prefix = null,
            Object context = null,
            bool editorOnly = false,
            Color colorText = default,
            [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            Log(LogType.Log, message, caller, prefix, context, editorOnly, colorText, callerMemberName);
        }

        public static void LogWarning(object message,
            object caller = null,
            string prefix = null,
            Object context = null,
            bool editorOnly = false,
            Color colorText = default,
            [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            Log(LogType.Warning, message, caller, prefix, context, editorOnly, colorText, callerMemberName);
        }

        public static void LogError(object message,
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
            if(CachedColors == null)
                CachedColors = new ConcurrentDictionary<string, string>();
            else
                CachedColors.Clear();
        }


        private static void Log(LogType logType,
            object message,
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
            message = $"[{prefixResult}] \"{callerMemberName}\". {message}";
#endif

            if(context)
                Debug.unityLogger.Log(logType, (object)message, context);
            else
                Debug.unityLogger.Log(logType, message);
        }

        private static string ComputeColor(string prefix)
        {
            var hash = prefix.GetHashCode();
            var uHash = unchecked((uint)hash);
            const uint max = 0xFFFFFF;
            var hue = (uHash & max) / (float)max;
            var color = Color.HSVToRGB(hue, Settings.saturation, Settings.value);
            return ColorUtility.ToHtmlStringRGB(color);
        }

        private static string GetPrefixColor(string prefix)
        {
            if(string.IsNullOrEmpty(prefix))
                return null;

            return CachedColors.GetOrAdd(prefix, ComputeColor(prefix));
        }
    }
}