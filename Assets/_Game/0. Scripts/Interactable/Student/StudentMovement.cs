using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;

public class StudentMovement : MonoBehaviour
{
    internal NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform target;

    public static Action<Student> OnReachingDesk;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        Table.OnSelectingDesk += MoveToDestination;
        EnrollmentTable.OnSelectingDesk += MoveToDestination;
        DestinationManager.OnReachingDestination += OnReachingDestinationPoint;
    }
    private void OnDisable()
    {
        Table.OnSelectingDesk -= MoveToDestination;
        EnrollmentTable.OnSelectingDesk -= MoveToDestination;
        DestinationManager.OnReachingDestination -= OnReachingDestinationPoint;
    }

    public void MoveToDestination(Student student, Vector3 destination)
    {
        student.movement.navMeshAgent.SetDestination(destination);
        student.transform.LookAt(destination);
        enabled = true;
        navMeshAgent.enabled = true;
    }
    public void OnReachingDestinationPoint(Student student)
    {
        if (student.movement == this)
        {
            switch (student.phase)
            {
                case UniversityPhase.Enrollment:
                    OnReachingDesk?.Invoke(student);
                    Debug.Log("Phase: is" + student.phase, student.gameObject);
                    break;
                case UniversityPhase.Examination:
                    Debug.Log("Phase: is" + student.phase, student.gameObject);
                    break;
                case UniversityPhase.Ceremony:
                    Debug.Log("Phase: is" + student.phase, student.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    public void StopMovement()
    {
        navMeshAgent.isStopped = true;
    }

}