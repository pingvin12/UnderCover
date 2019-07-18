using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianStats : MonoBehaviour {

    public int Health;
    string[] firstnames = new string[] { "Matt ", "Joanne ", "Robert ", "Donald ", "Ivanovich ", "Boris ", "Александар ", " Бранкица " };
    string[] lastnames = new string[] { "Oakley", "Smith", "Robinson", "Alexendrey", "Johannson", "Анастаија", "Братислава" };

    public string name;
    public int age;

   
    void Start () {
        name = firstnames[Random.Range(0, firstnames.Length)] + lastnames[Random.Range(0, lastnames.Length)];
        age = Random.Range(25, 42);
        Health = Random.Range(70, 150);
    }
	
	
	void Update () {
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
