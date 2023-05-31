using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

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
        StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly(moneyText,playerMoney, playerMoney + amount));
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
