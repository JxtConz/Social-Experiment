using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wave;

public class ScriptFinishWave : MonoBehaviour, Finishable
{

    public int sceneIndex = -1;

    public void Finished()
    {
        end();
    }


    // Update is called once per frame
    void end()
    {
        if(sceneIndex == -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
