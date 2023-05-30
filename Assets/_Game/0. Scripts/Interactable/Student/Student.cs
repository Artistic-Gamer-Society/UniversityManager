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

    public SkinnedMeshRenderer skinMeshRenderer;
    public StudentData data;

    public StudentAnimator animator;
    public StudentMovement movement;

    internal Vector3 startPos;
    internal Vector3 doorPos;

    internal Table table;


    public bool isReadyToChangePhase;

    [Button("Get Component References")]
    private void GetComponentReferences()
    {
        skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
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
        Actions.OnStudentSelection?.Invoke(this);
        capsuleCollider.enabled = false;
        movement.navMeshAgent.speed = data.studentSpeed;
        if (isReadyToChangePhase)
        {
            transform.parent = null;
            animator.Walk();
            movement.MoveToDestination(this, doorPos);
            movement.navMeshAgent.speed += data.studentSpeedTowardsDoor;
            table.boxCollider.enabled = true;
        }
    }
    private void MakeReadyForNextPhase(Student obj)
    {
        if (obj == this)
        {
            StudentData.SetOutline(60, this);
            capsuleCollider.enabled = true;
            isReadyToChangePhase = true;
            data.UpdateStudentSkin(GetNextPhase(), obj);
            phase = GetNextPhase();
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
        //st.transform.localPosition = st.startPos;
        movement.enabled = false;
        capsuleCollider.enabled = true;
    }
    //===============Tweens===============
    internal Tween RearrangeAnimation;

    public void StartRearranging(Vector3 targetPosition, float duration)
    {
        if (!isSwitchingLine)
        {
            StopSwitchLineTween();
            StopRearranging();
            RearrangeAnimation = transform.DOLocalMove(targetPosition, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    animator.StopWalking();
                });
        }
    }

    public void StopRearranging()
    {
        if (RearrangeAnimation != null && RearrangeAnimation.IsActive())
        {
            RearrangeAnimation.Kill();
        }
    }
    private Tween SwtichLineAnimation;

   [SerializeField] bool isSwitchingLine;
    public void SwitchLineAnimation(Vector3[] pathPoints, float duration, Student st)
    {
        // Stop the previous tween if it exists
        isSwitchingLine = true;       
        if (isSwitchingLine)
        {

            isReadyToChangePhase = false;
            StopSwitchLineTween();
            StopRearranging();

            SwtichLineAnimation = transform.DOLocalPath(pathPoints, duration, PathType.Linear, PathMode.Full3D)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    animator.StopWalking();
                    capsuleCollider.enabled = true;
                    isSwitchingLine = false;
                    // Animation completed
                    // Add any additional logic here
                });
        }
    }

    public void StopSwitchLineTween()
    {
        if (SwtichLineAnimation != null && SwtichLineAnimation.IsActive())
        {
            SwtichLineAnimation.Kill();
        }
    }

}
