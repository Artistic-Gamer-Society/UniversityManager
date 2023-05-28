using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StudentData", menuName = "ScriptableObjects/StudentData", order = 1)]
public class StudentData : ScriptableObject
{
    public List<MaterialProperties> material;
    public Material studentMaterial;
    public void UpdateStudentSkin(StudentPhase studentPhase)
    {
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
    public void EnableOutline()
    {
        studentMaterial.SetFloat("_OutlineSize", 60);
    }
    [Serializable]
    public struct MaterialProperties
    {
        [BoxGroup("Color"), HideLabel]
        public Color albedoColor;
    }
}
