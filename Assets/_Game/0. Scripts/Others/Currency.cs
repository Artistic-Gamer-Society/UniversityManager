using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class Currency : MonoBehaviour
{
    public int playerMoney = 200;
    public TextMeshProUGUI moneyText;
    private static Currency instance;

    private const string PlayerMoneyKey = "PlayerMoney";

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

    private void Start()
    {
        LoadPlayerMoney(); // Load the player's money from PlayerPrefs
        UpdateMoneyText();
    }

    private void OnEnable()
    {
        Actions.OnStudentCeremony += AddMoney;
    }

    private void OnDisable()
    {
        Actions.OnStudentCeremony -= AddMoney;
    }

    public static Currency GetInstance()
    {
        return instance;
    }

    [Button]
    public void AddMoney(Student student, int amount)
    {
        StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly(moneyText, playerMoney, playerMoney + amount));
        playerMoney += amount;
        SavePlayerMoney(); // Save the updated player's money to PlayerPrefs
    }

    public void SubtractMoney(int amount)
    {
        StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly(moneyText, playerMoney, playerMoney - amount));
        playerMoney -= amount;
        SavePlayerMoney(); // Save the updated player's money to PlayerPrefs
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "$" + playerMoney.ToString();
    }

    private void SavePlayerMoney()
    {
        PlayerPrefs.SetInt(PlayerMoneyKey, playerMoney);
        PlayerPrefs.Save();
    }

    private void LoadPlayerMoney()
    {
        if (PlayerPrefs.HasKey(PlayerMoneyKey))
        {
            playerMoney = PlayerPrefs.GetInt(PlayerMoneyKey);
        }
    }
}
