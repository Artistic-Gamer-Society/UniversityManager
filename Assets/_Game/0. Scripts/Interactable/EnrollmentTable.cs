using UnityEngine;

public class EnrollmentTable : Table
{    
    private void OnMouseDown()
    {
        base.OnSelectTable();
        Debug.Log("EnrollmentTable: is" + name, gameObject);

    }
    private void OnEnable()
    {
        StudentMovement.OnReachingDesk += base.StartProcess;
    }
    private void OnDisable()
    {
        StudentMovement.OnReachingDesk -= base.StartProcess;
    }
}
