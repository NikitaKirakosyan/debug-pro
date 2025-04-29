using UnityEngine;

namespace NKLogger.Demo
{
    public class DebugProDemo : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Default Log");
            Debug.LogWarning("Default Log Warning");
            Debug.LogError("Default Log Error");
            
            new DemoClass().Foo();
            new DemoStruct().Foo();
            DemoStaticClass.Foo();
            new Player().TakeDamage(30);
            new Lamp().Enable();
        }
    }

    public class DemoClass
    {
        public void Foo()
        {
            DebugPro.Log("Log from DemoClass by DebugPro!", this);
        }
    }

    public struct DemoStruct
    {
        public void Foo()
        {
            DebugPro.LogWarning("Log Warning from DemoStruct by DebugPro!", this);
        }
    }

    public static class DemoStaticClass
    {
        public static void Foo()
        {
            DebugPro.LogError("Log Error from DemoStruct by DemoStaticClass!", prefix: nameof(DemoStaticClass));
        }
    }

    public class Player
    {
        public float health = 100;
        
        public void TakeDamage(float damage)
        {
            var healthBefore = health;
            health -= damage;
            DebugPro.Log($"Health before: {healthBefore}. After: {health}");
        }
    }

    public class Lamp
    {
        public void Enable()
        {
            DebugPro.Log("Lamp enabled", prefix: "Custom Prefix");
        }
    }
}