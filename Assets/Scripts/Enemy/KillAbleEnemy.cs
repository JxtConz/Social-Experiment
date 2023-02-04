using System.Collections;
using UnityEngine;

namespace Enemy
{

    public delegate void Dying();
    public interface KillAbleEnemy
    {
        event Dying OnDying;
    }
}