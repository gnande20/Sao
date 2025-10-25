using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp = 100;
    int currentHp;

    void Start() { currentHp = maxHp; }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        Debug.Log($"{gameObject.name} took {amount} dmg, hp={currentHp}");
        if (currentHp <= 0) Die();
    }

    void Die()
    {
        // simple death
        Destroy(gameObject);
    }
}
