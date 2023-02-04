using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTimer : MonoBehaviour
{
    public static ScriptTimer instance;

    public float currentTime = 0f;
    public float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0f;
        }
    }
}
