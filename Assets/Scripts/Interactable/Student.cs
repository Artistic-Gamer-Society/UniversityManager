using System;
using UnityEngine;
/// <summary>
/// - Student Has 4 Phases {None -> Enrollment -> Examination -> Passout -> None}
/// </summary>
public enum StudentPhase
{
    None = 0,
    Enrollment = 1,
    Examination = 2,
    Passout = 3
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
    private void Awake()
    {        
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
        Actions.OnStudentSelection?.Invoke(this);
        capsuleCollider.enabled = false;
        gameObject.transform.parent = null;
    }
    private void OnMouseUp()
    {

    }
    private void MakeReadyForNextPhase(Student obj)
    {
        data.SetOutline(60, this);
    }
}
