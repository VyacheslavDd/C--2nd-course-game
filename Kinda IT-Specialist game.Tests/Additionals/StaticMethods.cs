using Microsoft.Xna.Framework;
using System.Reflection;

namespace Game2DTests.Additionals;

public static class StaticMethods
{
    public static object GetValue(string fieldName, object obj)
    {
        return obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
    }

    public static void SetValue(string fieldName, object obj, object value)
    {
        obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(obj, value);
    }

    public static void InvokeMethod(string methodName, object obj, object[] args)
    {
        obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, args);
    }

    public static void InitializeGame(object game)
    {
        InvokeMethod("Initialize", game, new object[] { });
        InvokeMethod("LoadContent", game, new object[] { });
    }
}
