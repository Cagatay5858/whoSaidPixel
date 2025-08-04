using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class RangedEnemys : MonoBehaviour
{
    public GameObject Target;
    public float speed = 150f;
    public float rotateSpeed = 0.0025f;

    public float distanceToShoot = 10f;
    public float distanceToStop = 8f;

    public float fireRate;
    private float timeToFire;

    private GameObject bulletIns;
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public GameObject enemyWeapon;



    public virtual void Shoot()
    {
        if(timeToFire <= 0f)
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
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        enemyWeapon.transform.localRotation = Quaternion.Slerp(enemyWeapon.transform.localRotation, q, rotateSpeed);
    }
}
