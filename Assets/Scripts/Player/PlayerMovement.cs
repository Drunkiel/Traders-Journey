using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    [SerializeField] private Rigidbody2D rgBody;
    private Vector2 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        movement.Normalize();

        Vector2 clampedVelocity = movement * speed;

        if (clampedVelocity.magnitude > maxSpeed)
        {
            clampedVelocity = clampedVelocity.normalized * maxSpeed;
        }

        rgBody.MovePosition(rgBody.position + clampedVelocity * Time.fixedDeltaTime);
    }
}

