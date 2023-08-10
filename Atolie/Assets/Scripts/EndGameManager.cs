using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance;

    [SerializeField] private GameObject canvas;

    private bool[] conditionsForEndGame;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            canvas.SetActive(false);
            InitiateConditionsForEndGame();
            InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Dog Tag":
                MeetConditionForEndGame(0);
                break;
            case "Truffle":
                MeetConditionForEndGame(1);
                break;
            case "Winding Key":
                MeetConditionForEndGame(2);
                break;
        }
    }

    private void OnDisable()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
    }

    private void OnDestroy()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
    }

    private void InitiateConditionsForEndGame()
    {
        conditionsForEndGame = new bool[3];
        for (int i = 0; i < conditionsForEndGame.Length; i++)
        {
            conditionsForEndGame[i] = false;
        }
    }

    private void MeetConditionForEndGame(int index)
    {
        conditionsForEndGame[index] = true;
        CheckIfAllConditionsAreMet(conditionsForEndGame);
    }

    private void CheckIfAllConditionsAreMet(bool[] condition)
    {
        foreach (bool met in condition)
        {
            if (!met)
            {
                return;
            }
        }
        AllConditionsMet(condition);
    }

    private void AllConditionsMet(bool[] condition)
    {
        if (condition == conditionsForEndGame)
        {
            canvas.SetActive(true);
        }
    }

    public void EndGame()
    {
        canvas.SetActive(false);
        GameManager.Instance.ChangeScene(GameScene.DemoEnd);
        GameManager.Instance.UpdateGameState(GameState.Finish);
    }

}
