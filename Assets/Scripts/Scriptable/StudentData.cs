using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

[CreateAssetMenu(fileName = "StudentData", menuName = "ScriptableObjects/StudentData", order = 1)]
public class StudentData : ScriptableObject
{
    [SerializeField] List<MaterialProperties> material;
    public void UpdateStudentSkin(StudentPhase studentPhase, Student student)
    {

        Debug.Log("UpdateSkin: is" + studentPhase);
        Material studentMaterial = student.meshRenderer.material; // Get the material of the student
        Material materialInstance = new Material(studentMaterial); // Create a new material instance

        switch (studentPhase)
        {
            case (StudentPhase.Enrollment):
                materialInstance.SetColor("_AlbedoColor", material[0].albedoColor);
                break;
            case (StudentPhase.Examination):
                materialInstance.SetColor("_AlbedoColor", material[1].albedoColor);
                break;
            case (StudentPhase.Passout):
                materialInstance.SetColor("_AlbedoColor", material[2].albedoColor);
                break;
        }
        student.meshRenderer.material = materialInstance;
    }
    public void SetOutline(int value, Student student)
    {
        Material studentMaterial = student.meshRenderer.material; // Get the material of the student
        Material materialInstance = new Material(studentMaterial); // Create a new material instance

        // Set the outline size value in the material instance
        materialInstance.SetFloat("_OutlineSize", value);

        // Apply the material instance to the student's mesh renderer
        student.meshRenderer.material = materialInstance;
    }
    [Serializable]
    public struct MaterialProperties
    {
        [BoxGroup("Color"), HideLabel]
        public Color albedoColor;
    }
}
