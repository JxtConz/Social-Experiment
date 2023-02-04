using System.Collections;
using UnityEngine;

namespace Wave
{
    public class NoOneLeftWave : WaveData
    {
        public override bool WaveFinished()
        {
            return base.aliveEnemies == 0;
        }
    }
}