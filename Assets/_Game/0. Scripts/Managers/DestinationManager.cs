using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(9)]
public class DestinationManager : MonoBehaviour
{
    public List<Student> agents; // List of agents to track

    public Transform doorWorldTransform;

    private void Start()
    {
        // Initialize the list of agents (you can populate it through code or the Inspector)
        agents = new List<Student>();
    }
    private void OnEnable()
    {
        EnrollmentTable.OnSelectingDesk += AddAgent;
    }
    private void OnDisable()
    {
        EnrollmentTable.OnSelectingDesk -= AddAgent;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            Student student = agents[i];
            var agent = student.movement.navMeshAgent;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // The agent has reached its destination or is very close to it                
                student.movement.OnReachingDestinationPoint(student);
                RemoveAgent(student);
            }
        }
    }
    public void AddAgent(Student student, Vector3 pos)
    {
        agents.Add(student);
        student.doorPos = doorWorldTransform.position;
    }

    public void RemoveAgent(Student student)
    {
        agents.Remove(student);
    }
}
