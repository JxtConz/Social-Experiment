using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RootDelegate : MonoBehaviour, HitAbleEnemy
    {
        public HitAbleEnemy owner;

        public bool HitEnemy(int damage)
        {
            return owner.HitEnemy(damage);
        }
    }
}