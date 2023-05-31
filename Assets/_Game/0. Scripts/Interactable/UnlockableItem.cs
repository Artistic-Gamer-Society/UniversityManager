using UnityEngine;
using TMPro;

public class UnlockableItem : MonoBehaviour
{
    public int itemCost = 100;
    public TextMeshPro costText;
    public string itemKey; // Unique key to identify the unlockable item

    [SerializeField] GameObject itemToUnlock;

    private void Start()
    {
        costText.text = "$" + itemCost.ToString();
        CheckUnlockStatus();
    }

    private void OnMouseDown()
    {
        Currency currency = Currency.GetInstance();
        if (currency.playerMoney >= itemCost)
        {
            currency.SubtractMoney(itemCost);
            UnlockItem();
            SaveUnlockStatus();
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

    private void CheckUnlockStatus()
    {
        if (PlayerPrefs.HasKey(itemKey))
        {
            // Item is already unlocked, update the UI or apply any necessary changes
            UnlockItem();
        }
    }

    private void SaveUnlockStatus()
    {
        // Save the unlock status of the item
        PlayerPrefs.SetInt(itemKey, 1);
        PlayerPrefs.Save();
    }
}
