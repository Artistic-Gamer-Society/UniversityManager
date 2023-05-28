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
        Actions.OnResetStudentSelection += ResetSelectedStudent;
    }
    private void OnDisable()
    {
        Actions.OnStudentSelection -= SetSelectedStudent;
        Actions.OnResetStudentSelection -= ResetSelectedStudent;
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
    }
    private Student ResetSelectedStudent(Student student)
    {
        student = selectedStudent;
        selectedStudent = null;
        return student;
    }    
}
/*Commented
    private RaycastHit hit;    
void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hits a student in the queue
                if (hit.collider.CompareTag("Student"))
                {
                    // Select the student
                    selectedStudent = hit.collider.gameObject;
                }
            }
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            // Check if a student is currently selected
            if (selectedStudent != null)
            {
                // Cast a ray from the mouse position into the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the ray hits an enrollment stall
                    if (hit.collider.CompareTag("EnrollmentStall"))
                    {
                        // Drag the student to the enrollment stall
                        selectedStudent.transform.position = hit.point;
                        // TODO: Add enrollment logic here
                    }
                }

                // Deselect the student
                selectedStudent = null;
            }
        }
    }
 * 
 */
