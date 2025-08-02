using UnityEngine;

public interface InterfaceEnemy 
{
    void SetHealth(float health);
    void SetDamage();
    void SetRange();
    void Attack();
    void DamageTaken();
}
