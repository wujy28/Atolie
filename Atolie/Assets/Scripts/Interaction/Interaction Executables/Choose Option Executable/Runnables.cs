using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains a dictionary of all created runnables for easy access by game objects (for choose option IE)
public class Runnables : MonoBehaviour
{
    private static Dictionary<string, Runnable> runnables = new Dictionary<string, Runnable>();

    public static void AddToRunnables(string name, Runnable runnable)
    {
        runnables.Add(name, runnable);
    }

    public static void RemoveFromRunnables(string name)
    {
        runnables.Remove(name);
    }

    public static Runnable GetRunnable(string name)
    {
        return runnables.GetValueOrDefault(name);
    }

    public static int RunnablesSize()
    {
        return runnables.Count;
    }
}
