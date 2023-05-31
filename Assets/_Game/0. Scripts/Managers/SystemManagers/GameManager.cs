using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public UnityEvent OnFirstTimeGameStart;

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


    private const string FirstTimeKey = "FirstTime";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(FirstTimeKey))
        {
            PerformFirstTimeAction();

            PlayerPrefs.SetInt(FirstTimeKey, 1);
            PlayerPrefs.Save();
        }
    }

    private void PerformFirstTimeAction()
    {
        OnFirstTimeGameStart?.Invoke();
    }

}
public enum UniversityPhase
{
    Enrollment,
    Examination,
    Ceremony
}
public static class Actions
{
    /// <summary>
    /// - Invokes From OnMouseDown At Student Script
    /// - Prepare a new selected student in Selection Manager
    /// </summary>
    public static Action<Student> OnStudentSelection;
    public static Action<Student,int> OnStudentCeremony;

    public static Action<Student> OnStudentSelectionCancel;

    public static Func<Student, Student> GetStudentAtTable;
}


