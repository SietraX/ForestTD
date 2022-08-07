using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Outline : MonoBehaviour
{
    TextMeshPro textMeshPro;
    [SerializeField][Range(0f, 10f)] float outlineKalinligi;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.outlineWidth = outlineKalinligi;
        textMeshPro.outlineColor = new Color32(255, 128, 0, 255);
    }
}
