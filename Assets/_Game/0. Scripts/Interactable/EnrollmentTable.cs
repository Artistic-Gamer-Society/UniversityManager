using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.AI;
using System;

public class EnrollmentTable : MonoBehaviour
{
    public BoxCollider boxCollider;
    [SerializeField] Student currentStudent; // Need To Store Reference.
    [SerializeField] Transform studentStandingPoint;
    [SerializeField] RadialProgressBar progressBar;
    public UnityEvent OnStartEnrollment;

    public static Action<Student, Vector3> OnSelectingDesk;


    private void OnMouseUp()
    {
        OnSelectTable();
    }
    private void OnEnable()
    {
        StudentMovement.OnReachingDesk += StartProcess;
    }
    private void OnDisable()
    {
        StudentMovement.OnReachingDesk -= StartProcess;
    }
    public void OnSelectTable()
    {

        currentStudent = SelectionManager.selectedStudent; // Pick Student Ref
        if (currentStudent == null) //Is There any selected Student
            return;

        OnSelectingDesk?.Invoke(currentStudent, transform.position);
        OnStartEnrollment?.Invoke(); // In case we want to add something from editor.


        boxCollider.enabled = false;

        progressBar.student = currentStudent; // It will be use to make student ready for next phase, whenever progress will be completed.         
        SelectionManager.selectedStudent = null;
    }
    private void StartProcess(Student student)
    {
        if (currentStudent != null)
        {
            if (student == currentStudent)
            {
                student.enrollmentTable = this;
                progressBar.gameObject.SetActive(true);
                currentStudent.transform.parent = studentStandingPoint;
                currentStudent.transform.DOLocalMove(Vector3.zero, 0.5f);
                student.transform.LookAt(studentStandingPoint);
                currentStudent = null;
            }
        }
    }
}
