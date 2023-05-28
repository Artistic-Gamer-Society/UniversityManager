using System;
using TMPro;

public class GameManager
{
    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }
    internal bool isPlay = true;
    internal RoomType roomType = RoomType.Enrollment;
    internal Student currentSelectedStudent;
}
public enum RoomType
{
    Enrollment,
    Examination,
    Ceremony
}
public static class Actions
{
    public static Action<Student> OnStudentSelection;
    public static Action<Student> OnStudentSelectionCancel;
    public static Action<Student> OnEnrollmentTable;
    public static Func<Student, Student> OnStudentProcessing;
}

