using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StudentData", menuName = "ScriptableObjects/StudentData", order = 1)]
public class StudentData : ScriptableObject
{
    [SerializeField] List<MaterialProperties> material;
    [SerializeField] Material studentMaterial;
    public void UpdateStudentSkin(StudentPhase studentPhase, Student student)
    {
        student.meshRenderer.material = studentMaterial;
        switch (studentPhase)
        {
            case (StudentPhase.Enrollment):
                studentMaterial.SetColor("_AlbedoColor", material[0].albedoColor);
                break;
            case (StudentPhase.Examination):
                studentMaterial.SetColor("_AlbedoColor", material[1].albedoColor);
                break;
            case (StudentPhase.Passout):
                studentMaterial.SetColor("_AlbedoColor", material[2].albedoColor);
                break;
        }
    }
    public void SetOutline(int value, Student student)
    {
        student.meshRenderer.sharedMaterial = studentMaterial;
        studentMaterial = student.meshRenderer.sharedMaterial;
        studentMaterial.SetFloat("_OutlineSize", value);
    }
    [Serializable]
    public struct MaterialProperties
    {
        [BoxGroup("Color"), HideLabel]
        public Color albedoColor;
    }
}
