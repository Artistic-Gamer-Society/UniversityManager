using UnityEngine;
using TMPro;

public class Currency : MonoBehaviour
{
    public int playerMoney = 0;
    public TextMeshProUGUI moneyText;
    private static Currency instance;

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
        UpdateMoneyText();
    }

    public static Currency GetInstance()
    {
        return instance;
    }

    public void AddMoney(int amount)
    {
        StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly(moneyText,playerMoney, playerMoney - amount));
        playerMoney += amount;
    }

    public void SubtractMoney(int amount)
    {
        StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly(moneyText,playerMoney, playerMoney - amount));
        playerMoney -= amount;
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "$" + playerMoney.ToString();
    }
}
