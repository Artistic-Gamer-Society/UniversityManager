using System;

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
    internal bool isPlay = false;
}
public enum UniversityPhase
{
    None,
    Enrollment,
    Examination,
    Ceremony
}
public static class Actions
{
    public static Action<Student> OnStudentSelection;
    public static Action<Student> OnStudentSelectionCancel;

    public static Func<Student, Student> GetStudentAtTable;
}

