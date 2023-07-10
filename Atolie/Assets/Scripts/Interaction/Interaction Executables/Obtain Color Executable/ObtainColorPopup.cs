using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainColorPopup : MonoBehaviour
{
    public static ObtainColorPopup Instance;

    [SerializeField] private List<Sprite> colorImages;

    private ColorManager.Color color;

    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private Text colorName;
    [SerializeField] private Image colorImage;

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
    }

    public void updatePopup(ColorManager.Color colorObtained)
    {
        color = colorObtained;
        colorName.text = Enum.GetName(typeof(ColorManager.Color), colorObtained);
        colorImage.sprite = colorImages[(int)colorObtained];
    }

    public void ShowPopup()
    {
        popupCanvas.SetActive(true);
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
    }

    public void CloseWindow()
    {
        HidePopup();
        InteractionExecutable.currentExecutableCompleted();
    }
}
