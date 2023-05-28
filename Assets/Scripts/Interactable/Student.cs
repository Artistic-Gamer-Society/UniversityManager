using UnityEngine;
/// <summary>
/// - Student Has 4 Phases {None -> Enrollment -> Examination -> Passout -> None}
/// </summary>
public enum StudentPhase
{
    None = 0,
    Enrollment = 1,
    Examination = 2,
    Passout = 3
}
public class Student : MonoBehaviour, IInteractable
{
    [SerializeField]
    StudentPhase phase;
    [SerializeField]
    CapsuleCollider capsuleCollider;
    /// <summary>
    /// - Check If The Student Is Selected
    /// </summary>
    private void OnMouseDown()
    {
        print("Select Student");
        Actions.OnStudentSelection?.Invoke(this);
        capsuleCollider.enabled = false;
        gameObject.transform.parent = null;
    }
    private void OnMouseUp()
    {
        
    }
}
