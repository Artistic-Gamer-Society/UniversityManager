using System;
using UnityEngine;
using UnityEngine.AI;

public class StudentAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnEnable()
    {
        Table.OnSelectingDesk += OnSelectingDeskWalk;
        EnrollmentTable.OnSelectingDesk += OnSelectingDeskWalk;
        StudentMovement.OnReachingDesk += OnReachingDeskStopWalk;
        StudentLineManager.OnStartRearrangeing += OnStartRearrangingWalk;
        StudentLineManager.OnCompleteRearranging += OnCompleteRearrangingStopWalk;
    }


    private void OnDisable()
    {
        Table.OnSelectingDesk -= OnSelectingDeskWalk;
        EnrollmentTable.OnSelectingDesk -= OnSelectingDeskWalk;
        StudentMovement.OnReachingDesk -= OnReachingDeskStopWalk;
        StudentLineManager.OnStartRearrangeing -= OnStartRearrangingWalk;
        StudentLineManager.OnCompleteRearranging -= OnCompleteRearrangingStopWalk;
    }
    private void OnReachingDeskStopWalk(Student student)
    {
        if (student.animator == this)
            StopWalking();
    }

    private void OnSelectingDeskWalk(Student student, Vector3 arg2)
    {
        if (student.animator == this)
            Walk();
    }
    private void OnCompleteRearrangingStopWalk(Student student)
    {
        if (student.animator == this)
            StopWalking();
    }

    private void OnStartRearrangingWalk(Student student)
    {
        if (student.animator == this)
            Walk();
    }
    public void Walk()
    {
        // Play walk animation
        animator.SetBool("isRunning", true);
    }

    public void StopWalking()
    {
        // Stop walk animation
        animator.SetBool("isRunning", false);
    }
    public void ResetPosition()
    {
        transform.localPosition = Vector3.up * transform.localPosition.y;
    }
    public void ResetRotaion()
    {
        transform.localRotation = Quaternion.identity;
    }
    // Other animation methods
}