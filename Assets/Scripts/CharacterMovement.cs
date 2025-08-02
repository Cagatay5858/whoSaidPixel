using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    private float horizontal;
    private float vertical;
    public float speed = 100.0f;
    public Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
    }
    private void FixedUpdate()
    {
        movement = new Vector2(horizontal, vertical);
        rigidbody.linearVelocity = new Vector2(horizontal * speed, vertical * speed) * Time.fixedDeltaTime;
    }
}
