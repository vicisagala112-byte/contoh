using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Joystick joystick; // drag joystick ke sini (jika pakai joystick)

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // === Input ===
        // Kalau pakai joystick
        float moveX = joystick != null ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");
        float moveY = joystick != null ? joystick.Vertical : Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        // === Animator Parameter ===
        bool isMoving = movement.magnitude > 0;
        anim.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            anim.SetFloat("moveX", movement.x);
            anim.SetFloat("moveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        // === Movement ===
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
