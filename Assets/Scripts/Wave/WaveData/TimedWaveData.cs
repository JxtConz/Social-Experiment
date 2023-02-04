using System.Collections;
using UnityEngine;

namespace Wave
{
    public class TimedWaveData : WaveData
    {
        public float waveTime = 5f;

        private float endTime;
        private bool waveFinished = false;
        public override void StartWave()
        {
            base.StartWave();
            endTime = Time.time + waveTime;
            Debug.Log("Start " + Time.time + " end " + endTime);
        }

        public override void UpdateWave()
        {
            if (Time.time > endTime)
                waveFinished = true;
        }

        public override bool WaveFinished()
        {
            return waveFinished;
        }
    }
}