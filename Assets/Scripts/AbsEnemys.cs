using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class AbsEnemys : MonoBehaviour
{
    public GameObject Target;
    public float speed = 150f;
    public float rotateSpeed = 0.0025f;
    private bool hasOnSight = false;

    public float distanceToShoot = 10f;
    public float distanceToStop = 8f;

    public float fireRate;
    private float timeToFire;

    private GameObject bulletIns;
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public GameObject enemyWeapon;

    public bool getHasOnSight()
    {
        return hasOnSight;
    }

    public virtual void Shoot()
    {
        if (timeToFire <= 0f)
        {
            //ateþ Et
            bulletIns = Instantiate(bulletPrefab, firingPoint.position, enemyWeapon.transform.rotation);
            timeToFire = fireRate;

        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    public virtual void RotateTowardsTarget()
    {
        Vector2 targetDirection = Target.transform.position - enemyWeapon.transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // Silahý döndür
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        enemyWeapon.transform.localRotation = Quaternion.Slerp(enemyWeapon.transform.localRotation, q, rotateSpeed);

        // Karakter yönünü deðiþtirme kýsmý
        if (angle > 90 || angle < -90)
        {
            // Sola bak
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // Saða bak
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public virtual void MakeRay()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Target.transform.position - transform.position);
        if(ray.collider != null)
        {
            hasOnSight = ray.collider.CompareTag("Player");
            if (hasOnSight)
            {
                Debug.DrawRay(transform.position, Target.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, Target.transform.position - transform.position, Color.red);
            }
        }
    }
}
