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
        moneyText.text = "Money: " + playerMoney.ToString();
    }

    private System.Collections.IEnumerator UpdateMoneyTextSmoothly(int startValue, int endValue, float duration = 1f)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time <= endTime)
        {
            float t = Mathf.InverseLerp(startTime, endTime, Time.time);
            int currentValue = (int)Mathf.Lerp(startValue, endValue, t);
            moneyText.text = "Money: " + currentValue.ToString();
            yield return null;
        }

        moneyText.text = "Money: " + endValue.ToString();
    }
}
