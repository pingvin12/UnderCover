using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

//Fényes József - Játékos fegyverszkript
public class PlayerShoot : MonoBehaviour
{
    

    private Animation anim;

    [SerializeField]
    private Camera cam;
    EnemyStats EnemyStat;

    [Header("Játékos GUI")]
    public Text MagCount;
    public Text MagSoze;
    
   
    [Space]

    [Header("Fegyver tulajdonságok")]

    
    public float Range;
    public Transform FirePoint;
    AudioSource audiosource;
    public float GunDamage = 50f;
    public AudioClip ShootSound;
    public AudioClip ReloadSound;
    public AudioClip EmptyClipSound;
    public ParticleSystem fireGFX;
    private int magSize = 8;
    private int magCount = 5;
    private float SpecialAttackTimer = 20f;
    public LayerMask EnemyMask;
    Ray shootRay;
    private bool isAiming = false;
    public float fireRate = -1f;

   



    [Header("Kamera Tulajdonságai")]
    private Camera cameraFreeWalk;
    private Camera GunCamera;
    public float zoomSpeed = 20f;
    public float minZoomFOV = 10f;
    public GameObject CrossHair;

    [Header("Játékos Tulajdonságok")]
    public Text HealthUi;
    public float Health = 200;


    [Header("Fegyver mozgatás elmosódása")]

    public float MoveAmount = 1;
    public float MoveSpeed = 2;
    public float MoveOnX;
    public float MoveOnY;
    public Vector3 DefaultPos;
    public Vector3 NewGunPos;
    private bool isMoving = false;


    public void ZoomIn()
    {
        while (cameraFreeWalk.fieldOfView == 45 && GunCamera.fieldOfView == 45)
        {
            cameraFreeWalk.fieldOfView -= zoomSpeed / 8;
            GunCamera.fieldOfView -= zoomSpeed / 8;
        }

        if (cameraFreeWalk.fieldOfView < minZoomFOV && GunCamera.fieldOfView < minZoomFOV)
        {
            GunCamera.fieldOfView = minZoomFOV;
            cameraFreeWalk.fieldOfView = minZoomFOV;
        }
    }

    public void ZoomOut()
    {
        GunCamera.fieldOfView = 80;
        cameraFreeWalk.fieldOfView = 80;
        if (cameraFreeWalk.fieldOfView < minZoomFOV || GunCamera.fieldOfView < minZoomFOV)
        {
            GunCamera.fieldOfView = minZoomFOV;
            cameraFreeWalk.fieldOfView = minZoomFOV;
        }
    }


    void Awake()
    {
        fireRate = Time.time;

        GunCamera = GameObject.Find("GunCamera").GetComponent<Camera>();
        audiosource = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        DefaultPos = transform.localPosition;
        cam = Camera.main;
       

        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No Camera Referenced!");
            this.enabled = false;
        }

        cameraFreeWalk = Camera.main;

    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
       

        HealthUi.text = Health.ToString() + " HP";
        MagSoze.text = magSize.ToString() + "/";
        MagCount.text = magCount.ToString();
        Mathf.Clamp(Health, 0, 100);



        if (Health <= 0)
        {
            Destroy(gameObject);
        }




       

        if (isMoving) {
            MoveOnX = Input.GetAxis("Mouse X") * Time.deltaTime * MoveAmount;

            MoveOnY = Input.GetAxis("Mouse Y") * Time.deltaTime * MoveAmount;

            NewGunPos = new Vector3(DefaultPos.x + MoveOnX, DefaultPos.y + MoveOnY, DefaultPos.z);
        }


        if (!isMoving) { }

        if (NewGunPos.x >= 0 && NewGunPos.y >= 0)
        {


            this.anim.enabled = true;
        }


        gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);






        if (Time.time >= fireRate)
        {

            if (magCount >= 0 && magSize > 0)
            {

                if (Input.GetButtonDown("Fire1") && magSize >= 0)
                {

                    RaycastHit _hit;
                    fireRate = Time.time + 1;
                    audiosource.PlayOneShot(ShootSound, 0.7f);
                    anim.Play("shotgunshoot");
                    Camera.main.transform.Rotate(-1, 0, 0);
                    (Instantiate(fireGFX, FirePoint.position, FirePoint.rotation) as ParticleSystem).transform.parent = FirePoint.transform;

                    magSize--;

                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, Range))
                    {
                        Debug.Log("We hit " + _hit.collider.name);


                        EnemyStats EnemyStat = _hit.transform.GetComponent<EnemyStats>();
                        if (EnemyStat != null)
                        {
                            EnemyStat.TakeDamage(GunDamage);
                        }








                    }
                }

            }
        }

        if (Input.GetButtonDown("Fire1") && magSize == 0 && magCount == 0)
        {
            audiosource.PlayOneShot(EmptyClipSound, 0.7f);
        }

        if (magSize < 8 && magSize >= 0 && Input.GetButtonDown("Reload") && magCount != 0)
        {
            audiosource.PlayOneShot(ReloadSound, 0.7f);
            magCount--;
            magSize = 8;

            anim.Play("shotgunreload");
        }
    

                    if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1F && (!Input.GetButton("Sprint")))
                    {
                anim.Play("shotgunwalk");
            }
            else { anim.Stop("shotgunwalk"); }

                    if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1F && Input.GetButton("Sprint"))
                    {
                anim.Play("shotgunsprint");
            }
            else { anim.Stop("shotgunsprint"); }

                    

                    
                }

            
    IEnumerator DestroyMuzzleFlash()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(fireGFX);
        
    }
 }
    

    
