using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentKeyParent : MonoBehaviour
{
    [SerializeField]
    private List<Student> students = new List<Student>();

    [Button]
    public void GetUnlockItemsAndAssignKeys()
    {
        students.Clear();
        CollectUnlockableItems();
        AssignItemKeys();
    }
    private void CollectUnlockableItems()
    {
        students.AddRange(GetComponentsInChildren<Student>(true));
    }

    private void AssignItemKeys()
    {
        for (int i = 0; i < students.Count; i++)
        {
            students[i].studentKey = gameObject.name + "_" + i.ToString();
        }
    }
}
