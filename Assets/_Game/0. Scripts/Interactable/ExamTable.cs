using UnityEngine;

public class ExamTable : Table
{
    private void OnMouseDown()
    {
        base.OnSelectTable();

        Debug.Log("ExamTable: is" + name,gameObject);
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
