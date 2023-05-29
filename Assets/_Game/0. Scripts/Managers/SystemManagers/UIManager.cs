using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RoomButton roomButton;
    private static UIManager instance;
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
    public static UIManager GetInstance()
    {
        return instance;
    }
}
[Serializable]
public struct RoomButton
{
    public TextMeshProUGUI roomNameTMP;
    public RectTransform door;
    internal void UpdateRoomName()
    {

    }
}

