using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptWaveSystem : MonoBehaviour
{

    public event EventHandler OnEnded;

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    private enum State
    {
        Idle,
        Active,
        Over,
    }

    [SerializeField] private TriggerArea scriptTrigger;

    private State state;

    [SerializeField] private Wave[] waveArray;

    private void Awake()
    {
        state = State.Idle;
    }

    private void Start()
    {
        scriptTrigger.OnPlayerTrigger += Trigger_PlayerTrigger;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Active:
                foreach (Wave wave in waveArray)
                {
                    wave.Update();
                }

                TestFightOver();
                break;
        }
    }

    private void TestFightOver()
    {
        if (state == State.Active)
        {
            if (AllWaveOver())
            {
                //Battle is over
                if(ScriptTimer.instance.currentTime > 0)
                {
                    state = State.Over;
                    Debug.Log("VICTORY!!");
                    ScriptTimer.instance.currentTime = 0;
                    OnEnded?.Invoke(this, EventArgs.Empty);

                    SceneManager.LoadScene(2);
                }
                /*state = State.Over;
                Debug.Log("VICTORY!!");
                ScriptTimer.instance.currentTime = 0;
                OnEnded?.Invoke(this, EventArgs.Empty);*/
            }
            /*else if(ScriptTimer.instance.currentTime <= 0)
            {
                ScriptTimer.instance.currentTime = 0;
                Time.timeScale = 0f;
                Debug.Log("DEFEAT");
            }*/
        }
    }

    private bool AllWaveOver()
    {
        foreach (Wave wave in waveArray)
        {
            if (wave.IsWaveOver())
            {
                //Wave is over
            }
            else
            {
                //Wave not over
                if (ScriptTimer.instance.currentTime <= 0)
                {
                    ScriptTimer.instance.currentTime = 0;
                    Time.timeScale = 0f;
                    Debug.Log("DEFEAT");
                }
                return false;
            }
        }

        return true;
    }

    private void Trigger_PlayerTrigger(object sender, System.EventArgs e)
    {
        if (state == State.Idle)
        {
            StartFirstWave();
            scriptTrigger.OnPlayerTrigger -= Trigger_PlayerTrigger;
        }
    }

    private void StartFirstWave()
    {
        Debug.Log("StartWave!!!");
        state = State.Active;
    }

    [System.Serializable]
    private class Wave
    {
        [SerializeField] private ScriptSpawnController[] spawnController;
        [SerializeField] private float timer;

        public void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            foreach (ScriptSpawnController enemySpawn in spawnController)
            {
                enemySpawn.SpawnEnemy();
            }
        }

        public bool IsWaveOver()
        {
            if (timer < 0)
            {
                //spawned
                foreach (ScriptSpawnController enemySpawn in spawnController)
                {
                    if (enemySpawn.IsAlive())
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                //haven't spawned yet
                return false;
            }
        }
    }
}
