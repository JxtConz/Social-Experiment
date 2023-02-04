using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour, EnemySound
{
    public void MakeSound(EnemySound.SoundType sound)
    {
        Debug.Log("play " + sound);
    }

    
}
