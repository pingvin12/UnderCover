using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fényes József - EnemyStats - Az ellenség tulajdonságai script
 
public class EnemyStats : MonoBehaviour {

    public float Health;
    public int maxDamage;
    public int maxClips;
    public int curAmmo = 30;
    string[] firstnames = new string[] { "Matt ", "Joanne ", "Robert ", "Donald ", "Ivanovich ","Boris ", "Александар ", " Бранкица " };
    string[] lastnames = new string[] { "Oakley", "Smith", "Robinson", "Alexendrey", "Johannson", "Анастаија", "Братислава" };
    public GameObject[] Weapons;
    public int detectionLevel;
    public int ShootRange = 50;
    public GMananger GM;

    public string name;
    public int age;

    void Start()
    {
        name = firstnames[Random.Range(0, firstnames.Length)] + lastnames[Random.Range(0, lastnames.Length)];
        age = Random.Range(25, 42);
        Health = Random.Range(70, 150);
        maxDamage = Random.Range(30, 50);
        maxClips = Random.Range(20, 30);
        maxClips = Mathf.Clamp(maxClips, 0, 30);
        curAmmo = Mathf.Clamp(curAmmo, 0, 30);
        detectionLevel = Mathf.Clamp(detectionLevel,0, 100);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            GM.enemies -= 1;
            Destroy(gameObject);
        }
    }

    void Update()
    {
    
        if(Health <= 110)
        {
            detectionLevel += 50;
        }

    }


}
