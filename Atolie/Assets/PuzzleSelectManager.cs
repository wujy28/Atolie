using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleSelectManager : MonoBehaviour
{
    private static int puzzleSelectSceneIndex = 0;

    public void GoToPuzzleSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(puzzleSelectSceneIndex);
    }
}
