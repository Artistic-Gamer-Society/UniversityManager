using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public void OnClick()
    {

    }
}
public interface IStudentProcess
{
    public void ProcessStart();
    public void ProcessComplete()
    {

    }
}