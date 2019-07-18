using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System.IO;
//Fényes József - GMananger script
public class GMananger : MonoBehaviour
{
    public Scene FirstScene = new Scene(0, "The Five Stores", "Insert Description Here");
    JsonData sceneJson;

    public Player p = new Player(100, 0, new int[] { 69 });
    JsonData playerJson;

    [Header("Start")]
    int i = 1;
    
    public bool SpawnEnemies = true;
    public int enemies;
    public Transform[] SpawnPoints;
    public GameObject[] Enemy;
    public Transform[] curPlayers;

    public Text enemiesLeft;
    public Text enemiesLeftT;

    public bool Detected = false;
    public bool MP = false;
    AgressiveAI AI;

    public int PrivWave = 0;
   
    [Space]

    [Header("Zene")]
    public bool PlayFromList = true;
    public AudioClip[] Music;
    private AudioSource m;


    void Start()
    {
        sceneJson = JsonMapper.ToJson(FirstScene);
        if(sceneJson == null)File.WriteAllText(Application.dataPath + "/Data/SceneData.json",sceneJson.ToString());

        playerJson = JsonMapper.ToJson(p);
        if (playerJson == null) File.WriteAllText(Application.dataPath + "/Data/playerData.json", playerJson.ToString());
        


        PrivWave = Random.Range(0, 3);
        PrivWave = enemies;
        Waves();
        m = gameObject.GetComponent<AudioSource>();
        for (int i = 0; i < enemies; i++)
        {
            SpEnemies();
        }


    }

    
    void Update()
    {
        if(enemies <= 0)
        {
            Destroy(enemiesLeft);
            Destroy(enemiesLeftT);
        }
       

        //Zene
        if (PlayFromList == true)
        {
            if (!m.isPlaying)
            {
                m.clip = Music[Random.Range(0, Music.Length)];
                //Automatikusan választ a megadott tömből egy zenét.
                m.Play();
            }
        }

    }
        void SpEnemies()
        {
                    int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
                    int EnemyTypes = Random.Range(0, Enemy.Length);
                    int CurPlayersIndex = Random.Range(0, curPlayers.Length);
                    
                    GameObject clo = (GameObject)Instantiate(Enemy[EnemyTypes], SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
                    AgressiveAI AI = clo.GetComponent<AgressiveAI>();
                    
                if (MP == true)
                {

                    //Multiplayerhez majd használható
                    AI.target = curPlayers[CurPlayersIndex];
                }
        }

        void Waves()
        {
        switch (PrivWave)
        {
            case 0:
                enemies = Random.Range(1,4);
                break;
            case 1:
                enemies = Random.Range(4, 8);
                break;
            case 2:
                enemies = Random.Range(8, 12);
                break;
            case 3:
                enemies = Random.Range(12, 16);
                break;
        }
    }

   
}

public class Scene
{
    public int NumberOfScene;
    public string NameOfScene = "";
    public string DescriptionOfScene = "";

    public Scene(int numberofscene, string nameofscene, string descofscene)
    {
        this.NumberOfScene = numberofscene;
        this.NameOfScene = nameofscene;
        this.DescriptionOfScene = descofscene;
    }
}

public class Player
{
    public int Health;
    public int Level;
    public int[] Items;


    public Player(int health,int level, int[] items)
    {
        this.Health = health;
        this.Level = level;
    }    
}





