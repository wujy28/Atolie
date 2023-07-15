using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public GameObject[] paletteSlots;
    public int currColor = 0;

    [SerializeField] private SetDisplayColor displayColor;

    public Color selectedColor;

    public static event Action<String> OnSelectedColorChanged;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            selectedColor = Color.noColor;
        }
    }

    public void AddColor()
    {
        GameObject slot = paletteSlots[currColor];

        if (slot.transform.childCount > 0)
        {
            GameObject color = slot.transform.GetChild(0).gameObject;
            color.SetActive(true);
            currColor++;
        }
    }

    public void ChangeSelectedColor(int color)
    {
        selectedColor = (Color)color;
        displayColor.changeCurrentColor(color);
        OnSelectedColorChanged?.Invoke(Enum.GetName(typeof(Color), selectedColor));
    }

    public string GetSelectedColorAsString()
    {
        return Enum.GetName(typeof(Color), selectedColor);
    }

    public enum Color
    {
        noColor,
        Purple,
        Pink,
        Blue
    }
}
