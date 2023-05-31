using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.AI;
using DG.Tweening;

[DefaultExecutionOrder(9)]
public class DestinationManager : MonoBehaviour
{
    public List<Student> agents; // List of agents to track

    [SerializeField] Transform enrollmentDoor;
    [SerializeField] Transform examinationDoor;
    [SerializeField] Transform ceremonyDoor;

    Transform nextRoomDoor;

    public static event Action<Student> OnReachingDestination;
    public static event Action<Student> OnReachingNextPhase;

    private void Start()
    {
        // Initialize the list of agents (you can populate it through code or the Inspector)
        agents = new List<Student>();
    }

    private void OnEnable()
    {
        Actions.OnStudentSelection += AddAgent;
    }

    private void OnDisable()
    {
        Actions.OnStudentSelection -= AddAgent;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            Student student = agents[i];
            var agent = student.movement.navMeshAgent;

            if (agent.isActiveAndEnabled)
            {
                if (agent.hasPath)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        // The agent has reached its destination or is very close to it
                        OnReachingDestination?.Invoke(student);
                        RemoveAgentWithoutEvent(student);           
                        if (agent.destination.x == GetNextRoomDoorDestination(student.phase).position.x)
                        {
                            OnReachingNextPhase?.Invoke(student);
                            student.transform.rotation = Quaternion.identity;
                            agent.enabled = false;
                        }
                    }

                }

            }
        }
    }

    public Transform GetNextRoomDoorDestination(UniversityPhase phase)
    {
        if (phase == UniversityPhase.Enrollment)
        {
            nextRoomDoor = enrollmentDoor;
        }
        else if(phase== UniversityPhase.Examination)
        {
            nextRoomDoor = examinationDoor;
        }
        else if (phase == UniversityPhase.Ceremony)
        {
            nextRoomDoor = ceremonyDoor;
        }
        return nextRoomDoor;
    }
    public void AddAgent(Student student)
    {
        student.doorPos = GetNextRoomDoorDestination(student.phase).position;
        agents.Add(student);
    }
    public void SetRoomDoor(Transform door)
    {
        examinationDoor = door;
    }
    public void RemoveAgentWithoutEvent(Student student)
    {
        agents.Remove(student);
    }
}
