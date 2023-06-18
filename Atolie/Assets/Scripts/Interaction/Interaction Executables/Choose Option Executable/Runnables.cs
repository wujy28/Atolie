using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnables : MonoBehaviour
{
    public static Runnables Instance;

    private Dictionary<string, Runnable> runnables;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        runnables = new Dictionary<string, Runnable>();
    }

    public void AddToRunnables(string name, Runnable runnable)
    {
        runnables.Add(name, runnable);
    }

    public void RemoveFromRunnables(string name)
    {
        runnables.Remove(name);
    }

    public Runnable GetRunnable(string name)
    {
        return runnables.GetValueOrDefault(name);
    }

    public int RunnablesSize()
    {
        return runnables.Count;
    }
}
