using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class RangedEnemys : MonoBehaviour
{
    public Transform Target;
    public float speed = 150f;
    public float rotateSpeed = 0.0025f;

    public float distanceToShoot = 10f;
    public float distanceToStop = 8f;

    public float fireRate;
    private float timeToFire;

    public Transform firingPoint;
    public GameObject bulletPrefab;




    public virtual void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public virtual void Shoot()
    {
        if(timeToFire <= 0f)
        {
            //ateþ Et

            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    public virtual void RotateTowardsTarget()
    {
        Vector2 targetDirection = Target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
}
