using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DAN : MonoBehaviour {



    [Space]

    [Header("Weather")]
    public int chance;
    public bool isRaining;
    public bool isFoggy;
    public bool isNormal;

    public ParticleSystem Rain;
   
    //CHANGED TO PUBLIC FOR DEBUG PURPOSES
    public int RandomWeather;

    private int x;
    private int y;
    private int z;

    public AudioSource RainSound;
  
    [Space]

    [Header("Time")]
    public string curTime;
    public int speed;
    public int days;
   
    public TimeSpan currenttime;
    public Transform Suntransform;
    public Light Sun;

    private float intensity;
    private float time;

    public int RandomTimeFrom = 21600;
    public int RandomTimeTo = 43200;


    void Start()
    {
        ChangeWeather();
        time = Random.Range(RandomTimeFrom, RandomTimeTo);
        RainSound = GetComponent<AudioSource>();
    }

    void Update()
    {

        ChangeTime();
        DoNotBugOutWeather();
       

    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if(time > 86400)
        {
            days += 1;
            ChangeWeather();
            time = 0;
        }

        currenttime = TimeSpan.FromSeconds(time);
        string[] temptime = currenttime.ToString().Split(":"[0]);
        curTime = temptime[0] + ":" + temptime [1];

       

        Suntransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * 360, 0, 0));
        intensity = 1 - ((43200 - time) / 43200 * ((Mathf.Sign(time - 43200)) * -1));

        Sun.intensity = intensity;
    }

    public void ChangeWeather()
    {
        RandomWeather = Random.Range(0, 3);
        switch (RandomWeather)
        {
                case 0:
                Debug.Log("Normal");
                isNormal = true;
                isRaining = false;
                isFoggy = false;
                RainSound.Stop();
                Rain.Stop();
                break;

                case 1:
                Debug.Log("Rain");
                isNormal = false;
                isRaining = true;
                isFoggy = false;
                RainSound.Play();
                Rain.Play();
                break;

                case 2:
                Debug.Log("Fog");
                isNormal = false;
                isRaining = false;
                isFoggy = true;
                Rain.Stop();
                RainSound.Stop();
                break;


        }

    }

   

    public void DoNotBugOutWeather()
    {
        if(isNormal == true)
        {
            isRaining = false;
            isFoggy = false;
        }

        if (isRaining == true)
        {
            isNormal = false;
            isFoggy = false;
        }

        if (isFoggy == true)
        {
            isRaining = false;
            isNormal = false;
        }
    }
}

