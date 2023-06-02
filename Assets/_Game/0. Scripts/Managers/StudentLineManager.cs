using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(9)]
public class StudentLineManager : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] float spacing;
    [SerializeField] float rearrangeDuration = 1.5f;
    [SerializeField] Axis axis;

    public UniversityPhase linePhase;
    public List<Student> students = new List<Student>();

    private Vector3 startPos;
    /// <summary>
    /// - Start Walk
    /// </summary>
    public static event Action<Student> OnStartRearrangeing;
    public enum Axis
    {
        X,
        Y,
        Z
    }
    #region Unity CallBacks
    private void Start()
    {
        startPos = startPoint.localPosition;
    }
    private void OnEnable()
    {
        RearrangeStudents();
        Table.OnSelectingDesk += RemoveStudent;
    }
    private void OnDisable()
    {
        Table.OnSelectingDesk -= RemoveStudent;
    }
    #endregion
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
        student.StopRearranging();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            RearrangeStudents();
        });

    }
    private void RearrangeStudents()
    {
        for (int i = 0; i < students.Count; i++)
        {
            Vector3 targetPosition = GetStudentLocalPosition(i);
            OnStartRearrangeing?.Invoke(students[i]);
            if (students[i].isReadyToChangePhase == false)
            {
                students[i].StartRearranging(targetPosition, rearrangeDuration);
                students[i].startPos = targetPosition;
            }
            else
            {
                var pos = students[i].transform.localPosition;
                Vector3[] positions = new Vector3[]
                {
                    new Vector3(pos.x, -5, -15),
                    new Vector3(targetPosition.x, targetPosition.y, -15),
                    targetPosition,
                };
                students[i].SwitchLineAnimation(positions, 2, students[i]);
                students[i].startPos = targetPosition;
            }
        }
    }
    private Vector3 GetStudentLocalPosition(int index)
    {
        float offset = index * spacing;
        Vector3 localPosition = startPos;

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
