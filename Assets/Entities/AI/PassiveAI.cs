using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassiveAI : MonoBehaviour {

    CivilianStats Stats;

    public float distance;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform target;
    public AudioSource audioc;
    public float wanderRadius = 50f;
    public AudioClip mad;
    public AudioClip frightened;
    public Transform ShootingPoint;
    public GameObject muzzleFlash;
    private bool scared = false;

    void Start()
    {
        Stats = new CivilianStats();


        audioc = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Stats = GetComponent<CivilianStats>();

        agent.autoBraking = false;

    }

    public void TakeDamage()
    {
        Stats.Health -= 50;

    }


    void Update()
    {

        distance = Vector3.Distance(transform.position, target.position);

        agent.isStopped = false;

        if (Stats.Health <= 0)
        {
            Destroy(gameObject);
        }


        if (agent.remainingDistance < 0.5f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
        }

        if (distance <= 25f)
        {
            agent.destination = target.position;
            audioc.Play();
            agent.speed = 5;

            if (distance <= 10f)
            {
                agent.speed = 15;
                agent.isStopped = false;
                scared = true;
                if (scared == true)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                } else
                {
                    agent.isStopped = true;
                }
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
