using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public abstract class InterfaceGuns : MonoBehaviour
{
    public SpriteRenderer character;
    public GameObject weapon;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    public abstract void Attack();

    public virtual void Aim()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)weapon.transform.position).normalized;
        weapon.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if(angle >90 || angle < -90)
        {
            localScale.y = -1f;
            character.flipX = true;
        }
        else
        {
            localScale.y = 1f;
            character.flipX = false;
        }
        weapon.transform.localScale = localScale;
        //if (characterSprite != null)
        //{
        //    characterSprite.flipX = (mousePos.x < transform.position.x);
        //    weaponSprite.flipY = (mousePos.x < transform.position.x);
       // }
    }
}
