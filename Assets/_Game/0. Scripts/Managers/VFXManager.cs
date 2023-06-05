using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [BoxGroup("Table")]
    public UnlockTableVFX unlockTable;
    [BoxGroup("On Student Passout")]
    public CoinsVFX coins;

    #region Unity Callbacks
    private void Start()
    {
        coins.currentIndex = 0;
        unlockTable.currentIndex = 0;
    }
    private void OnEnable()
    {
        UnlockableItem.OnUnlockItem += unlockTable.Play_Paricles;
        Actions.OnStudentCeremony += coins.Play_Paricles;
    }
    private void OnDisable()
    {
        UnlockableItem.OnUnlockItem -= unlockTable.Play_Paricles;
        Actions.OnStudentCeremony -= coins.Play_Paricles;
    }
    #endregion
}
[Serializable]
public struct UnlockTableVFX
{
    public List<ParticleSystem> _particles;
    [ReadOnly]
    public int currentIndex;
    public void Play_Paricles(Vector3 contactPos)
    {
        if (_particles[currentIndex] != null)
        {
            if (currentIndex < _particles.Count)
            {
                _particles[currentIndex].transform.position = contactPos;

                _particles[currentIndex].Play();
            }
            else
            {
                currentIndex = 0;
                _particles[currentIndex].transform.position = contactPos;
                _particles[currentIndex].Play();
            }
            currentIndex++;
        }
    }
}
/// <summary>
/// The UnlockTableVFX And CoinsVFX Is Something Like Rewritting Code But 
/// I Did It Intentionally Because It's Struct And I Do It For My Own Convinence.
/// Like I Can Change Play_Particles WithOut Worring Too Much
/// </summary>
[Serializable]
public struct CoinsVFX
{
    public List<ParticleSystem> _particles;
    [ReadOnly]
    public int currentIndex;
    public void Play_Paricles(Student student, int num)
    {
        var contactPos = student.transform.position;
        if (_particles[currentIndex] != null)
        {
            if (currentIndex < _particles.Count)
            {
                _particles[currentIndex].transform.position = contactPos;

                _particles[currentIndex].Play();
            }
            else
            {
                currentIndex = 0;
                _particles[currentIndex].transform.position = contactPos;
                _particles[currentIndex].Play();
            }
            currentIndex++;
        }
    }
}