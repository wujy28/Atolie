using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //to change scene in scenes without game manager
    public void MoveToTab(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
