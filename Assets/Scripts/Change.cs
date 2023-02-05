using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    void Start()
    {

        StartCoroutine(loadSceneAfterDelay(5));

    }
    IEnumerator loadSceneAfterDelay(float waitbySecs)
    {

        yield return new WaitForSeconds(waitbySecs);
        SceneManager.LoadScene(4);
    }
}
