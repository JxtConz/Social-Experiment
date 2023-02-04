using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{

    public event EventHandler OnPlayerTrigger;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ScriptPlayerController player = collider.GetComponent<ScriptPlayerController>();

        if (player != null)
        {
            OnPlayerTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
