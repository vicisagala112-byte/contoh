using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Joystick joystick;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    [Header("Boat Settings")]
    public bool isOnBoat = false;   // apakah player di kapal?
    public GameObject boat;         // referensi kapal (bisa prefab atau child dari player)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (boat != null)
            boat.SetActive(false); // awalnya kapal disembunyikan
    }

    void Update()
    {
        // Input
        float moveX = joystick != null ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");
        float moveY = joystick != null ? joystick.Vertical : Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        // Animator
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // === Masuk Sungai ===
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sungai"))
        {
            isOnBoat = true;
            if (boat != null) boat.SetActive(true);  // munculkan kapal
            Debug.Log("Player naik kapal!");
        }
    }

    // === Keluar Sungai ===
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sungai"))
        {
            isOnBoat = false;
            if (boat != null) boat.SetActive(false); // sembunyikan kapal
            Debug.Log("Player turun kapal!");
        }
    }
}
