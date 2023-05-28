using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StudentLineManager : MonoBehaviour
{
    public Vector3 startingPoint;
    public float spacing;
    public Axis axis;
    public float rearrangeDuration = 1f;

    public List<Student> students = new List<Student>();
    public enum Axis
    {
        X,
        Y,
        Z
    }

    private void OnEnable()
    {
        RearrangeStudents();
        Actions.OnEnrollmentTable += RemoveStudent;
    }
    private void OnDestroy()
    {
        Actions.OnEnrollmentTable -= RemoveStudent;
    }
    public void AddStudent(Student student)
    {
        students.Add(student);
        student.transform.SetParent(transform); // Set student object as a child of the StudentLineManager
        RearrangeStudents();
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        student.transform.SetParent(null); // Remove student object from the StudentLineManager's children
        RearrangeStudents();
    }

    private void RearrangeStudents()
    {
        for (int i = 0; i < students.Count; i++)
        {
            Vector3 targetPosition = GetStudentLocalPosition(i);
            students[i].transform.DOLocalMove(targetPosition, rearrangeDuration).SetEase(Ease.Linear);
            students[i].startPos = targetPosition;
        }
    }

    private Vector3 GetStudentLocalPosition(int index)
    {
        float offset = index * spacing;
        Vector3 localPosition = startingPoint;

        switch (axis)
        {
            case Axis.X:
                localPosition.x += offset;
                break;
            case Axis.Y:
                localPosition.y += offset;
                break;
            case Axis.Z:
                localPosition.z += offset;
                break;
        }

        return localPosition;
    }
}
