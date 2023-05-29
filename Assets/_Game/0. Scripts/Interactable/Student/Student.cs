using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
/// <summary>
/// - phase: In which Student is.
/// </summary>

public class Student : MonoBehaviour
{
    public UniversityPhase phase = UniversityPhase.Enrollment;
    [SerializeField] CapsuleCollider capsuleCollider;

    public MeshRenderer meshRenderer;
    public StudentData data;

    public StudentAnimator animator;
    public StudentMovement movement;

    internal Vector3 startPos;
    internal Vector3 doorPos;

    internal EnrollmentTable enrollmentTable;

    public bool isReadyToChangePhase;

    public static event Action<Student> OnPhaseChanged;
    [Button("Get Component References")]
    private void GetComponentReferences()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        animator = GetComponentInChildren<StudentAnimator>();
        movement = GetComponent<StudentMovement>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Awake()
    {
        startPos = transform.localPosition;
        StudentData.SetOutline(0, this);
    }
    private void OnEnable()
    {
        RadialProgressBar.OnProgressComplete += MakeReadyForNextPhase;
    }
    private void OnDisable()
    {
        RadialProgressBar.OnProgressComplete -= MakeReadyForNextPhase;
    }
    private void OnMouseDown()
    {
        if (isReadyToChangePhase)
        {
            phase = GetNextPhase();
            transform.parent = null;
            animator.Walk();
            movement.MoveToDestination(this, doorPos);
            //float delay = (doorPos - transform.position).sqrMagnitude/50;
            //delay = Mathf.Clamp(delay, 3, 15);
            //DOVirtual.DelayedCall(delay, () => OnPhaseChanged?.Invoke(this));
            enrollmentTable.boxCollider.enabled = true;
        }
        else
        {
            Actions.OnStudentSelection?.Invoke(this);
            capsuleCollider.enabled = false;
        }
    }
    private void MakeReadyForNextPhase(Student obj)
    {
        if (obj == this)
        {
            StudentData.SetOutline(60, this);
            capsuleCollider.enabled = true;
            isReadyToChangePhase = true;
            //data.UpdateStudentSkin(GetNextPhase(), obj);
        }
    }
    UniversityPhase GetNextPhase()
    {
        int _phase = (int)phase;
        _phase++;

        return (UniversityPhase)_phase;
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
