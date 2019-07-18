using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IDMananger : MonoBehaviour {

    private string idfirst;
    private int idsecond;
    private int idamount;
    string[] chars = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    
    [Header("ID")]
    [SerializeField]
    public string ID;

    void Awake()
    {
        idamount = Random.Range(1, 5);
        idsecond = Random.Range(0 , 9);
        
        idfirst = chars[Random.Range(0, chars.Length)] + chars[Random.Range(0, chars.Length)];
        ID = idfirst + idsecond;
        //468 különböző id
        
    }

}
