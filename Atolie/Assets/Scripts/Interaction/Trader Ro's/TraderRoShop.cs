using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderRoShop : MonoBehaviour
{
    public static TraderRoShop instance;

    [SerializeField] private TraderRoListener listener;
    [SerializeField] private ShopDialogueManager shopDialogueManager;
    [SerializeField] private TradeItemPopup tradeBox;
    [SerializeField] private Button tradeButton;
    [SerializeField] private Button talkButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private List<Dialogue> allTalkDialogue;
    private int currentTalkDialogueIndex;

    [SerializeField] private List<Dialogue> allTradeItemDialogue;
    [SerializeField] private List<Item> allObtainableItems;

    [SerializeField] private Dialogue firstTimeWelcomeDialogue;
    [SerializeField] private Dialogue subsequentWelcomeDialogue;
    [SerializeField] private Dialogue tradeItemDialogue;
    [SerializeField] private Dialogue exitDialogue;

    [SerializeField] private Mode currentMode;

    [SerializeField] private Item itemToBeObtained;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        itemToBeObtained = null;
        currentTalkDialogueIndex = listener.currentTalkDialogueIndex;
        shopDialogueManager = ShopDialogueManager.instance;
    }

    private void Start()
    {
        UpdateShopMode(currentMode);

    }

    public void TradeItem(Item item)
    {
        if (item != null)
        {
            UpdateShopMode(Mode.Trading);
            switch (item.name)
            {
                case "Tamagotchi":
                    InventoryManager.instance.GetSelectedItem(true);
                    itemToBeObtained = allObtainableItems[0];
                    shopDialogueManager.StartDialogue(allTradeItemDialogue[0]);
                    break;
                case "Dog Leg":
                    InventoryManager.instance.GetSelectedItem(true);
                    itemToBeObtained = allObtainableItems[1];
                    shopDialogueManager.StartDialogue(allTradeItemDialogue[1]);
                    break;
                default:
                    shopDialogueManager.StartDialogue(allTradeItemDialogue[2]);
                    break;
            }
        }
    }

    public void UpdateShopMode(Mode mode)
    {
        currentMode = mode;

        switch (mode)
        {
            case Mode.Welcome:
                tradeBox.HidePopup();
                HandleWelcome();
                break;
            case Mode.Trade:
                itemToBeObtained = null;
                tradeBox.ShowPopup();
                EnableAllButtons();
                tradeBox.EnableSubmission();
                shopDialogueManager.StartStubDialogue(tradeItemDialogue);
                break;
            case Mode.Trading:
                DisableAllButtons();
                tradeBox.DisableSubmission();
                break;
            case Mode.Talk:
                tradeBox.HidePopup();
                EnableAllButtons();
                break;
            case Mode.Talking:
                tradeBox.HidePopup();
                DisableAllButtons();
                shopDialogueManager.StartDialogue(allTalkDialogue[currentTalkDialogueIndex]);
                break;
            case Mode.Exit:
                tradeBox.HidePopup();
                DisableAllButtons();
                HandleExit();
                break;
        }
    }

    private void HandleWelcome()
    {
        if (listener.firstTimeEntering)
        {
            DisableAllButtons();
            listener.firstTimeEntering = false;
            shopDialogueManager.StartDialogue(firstTimeWelcomeDialogue);
            return;
        }
        EnableAllButtons();
        shopDialogueManager.StartStubDialogue(subsequentWelcomeDialogue);
    }

    private void HandleExit()
    {
        shopDialogueManager.StartStubDialogue(exitDialogue);
        Invoke("ExitShop", 3);
    }

    private void ExitShop()
    {
        GameManager.Instance.ChangeScene(GameScene.CyberpunkCity);
    }

    public void DialogueCompleted()
    {
        switch (currentMode)
        {
            case Mode.Welcome:
                EnterTalkMode(firstTimeWelcomeDialogue);
                break;
            case Mode.Talking:
                EnterTalkMode(allTalkDialogue[currentTalkDialogueIndex]);
                break;
            case Mode.Trading:
                if (itemToBeObtained != null)
                {
                    InventoryManager.instance.AddItem(itemToBeObtained);
                }
                UpdateShopMode(Mode.Trade);
                break;
        }
    }

    private void EnterTalkMode(Dialogue dialogue)
    {
        UpdateShopMode(Mode.Talk);
        shopDialogueManager.DisplayLastSentence(dialogue);
    }

    public void EnterTradeMode()
    {
        UpdateShopMode(Mode.Trade);
    }

    public void EnterTalkingMode()
    {
        UpdateShopMode(Mode.Talking);
    }

    public void EnterExitMode()
    {
        UpdateShopMode(Mode.Exit);
    }

    private void EnableAllButtons()
    {
        tradeButton.interactable = true;
        talkButton.interactable = true;
        exitButton.interactable = true;
    }

    private void DisableAllButtons()
    {
        tradeButton.interactable = false;
        talkButton.interactable = false;
        exitButton.interactable = false;
    }

    public enum Mode
    {
        Welcome,
        Trade,
        Trading,
        Talk,
        Talking,
        Exit
    }
}
