using UnityEngine;

namespace NKLogger.Demo
{
    public class DebugProDemo : MonoBehaviour
    {
        private void Reset()
        {
            Debug.Log("Default Log");
            Debug.LogWarning("Default Log Warning");
            Debug.LogError("Default Log Error");
            DebugPro.Log("This is test debug by DebugPro", this, null, gameObject);
            DebugPro.Log(123, this, null, gameObject);

            var demoClass = new DemoClass();
            DebugPro.Log(demoClass, this, null, gameObject);
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
            DebugPro.LogError("Log Error from DemoStaticClass with cyan color by argument", prefix: nameof(DemoStaticClass), colorText: Color.cyan);
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