using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TODO: fix assigning scripts and removing from ints
//Fényes József - Ellenséges AI(mesterséges inteligencia) script
public class AgressiveAI : MonoBehaviour
{
    EnemyStats Stats;
    GMananger GM;

    public bool isAlerted = false;
    public float distance;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform target;
    public AudioClip angry;
    public AudioClip shoot;
    public AudioSource audioc;
    public float wanderRadius = 50f;
    public AudioClip mad;
    public Transform ShootingPoint;
    public ParticleSystem muzzleFlash;
    public Transform WeaponHolder;
    int index;

    private float secondsBetweenShots = 1f;

    public bool isReloading = false;

    void Start()
    {
        Stats = new EnemyStats();
        target = GameObject.FindWithTag("Player").transform;

        audioc = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Stats = GetComponent<EnemyStats>();

        agent.autoBraking = false;
        Instantiate(Stats.Weapons[Random.Range(0, Stats.Weapons.Length)] , WeaponHolder.transform.position, WeaponHolder.transform.rotation,transform);
        Stats.detectionLevel = 0;
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


        if (agent.remainingDistance < 1f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -5);
            agent.SetDestination(newPos);
        }

        if(Stats.Health <= 70)
        {
            Stats.detectionLevel += 100;
        }

        if(Stats.detectionLevel >= 100) {
            agent.destination = target.position;
            audioc.clip = angry;
            audioc.Play();
            

            if (distance <= 15f || distance == 15f)
            {
                agent.updatePosition = false;
                agent.updateRotation = true;
                for (int i = 0; i < Stats.curAmmo; i++)
                {
                    StartCoroutine(Shoot());
                }
            } else

            {
                agent.updatePosition = true;
                agent.updateRotation = true;

            }
        }
        
    }

    IEnumerator Shoot()
    {
        if (Stats.curAmmo > 0 && Stats.maxClips >= 0)
        {
            audioc.clip = mad;
            audioc.Play();
            yield return new WaitForSeconds(secondsBetweenShots);

            RaycastHit _hit;


            if (Physics.Raycast(ShootingPoint.transform.position, ShootingPoint.transform.forward, out _hit, Stats.ShootRange))
            {
                Debug.Log("The AI hit " + _hit.collider.name);

              
                    muzzleFlash.Play();
                
                Stats.curAmmo -= 1;

                PlayerShoot PlayerShoot = _hit.transform.GetComponent<PlayerShoot>();
                if (PlayerShoot != null)
                {
                    PlayerShoot.TakeDamage(Stats.maxDamage);
                }
            }

            
        }

        if(Stats.maxClips <= 0)
        {
            Stats.maxClips = 0;
        }

        if(Stats.curAmmo == 0 && Stats.maxClips >=0)
        {
            
            Stats.maxClips -= 1;
            if (Stats.maxClips >= 0)
            {
                Stats.curAmmo += 30;
            }
        }

        if (Stats.maxClips == 0)
        {
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
