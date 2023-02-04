using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wave { 
    public class SpawnSource : MonoBehaviour
    {
        public char[] goups = new char[0];

        public float lastTimeSpawn;

        public GameObject Spawn(GameObject prefab)
        {
            lastTimeSpawn = Time.time;
            return Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}