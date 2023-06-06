using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Student studentPrefab;
    public Transform spawnPoint;
    public int poolSize = 10;
    public Button spawnButton;
    public TextMeshProUGUI moneyRequiredToSpawnText;

    public List<Student> studentPool = new List<Student>();
    public static event Action<Student, StudentLineManager> OnStudentSpawn;
    [SerializeField] StudentLineManager lineToSpawnIn;
    [SerializeField] SpawnerData spawnerData;


    private const string RemovedStudentsKey = "RemovedStudents";

    private List<int> removedStudents = new List<int>();
    private void Start()
    {
        // Create the student pool       
        for (int i = 0; i < poolSize; i++)
        {
            Student student = Instantiate(studentPrefab, spawnPoint.position, Quaternion.identity);
            student.transform.SetParent(transform);
            student.gameObject.SetActive(false);
            studentPool.Add(student);
        }
        spawnerData.LoadRequiredMoney();
        moneyRequiredToSpawnText.text = "Student\n$" + spawnerData.GetCurrentRequiredMoney();

        if (PlayerPrefs.HasKey(RemovedStudentsKey))
        {
            string removedStudentsData = PlayerPrefs.GetString(RemovedStudentsKey);
            removedStudents = JsonUtility.FromJson<List<int>>(removedStudentsData);

            // Enable the removed students
            foreach (int studentIndex in removedStudents)
            {

                Debug.Log("ok: is");
                if (studentIndex < studentPool.Count)
                {
                    Student student = studentPool[studentIndex];
                    student.gameObject.SetActive(true);
                    // Set the position or any other properties as needed
                }
            }
        }
    }
    private void OnEnable()
    {
        spawnButton.onClick.AddListener(SpawnStudent);
    }
    private void OnDisable()
    {
        spawnButton.onClick.RemoveListener(SpawnStudent);
    }
    private void SpawnStudent()
    {
        Currency currency = Currency.GetInstance(); // To Unlock Item You Need Money So Here It Is Dependent.
       
        // Check if there are students available in the pool
        if (studentPool.Count > 0 && spawnerData.GetCurrentRequiredMoney() <= currency.playerMoney)
        {
            // Get the last student from the pool
            Student student = studentPool[studentPool.Count - 1];
            studentPool.Remove(student);

            // Enable the student object and position it at the spawn point
            student.gameObject.SetActive(true);
            student.transform.position = spawnPoint.position;
            OnStudentSpawn?.Invoke(student, lineToSpawnIn);
            currency.SubtractMoney(spawnerData.GetCurrentRequiredMoney());
            StartCoroutine(TextSmoothUpdater.UpdateMoneyTextSmoothly("Student\n$",moneyRequiredToSpawnText,
                spawnerData.GetCurrentRequiredMoney(), spawnerData.CalculateMoneyNeededToSpawn(),TextEffect.None));

            removedStudents.Add(studentPool.Count - 1);
            SaveRemovedStudents();
        }

        //// Disable the spawn button if there are no more students in the pool
        //spawnButton.interactable = studentPool.Count > 0;
    }

    private void ReturnStudentToPool(Student student)
    {
        // Reset the student's position and disable it
        student.transform.position = spawnPoint.position;
        student.gameObject.SetActive(false);

        // Add the student back to the pool
        studentPool.Add(student);

        // Enable the spawn button
        spawnButton.interactable = true;
        removedStudents.Add(studentPool.Count - 1);
        SaveRemovedStudents();
    }
    private void SaveRemovedStudents()
    {
        // Serialize the list of removed students and store it in PlayerPrefs
        string removedStudentsData = JsonUtility.ToJson(removedStudents);
        PlayerPrefs.SetString(RemovedStudentsKey, removedStudentsData);
        PlayerPrefs.Save();
    }
}
