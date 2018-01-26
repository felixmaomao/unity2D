using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Standard_Assets._2D.Scripts
{
    public class WeaponM16 : MonoBehaviour, IWeapon
    {
        public float fireRate = 0;
        public int Damage = 10;
        public LayerMask whatToHit;
        public Transform BulletTrailPrefab;
        public Transform MuzzleFlashPrefab;
        public Transform HitPrefab;

        float timeToFire = 0;
        Transform firePoint;

        private void Awake()
        {
            firePoint = transform.Find("FirePoint");
            if (null == firePoint)
            {
                Debug.Log("firePoint is null");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (fireRate == 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
        }

        public void Shoot()
        {
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

            //是否射击到物体，如果射击到了，那么攻击划线不能穿过去，并且在碰撞位置要放置射击碰撞特效
            Vector3 hitPos;
            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
            }
            else
            {
                hitPos = hit.point;
            }
            ShootEffect(hitPos);
            //攻击划线
            Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
            if (hit.collider != null)
            {
                Debug.DrawLine(firePointPosition, hit.point, Color.red);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(Damage);
                }
            }
            PlayShootNoise();
        }

        void ShootEffect(Vector3 hitPos)
        {
            //创建子弹飞行特效
            Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
            //射击碰撞特效
            Transform hitPrefab = Instantiate(HitPrefab, hitPos, firePoint.rotation) as Transform;
            Destroy(hitPrefab.gameObject, 1f);
            if (hitPrefab == null)
            {
                Debug.Log("hitprefab has been destroyed");
            }
            //枪口爆破特效
            Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
            clone.parent = firePoint;
            float size = UnityEngine.Random.Range(0.7f, 0.9f);
            clone.localScale = new Vector3(size, size, size);
            Destroy(clone.gameObject, 0.02f);
        }

        private void PlayShootNoise()
        {

            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource.clip == null)
            {
                Debug.LogError("none audio clip exists");
            }
            audioSource.time = 0;
            audioSource.Play();

        }
    }

}
    

