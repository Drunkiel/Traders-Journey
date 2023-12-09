using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    [SerializeField] private Rigidbody2D rgBody;
    [SerializeField] private Animator anim;
    private Vector2 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
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

