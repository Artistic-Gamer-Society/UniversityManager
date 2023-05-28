using DG.Tweening;
using System;
using UnityEngine;
/// <summary>
/// - Student Has 4 Phases {None -> Enrollment -> Examination -> Passout -> None}
/// </summary>
public enum StudentPhase
{
    Enrollment = 0,
    Examination = 1,
    Passout = 2
}
public class Student : MonoBehaviour, IInteractable
{
    [SerializeField]
    StudentPhase phase;
    [SerializeField]
    CapsuleCollider capsuleCollider;
    public MeshRenderer meshRenderer;

    [SerializeField]
    StudentData data;

    internal Vector3 startPos;
    bool isReadyToChangePhase;
    private void Awake()
    {
        startPos = transform.localPosition;
        data.SetOutline(0, this);
    }
    private void OnEnable()
    {
        RadialProgressBar.OnProgressComplete += MakeReadyForNextPhase;
    }



    private void OnDisable()
    {
        RadialProgressBar.OnProgressComplete -= MakeReadyForNextPhase;
    }
    /// <summary>
    /// - Check If The Student Is Selected
    /// </summary>
    private void OnMouseDown()
    {
        if (isReadyToChangePhase)
        {
            transform.parent = null;
            transform.MoveTowardsUI(UIManager.GetInstance().roomButton.doorWorldTransform, 2);
        }
        else
        {
            Actions.OnStudentSelection?.Invoke(this);
            capsuleCollider.enabled = false;
        }
    }
    private void OnMouseUp()
    {

    }
    private void MakeReadyForNextPhase(Student obj)
    {
        if (obj == this)
        {
            data.SetOutline(60, this);
            capsuleCollider.enabled = true;
            isReadyToChangePhase = true;
            //data.UpdateStudentSkin(GetNextPhase(), obj);
        }
    }
    StudentPhase GetNextPhase()
    {
        int _phase = (int)phase;
        _phase++;

        return (StudentPhase)_phase;
    }
    void MoveToNextRoom()
    {

    }
    public void ResetStudentDefaultState()
    {
        transform.localPosition = startPos;
        capsuleCollider.enabled = true;
    }
}
