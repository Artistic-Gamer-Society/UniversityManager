using System;
using UnityEngine;

/// <summary>
/// - One Selected. Disable Its Collider 
/// - Drag
/// - At Destination - At Cancel
/// </summary>
public class SelectionManager : MonoBehaviour
{
    internal static Student selectedStudent;
    private void OnEnable()
    {
        Actions.OnStudentSelection += SetSelectedStudent;
    }
    private void OnDisable()
    {
        Actions.OnStudentSelection -= SetSelectedStudent;
    }
    private void SetSelectedStudent(Student obj)
    {
        if (obj.isReadyToChangePhase)
            return;
        var previous = selectedStudent;
        if (previous != null)
        {
            StudentData.SetOutline(0, previous);
            previous.ResetStudentDefaultState();
        }
        selectedStudent = obj;
        selectedStudent.movement.enabled = true;
        selectedStudent.movement.navMeshAgent.enabled = true;
        StudentData.SetOutline(60, selectedStudent);
    }
}
#region Commented
/*
 *     private Vector3 refVel = Vector3.zero;
 * //private void Update()
//{
//    if (GameManager.Instance.isPlay && selectedStudent != null)
//    {
//        if (Input.GetMouseButton(0))
//        {
//            if (GameManager.Instance.isPlay == false)
//            {
//                return;
//            }
//            var currentPos = selectedStudent.transform.position;
//            MoveObjectToMousePosition(currentPos, refVel, gameConfig.movementSmoothing);
//        }
//        else if (Input.GetMouseButtonUp(0) && selectedStudent != null)
//        {
//            selectedStudent.ResetStudentDefaultState();
//            selectedStudent = null;
//        }
//    }
//}
    /// <summary>
    /// Returns Current Mouse Position, Screen to world point.
    /// </summary>
    /// <param name="gridEnabled"></param>
    /// <returns></returns>    
    Vector3 GetTargetPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = gameConfig.selectedStudentHeight - Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    bool IsUnderBoundary(Vector3 pos)
    {
        if (pos.x < gameConfig.leftBoundary || (pos.x > gameConfig.rightBoundary)
            || pos.y < gameConfig.bottomBounday || (pos.y > gameConfig.topBoundary))
        {
            return false;
        }
        else return true;
    }
    void MoveObjectToMousePosition(Vector3 currentPos, Vector3 refVel, float smoothTime)
    {
        Vector3 targetPos = GetTargetPos();
        currentPos = Vector3.SmoothDamp(currentPos, targetPos, ref refVel, smoothTime);
        GameManager.selectedStudent.transform.position = currentPos;
    }
 */
#endregion