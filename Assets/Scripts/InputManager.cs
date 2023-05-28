using System;
using UnityEngine;

/// <summary>
/// - One Selected. Disable Its Collider 
/// - Drag
/// - At Destination - At Cancel
/// </summary>
public class InputManager : MonoBehaviour
{
    private Student selectedStudent;
    private Vector3 refVel = Vector3.zero;
    [SerializeField] GameConfig gameConfig;
    private void OnEnable()
    {
        Actions.OnStudentSelection += SetSelectedStudent;
        Actions.OnStudentProcessing += ResetSelectedStudent;
    }
    private void OnDisable()
    {
        Actions.OnStudentSelection -= SetSelectedStudent;
        Actions.OnStudentProcessing -= ResetSelectedStudent;
    }
    private void Update()
    {
        if (GameManager.Instance.isPlay && selectedStudent != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (GameManager.Instance.isPlay == false)
                {
                    return;
                }
                var currentPos = selectedStudent.transform.position;
                MoveObjectToMousePosition(currentPos, refVel, gameConfig.movementSmoothing);
            }
            else if (Input.GetMouseButtonUp(0) && selectedStudent != null)
            {
                selectedStudent.ResetStudentDefaultState();
                selectedStudent = null;
            }
        }
    }
    public void Init()
    {
        selectedStudent = null;
    }
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
        selectedStudent.transform.position = currentPos;
    }
    private void SetSelectedStudent(Student obj)
    {
        selectedStudent = obj;
        GameManager.Instance.currentSelectedStudent = obj;
    }
    private Student ResetSelectedStudent(Student student)
    {
        student = selectedStudent;
        selectedStudent = null;
        return student;
    }
}