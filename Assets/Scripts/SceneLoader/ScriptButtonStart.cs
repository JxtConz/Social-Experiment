using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonStart : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(3);
    }
}
