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
        EnrollmentTable.OnSelectingDesk += MoveToDestination;
    }
    private void OnDisable()
    {
        EnrollmentTable.OnSelectingDesk -= MoveToDestination;
    }

    public void MoveToDestination(Student student, Vector3 destination)
    {
        enabled = true;
        navMeshAgent.enabled = true;
        student.movement.navMeshAgent.SetDestination(destination);
        student.transform.LookAt(destination);
    }
    public void OnReachingDestinationPoint(Student student)
    {
        if (student.movement == this)
        {
            enabled = false;
            navMeshAgent.enabled = false;

            OnReachingDesk?.Invoke(student);
        }
    }

    public void StopMovement()
    {
        navMeshAgent.isStopped = true;
    }

}