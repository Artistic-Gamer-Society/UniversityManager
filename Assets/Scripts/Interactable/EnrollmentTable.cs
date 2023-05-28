using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnrollmentTable : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] Student student;
    [SerializeField] Transform studentStandingPoint;
    public UnityEvent OnStartEnrollment;
    private void OnMouseEnter()
    {
        student = Actions.OnResetStudentSelection?.Invoke(student);
        if (student == null)
            return;
        Actions.OnEnrollmentTable?.Invoke(this);
        OnStartEnrollment?.Invoke();
        student.transform.parent = studentStandingPoint;
        student.transform.DOLocalMove(Vector3.zero, 0.3f);
        boxCollider.enabled = false;
    }
    private void OnMouseUp()
    {

    }
}
