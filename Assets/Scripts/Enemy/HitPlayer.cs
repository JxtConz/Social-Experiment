using System.Collections;
using UnityEngine;



public interface HitPlayer
{
    public enum HitType
    {
        RootTip,
        RootBranch
    }

    void HitPlayer(GameObject source, int amount, HitType hit);
}