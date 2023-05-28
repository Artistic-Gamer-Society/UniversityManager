using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
 
}
[Serializable]
public struct QueuseStruct
{
    public Ease movementMergingEase;
    public Ease rearrangingEase;
    public ParticleType particleType;
    public enum ParticleType
    {
        Puff = 0,
        Shine = 1,
    }
    public enum QueueArrangement
    {
        DestroyTowardsDown = 0,
        DestroyTowardsUp = 2,
    }
}
