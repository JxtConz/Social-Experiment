using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wave
{
    public class NoOneLeftDelayWave : WaveData
    {

        public float dealy = 1f;

        public int currentEnemyId = 0;

        private float curretDelayEnd;

        private void SpawnEnemy()
        {
            if (currentEnemyId < enemiesIds.Length)
                SpawnEnemy(manager.data.enemyList[enemiesIds[currentEnemyId++]]);
        }
        private void SpawnEnemy(GameObject prefab)
        {
            SpawnSource ss = GetRandomSpawner();
            GameObject enemy = ss.Spawn(prefab);
            KillAbleEnemy kae = enemy.GetComponent<KillAbleEnemy>();
            if (kae != null)
            {
                kae.OnDying += OnEnemyKilled;
            }
        }

        public override void StartWave()
        {
            aliveEnemies = base.enemiesIds.Length;
            SpawnEnemy();
            curretDelayEnd = Time.time + dealy;
        }

        public override void UpdateWave()
        {
            if(!WaveFinished() && Time.time > curretDelayEnd)
            {
                SpawnEnemy();
                curretDelayEnd = Time.time + dealy;
            }
        }

        public override bool WaveFinished()
        {
            return base.aliveEnemies == 0;
        }
    }
}
