using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;
    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;


    float timeToFire = 0;
    Transform firePoint;
    private AudioClip PistolAudioClip;

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (null==firePoint)
        {
            Debug.Log("firePoint is null");
        }
        PistolAudioClip = Resources.Load("pistol") as AudioClip;
    }

    // Update is called once per frame
    void Update () {      
        if (fireRate==0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1")&&Time.time>timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }		
	}

    private void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        ShootEffect();
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider!=null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("we have hit " + hit.collider.name + " and caused " + Damage + "damage.");
        }
        PlayShootNoise();
    }

    void ShootEffect()
    {       
        Instantiate(BulletTrailPrefab,firePoint.position,firePoint.rotation);
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.7f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }

    private void PlayShootNoise()
    {
        if (PistolAudioClip != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = PistolAudioClip;
            audioSource.time = 0;
            audioSource.Play();
        }
    }
}
