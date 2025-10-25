using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 10;
    public Transform player;
    private bool facingRight = true;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (player.position.x > transform.position.x && !facingRight) Flip();
            else if (player.position.x < transform.position.x && facingRight) Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
