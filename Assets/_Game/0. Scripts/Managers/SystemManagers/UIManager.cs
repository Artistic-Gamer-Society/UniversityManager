using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RoomDoor enrollment, examination, ceremony;
    public Animator examinationAnim;
    private static UIManager instance;

    private const string EnablePhase2= "EnableExamination";

    #region Unity CallBacks
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        DestinationManager.OnReachingNextPhase += EnableExmination;
        if (PlayerPrefs.HasKey(EnablePhase2))
        {

            DOVirtual.DelayedCall(1, () =>
            {
                examinationAnim.enabled = true;
            });
        }
    }
    private void OnDisable()
    {
        DestinationManager.OnReachingNextPhase -= EnableExmination;
    }
    #endregion
    public static UIManager GetInstance()
    {
        return instance;
    }
    public void EnableExmination(Student student)
    {
        if (!PlayerPrefs.HasKey(EnablePhase2))
        {
            examinationAnim.enabled = true;
            PlayerPrefs.SetInt(EnablePhase2, 1);
            PlayerPrefs.Save();
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
[Serializable]
public struct RoomDoor
{
    public GameObject door;
    public Button btn;
}

