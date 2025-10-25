using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;
    public float rotateSpeed = 10f;
    public float attackRange = 1.5f;
    public int attackDamage = 25;

    CharacterController controller;
    Animator animator;
    Vector3 velocity;
    Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        ApplyGravity();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = (cam.forward * v + cam.right * h);
        dir.y = 0;
        if (dir.magnitude > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, dir.normalized, Time.deltaTime * rotateSpeed);
            controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("jump");
        }
    }

    void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("attack");
            // Call damage after small delay to sync with animation
            Invoke(nameof(PerformAttack), 0.2f);
        }
    }

    void PerformAttack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position + transform.forward * 0.8f + Vector3.up * 1f, 0.8f, transform.forward, 0f);
        foreach (var h in hits)
        {
            var enemy = h.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
