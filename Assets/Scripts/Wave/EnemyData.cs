using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wave
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/ScriptableObject", order = 1)]
    public class EnemyData : ScriptableObject
    {
        public GameObject[] enemyList = new GameObject[0];
    }
}