using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTab : MonoBehaviour
{
    //to change scene in scenes with game manager
    public void MoveToTab(int sceneID)
    {
        switch (sceneID)
        {
            case 0:
                GameManager.Instance.ChangeScene(GameScene.Arcade);
                break;
            case 1:
                GameManager.Instance.ChangeScene(GameScene.CyberpunkCity);
                break;
            case 2:
                GameManager.Instance.ChangeScene(GameScene.MushroomGarden);
                break;
        }
    }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Exploration:
                GetComponent<Button>().interactable = true;
                break;
            case GameState.Interaction:
                GetComponent<Button>().interactable = false;
                break;
        }
    }
}
