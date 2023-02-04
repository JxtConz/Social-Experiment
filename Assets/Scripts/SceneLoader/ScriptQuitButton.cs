using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScriptQuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game is exiting");
    }
}
