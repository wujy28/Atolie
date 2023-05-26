using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTab : MonoBehaviour
{
    //to change scene
    public void MoveToTab(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
