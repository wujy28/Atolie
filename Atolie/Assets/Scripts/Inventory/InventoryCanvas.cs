using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    public static InventoryCanvas instance;

    public Canvas canvas;

    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        canvas.worldCamera = Camera.main;
    }
}
