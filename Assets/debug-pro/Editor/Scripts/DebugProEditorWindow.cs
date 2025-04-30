using UnityEditor;
using UnityEngine;

namespace NKLogger.Editor
{
    public class DebugProEditorWindow : EditorWindow
    {
        private const float Width = 512;
        private const float Height = 256;

        private bool _isAutoSaveEnabled;
        private DebugProSettings _debugProSettings;

        private DebugProSettings DebugProSettings => _debugProSettings ??= Resources.Load<DebugProSettings>("DebugProSettings");


        [MenuItem("Window/Debug Pro Settings")]
        public static void ShowWindow()
        {
            var window = GetWindow<DebugProEditorWindow>();
            window.titleContent = new GUIContent("Debug Pro Settings");
            window.position = new Rect((Screen.width - Width) / 2, (Screen.height - Height) / 2, Width, Height);
        }


        private void OnDestroy()
        {
            if(_isAutoSaveEnabled)
                Save();
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            DebugProSettings.prefix = EditorGUILayout.TextField("Default Prefix", DebugProSettings.prefix);
            DebugProSettings.saturation = EditorGUILayout.Slider("Saturation", DebugProSettings.saturation, 0, 255);
            DebugProSettings.value = EditorGUILayout.Slider("Value", DebugProSettings.value, 0, 255);
            DebugProSettings.isFullColorized = EditorGUILayout.Toggle("Is Full Colorized", DebugProSettings.isFullColorized);
            _isAutoSaveEnabled = EditorGUILayout.Toggle("Auto save", _isAutoSaveEnabled);

            GUI.enabled = false;
            _debugProSettings = EditorGUILayout.ObjectField("Debug Pro Settings asset", _debugProSettings, typeof(DebugProSettings), false) as DebugProSettings;
            GUI.enabled = true;

            if(GUILayout.Button("Reset"))
                DebugProSettings.ResetValues();

            DebugPro.Reset();

            if(_isAutoSaveEnabled && EditorGUI.EndChangeCheck())
                Save();
            else if(GUILayout.Button("Save"))
                Save();
        }

        private void Save()
        {
            EditorUtility.SetDirty(DebugProSettings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}