using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnrollmentTable : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] Student currentStudent;
    [SerializeField] Transform studentStandingPoint;
    [SerializeField] RadialProgressBar progressBar;
    public UnityEvent OnStartEnrollment;
    public UnityEvent OnEnrollmentComplete;

    private void OnMouseEnter()
    {
        ProcessStart();
    }
    public void ProcessStart()
    {
        currentStudent = Actions.OnStudentProcessing?.Invoke(currentStudent);
        if (currentStudent == null)
            return;
        Actions.OnEnrollmentTable?.Invoke(currentStudent);

        Debug.Log("Dialog: is" + currentStudent);
        OnStartEnrollment?.Invoke();
        currentStudent.transform.parent = studentStandingPoint;
        currentStudent.transform.DOLocalMove(Vector3.zero, 0.3f);
        boxCollider.enabled = false;
        progressBar.gameObject.SetActive(true);
        progressBar.student = currentStudent;

        currentStudent = null;
    }
}
