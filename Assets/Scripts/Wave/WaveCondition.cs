using System.Collections;
using UnityEngine;

namespace Wave
{

    public interface WaveCondition
    {

        public SpawnManager Manager
        {
            set;
        }

        public Transform transform
        {
            get;
        }

        public string name
        {
            get;
        }

        bool WaveFinished();

        void UpdateWave();

        void StartWave();
    }

}