using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainItemPopup : MonoBehaviour
{
    public static ObtainItemPopup Instance;

    private Item item;

    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;

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

    public void updatePopup(Item itemObtained)
    {
        item = itemObtained;
        itemName.text = item.name;
        itemImage.sprite = item.image;
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
