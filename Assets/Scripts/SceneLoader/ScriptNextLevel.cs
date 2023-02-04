using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptNextLevel : MonoBehaviour
{
    public void NextLevelButton()
    {
        SceneManager.LoadScene(1);
    }
}
