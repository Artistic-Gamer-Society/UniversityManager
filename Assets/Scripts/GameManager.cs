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
    internal bool isPlay = true;
}
public static class Actions
{
    public static Action<Student> OnStudentSelection;
    public static Action<EnrollmentTable> OnEnrollmentTable;
    public static Func<Student, Student> OnResetStudentSelection;
}

