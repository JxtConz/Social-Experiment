using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class MakeEnemySound : MonoBehaviour, EnemySound 
{
    public AudioSource audioSource;

    public AudioClip attack;
    public AudioClip dead;
    public AudioClip getHit;

    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void MakeSound(EnemySound.SoundType sound)
    {

        switch (sound)
        {
            case EnemySound.SoundType.RootDie: audioSource.clip = dead; audioSource.Play(); break;
            case EnemySound.SoundType.RootHurt: audioSource.clip = getHit; audioSource.Play(); break;
            case EnemySound.SoundType.RootAttack: audioSource.clip = attack; audioSource.Play(); break;
        }
    }
}
