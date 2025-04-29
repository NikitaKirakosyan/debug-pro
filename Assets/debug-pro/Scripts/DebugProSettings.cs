using UnityEngine;

namespace NKLogger
{
    public class DebugProSettings : ScriptableObject
    {
        public const string PrefixDefault = nameof(DebugPro);
        public const float SaturationDefault = 0.6f;
        public const float ValueDefault = 0.8f;
        
        public string prefix = "DebugPro";
        public float saturation = 0.6f;
        public float value = 0.8f;
        public bool isFullColorized;

        
        public void ResetValues()
        {
            prefix = PrefixDefault;
            saturation = SaturationDefault;
            value = ValueDefault;
            isFullColorized = false;
        }
    }
}