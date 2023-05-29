using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public StudentLineManager enrollmentLineManager;
    public StudentLineManager examinationLineManager;
    public StudentLineManager ceremonyLineManager;

    private Dictionary<Student, StudentLineManager> studentLineDictionary = new Dictionary<Student, StudentLineManager>();

    private void OnEnable()
    {
        Student.OnPhaseChanged += RemoveStudent;
    }
    private void OnDisable()
    {
        Student.OnPhaseChanged -= RemoveStudent;

    }
    private void Start()
    {
        foreach(var st in enrollmentLineManager.students)
        {
            studentLineDictionary.Add(st, enrollmentLineManager);
        }
    }
    public void AddStudent(Student student, StudentLineManager lineManager)
    {
        lineManager.AddStudent(student);
        studentLineDictionary.Add(student, lineManager);
    }

    public void RemoveStudent(Student student)
    {
        if (studentLineDictionary.ContainsKey(student))
        {
            print("ok");
            StudentLineManager currentLineManager = studentLineDictionary[student];
            currentLineManager.RemoveStudent(student, Vector3.zero);

            // Determine the new line manager based on the current line
            StudentLineManager newLineManager = GetNewLineManager(currentLineManager);

            if (newLineManager != null)
            {
                // Move the student to the new line manager
                newLineManager.AddStudent(student);
                studentLineDictionary[student] = newLineManager;
            }
            else
            {
                // If there is no new line manager, remove the student from the dictionary
                studentLineDictionary.Remove(student);
            }
        }
    }


    private StudentLineManager GetNewLineManager(StudentLineManager currentLineManager)
    {
        if (currentLineManager == enrollmentLineManager)
        {
            return examinationLineManager;
        }
        else if (currentLineManager == examinationLineManager)
        {
            return ceremonyLineManager;
        }
        else if (currentLineManager == ceremonyLineManager)
        {
            return null; // No new line manager
        }

        return null;
    }
}