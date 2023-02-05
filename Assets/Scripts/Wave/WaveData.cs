using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wave
{
    public abstract class WaveData : MonoBehaviour, WaveCondition
    {
        public int[] enemiesIds = new int[0];
        public char[] spawnIds = new char[0];

        [SerializeField]
        protected int aliveEnemies = int.MaxValue;

        private HashSet<SpawnSource> allSpawners;

        private List<SpawnSource> currentSpawner;

        public SpawnManager manager;

        public SpawnManager Manager { set { manager = value; } }

        public virtual void Start()
        {
            if(manager == null)
            {
                Debug.LogWarning("No SpawnManager make me a child of SpawnManager");
                this.enabled = false;
                return;
            }
            allSpawners = manager.getGroup(spawnIds);
        }

        protected SpawnSource GetRandomSpawner()
        {
            if(currentSpawner == null || currentSpawner.Count == 0)
            {
                currentSpawner = new List<SpawnSource>(allSpawners);
            }
            int i = Random.Range(0, currentSpawner.Count);
            SpawnSource s = currentSpawner[i];
            currentSpawner.RemoveAt(i);
            return s;
        }

        public virtual void StartWave()
        {
            aliveEnemies = 0;
            foreach (int id in enemiesIds)
            {
                SpawnSource ss = GetRandomSpawner();
                GameObject g =  manager.data.enemyList[id];
                GameObject enemy = ss.Spawn(g);
                aliveEnemies++;
                KillAbleEnemy kae = enemy.GetComponent<KillAbleEnemy>();
                if (kae != null)
                {
                    kae.OnDying += OnEnemyKilled;
                }
            }
        }

        public void OnEnemyKilled()
        {
            aliveEnemies--;
        }

        public virtual void UpdateWave()
        {

        }

        public abstract bool WaveFinished();
    }
}