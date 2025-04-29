using UnityEditor;
using UnityEngine;

namespace NKLogger.Editor
{
    public class DebugProEditorWindow : EditorWindow
    {
        private const float Width = 512;
        private const float Height = 256;

        private DebugProSettings _debugProSettings;

        private DebugProSettings DebugProSettings => _debugProSettings ??= Resources.Load<DebugProSettings>("DebugProSettings");


        [MenuItem("Window/Debug Pro Settings")]
        public static void ShowWindow()
        {
            var window = GetWindow<DebugProEditorWindow>();
            window.titleContent = new GUIContent("Debug Pro Settings");
            window.position = new Rect((Screen.width - Width) / 2, (Screen.height - Height) / 2, Width, Height);
        }


        private void OnGUI()
        {
            DebugProSettings.prefix = EditorGUILayout.TextField("Default Prefix", DebugProSettings.prefix);
            DebugProSettings.saturation = EditorGUILayout.Slider("Saturation", DebugProSettings.saturation, 0, 255);
            DebugProSettings.value = EditorGUILayout.Slider("Value", DebugProSettings.value, 0, 255);
            DebugProSettings.isFullColorized = EditorGUILayout.Toggle("Is Full Colorized", DebugProSettings.isFullColorized);

            GUI.enabled = false;
            _debugProSettings = EditorGUILayout.ObjectField("Debug Pro Settings asset", _debugProSettings, typeof(DebugProSettings), false) as DebugProSettings;
            GUI.enabled = true;

            if(GUILayout.Button("Reset"))
                DebugProSettings.ResetValues();

            DebugPro.Reset();
        }
    }
}