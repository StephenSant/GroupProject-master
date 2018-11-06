using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperWeapon : MonoBehaviour
{
    #region Weapon Stats
    [Header("Weapon Stats")]
    public float damage = 80f;
    public int currentAmmo, firedShots, remainingAmmo;
    public float reloadTime = 3.1f;
    public float delayBetweenShots = 0.2f;
    public float fireRate = 1f;
    public int magCap = 10;
    public float weaponRange = 100;
    #endregion
    #region Weapon Components
    [Header("Weapon Components")]
    public Transform muzzle;
    //public SphereCollider soundCollider;
    public GameObject soundCollision;
    #endregion
    #region Player Cam
    public Camera playerCam;
    #endregion
    #region Weapon Functions
    public float nextFire;
    public Text ammoText;
    public Transform laserSight;
    public AudioSource soundSource;
    public AudioClip[] soundclips;
    private bool reloading;
    private float rayDistance = 100f;
    private bool canFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);
    #endregion




    // Use this for initialization
    void Start()
    {

        currentAmmo = magCap;
        playerCam = Camera.main;
        soundSource = GameObject.Find("SniperSounds").GetComponent<AudioSource>();
        //soundCollider.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Ray aimray = new Ray(transform.position, Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(aimray.origin, aimray.origin + aimray.direction * rayDistance);
    }

    private void LateUpdate()
    {
        // Detect collision with wall (Raycast to wall)
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange);
        // If Raycast hits wall
        if (hit.collider)
        {// Rotate gun to hit point - Quaternion.LookRotation(direction)
            Vector3 relativePos = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = hit.point;
        }
        else
        {
            Vector3 relativePos = playerCam.transform.forward * weaponRange;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = playerCam.transform.forward * weaponRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse button down
        // Shoot bullet

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire && currentAmmo > 0 && Time.timeScale == 1 && !reloading)
        {
            Shoot();
            currentAmmo -= 1;
            firedShots += 1;

        }
        if (reloading)
        {
            ammoText.color = Color.red;
        }
        else if (reloading == false)
        {
            ammoText.color = Color.black;
            StopCoroutine(ReloadingSequence());
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magCap)
        {
            StartCoroutine(ReloadingSequence());
        }
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo <= 0 && remainingAmmo > 0
             && Time.timeScale == 1)
        {
            StartCoroutine(ReloadingSequence());
        }
        if (remainingAmmo <= 0)
        {
            remainingAmmo = 0;
        }
        AmmoLoadedText();
    }
    void Shoot()
    {
        nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());
        SpawnCollider();
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange))
        {
            Vector3 direction = (hit.point - muzzle.position).normalized;
        }
        else
        {
        }
    }

    private IEnumerator ShotEffect()
    {
        if (reloading == false)
        {
            soundSource.clip = soundclips[0];
            soundSource.Play();
        }

        yield return shotDuration;


    }
    private IEnumerator ReloadingSequence()
    {
        if (!reloading)
        {
            soundSource.clip = soundclips[1];
            soundSource.Play();
        }


        reloading = true;
        yield return new WaitForSeconds(3.5f);
        if (currentAmmo > 0)
        {
            remainingAmmo -= firedShots;
            currentAmmo = magCap;
        }
        if (currentAmmo <= 0)
        {
            remainingAmmo -= magCap;
            currentAmmo = magCap;
        }
        if (currentAmmo > 0 && remainingAmmo >= 0)
        {
            currentAmmo += remainingAmmo;
            remainingAmmo -= firedShots;
        }
        firedShots = 0;
        reloading = false;

    }
    public void AmmoLoadedText()
    {
        ammoText.text = "" + currentAmmo.ToString();
    }
    //public void SpawnCollider()
    //{
    //    GameObject soundCollision = new GameObject("SphereBubble");
    //    soundCollision.transform.position = gameObject.transform.position;
    //    soundCollision.AddComponent<SoundBubble>().SpawnCollider();
    //}
    public void SpawnCollider()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                hitColliders[i].SendMessage("Seek");
            }
            i++;
        }
    }
}
