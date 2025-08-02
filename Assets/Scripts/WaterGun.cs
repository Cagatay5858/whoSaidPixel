using UnityEngine;
using UnityEngine.InputSystem;

public class WaterGun : InterfaceGuns
{
    private float damage = 75.0f;
    public GameObject bullet;
    public Transform bulletSpawnPoint;

    private GameObject bulletIns;

    public override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //spawn Bullet
            bulletIns = Instantiate(bullet, bulletSpawnPoint.position, weapon.transform.rotation);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Attack();
    }
}
