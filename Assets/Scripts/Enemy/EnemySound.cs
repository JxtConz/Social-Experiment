using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemySound
{
    public enum SoundType
    {
        RootDie,
        RootHurt,
        RootAttack
    }

    void MakeSound(SoundType sound);
}
