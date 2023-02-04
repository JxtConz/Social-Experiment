using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ScriptPlayerSound
{
    public enum SoundType
    {
        PlayerAttack, PlayerHurt, PlayerDie, Player360Attack
    }

    void MakeSound(SoundType sound);
}
