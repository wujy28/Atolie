using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
