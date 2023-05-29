using System;
using UnityEngine;
using UnityEngine.AI;

public class StudentAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnEnable()
    {
        EnrollmentTable.OnSelectingDesk += OnSelectingDesk;
        StudentMovement.OnReachingDesk += OnReachingDesk;
        StudentLineManager.OnStartRearrangeing += OnStartRearranging;
        StudentLineManager.OnCompleteRearranging += OnCompleteRearranging;
    }


    private void OnDisable()
    {
        EnrollmentTable.OnSelectingDesk -= OnSelectingDesk;
        StudentMovement.OnReachingDesk -= OnReachingDesk;
        StudentLineManager.OnStartRearrangeing -= OnStartRearranging;
        StudentLineManager.OnCompleteRearranging -= OnCompleteRearranging;
    }
    private void OnReachingDesk(Student student)
    {
        if (student.animator == this)
            StopWalking();
    }

    private void OnSelectingDesk(Student student, Vector3 arg2)
    {
        if (student.animator == this)
            Walk();
    }
    private void OnCompleteRearranging(Student student)
    {
        if (student.animator == this)
            StopWalking();
    }

    private void OnStartRearranging(Student student)
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

    // Other animation methods
}