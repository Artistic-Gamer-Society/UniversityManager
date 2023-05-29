using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(9)]
public class StudentLineManager : MonoBehaviour
{
    public Vector3 startingPoint;
    public float spacing;
    public Axis axis;
    public float rearrangeDuration = 1f;

    public List<Student> students = new List<Student>();
    public UniversityPhase phase;

    public static event Action<Student> OnStartRearrangeing;
    public static event Action<Student> OnCompleteRearranging;

    public enum Axis
    {
        X,
        Y,
        Z
    }

    private void OnEnable()
    {
        RearrangeStudents();
        EnrollmentTable.OnSelectingDesk += RemoveStudent;
    }
    private void OnDestroy()
    {
        EnrollmentTable.OnSelectingDesk -= RemoveStudent;
    }
    public void AddStudent(Student student)
    {
        students.Add(student);
        student.transform.SetParent(transform); // Set student object as a child of the StudentLineManager
        RearrangeStudents();
    }

    public void RemoveStudent(Student student, Vector3 tablePos)
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
            OnStartRearrangeing?.Invoke(students[i]);
            students[i].transform.DOLocalMove(targetPosition, rearrangeDuration).
                    SetEase(Ease.Linear).OnComplete(() =>
                    {
                        for (int i = 0; i < students.Count; i++)
                        {
                            OnCompleteRearranging?.Invoke(students[i]);
                        }
                    });
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
