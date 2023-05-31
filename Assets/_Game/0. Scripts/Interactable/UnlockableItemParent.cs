using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class UnlockableItemParent : MonoBehaviour
{
    [SerializeField]
    private List<UnlockableItem> unlockableItems = new List<UnlockableItem>();

    [Button]
    public void GetUnlockItemsAndAssignKeys()
    {
        unlockableItems.Clear();
        CollectUnlockableItems();
        AssignItemKeys();
    }

    private void CollectUnlockableItems()
    {
        unlockableItems.AddRange(GetComponentsInChildren<UnlockableItem>(true));
    }

    private void AssignItemKeys()
    {
        for (int i = 0; i < unlockableItems.Count; i++)
        {
            unlockableItems[i].itemKey = gameObject.name + "_" + i.ToString();
        }
    }
}
