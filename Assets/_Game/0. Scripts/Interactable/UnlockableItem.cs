using UnityEngine;
using TMPro;

public class UnlockableItem : MonoBehaviour
{
    public int itemCost = 100;
    public TextMeshPro costText;

    [SerializeField] GameObject itemToUnlock;

    private void Start()
    {
        costText.text = "$" + itemCost.ToString();
    }

    private void OnMouseDown()
    {

        Currency currency = Currency.GetInstance();
        if (currency.playerMoney >= itemCost)
        {
            currency.SubtractMoney(itemCost);
            UnlockItem();
        }
        else
        {
            Debug.Log("Insufficient funds!");
        }
    }

    private void UnlockItem()
    {
        StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly(costText, itemCost, 0));
        itemCost = 0;      
        itemToUnlock.SetActive(true);
        gameObject.SetActive(false);
    }
}
