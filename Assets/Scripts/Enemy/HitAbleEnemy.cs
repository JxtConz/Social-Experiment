using System.Collections;
using UnityEngine;

namespace Enemy
{
    public interface HitAbleEnemy
    {
        bool HitEnemy(int damage);
    }
}