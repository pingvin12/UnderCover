using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PointyFont : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}

