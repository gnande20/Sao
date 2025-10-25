using UnityEngine;
using System.Collections;

public class CreatorPowers : MonoBehaviour
{
    [Header("Références")]
    public PlayerController player;
    public LayerMask enemyLayer;

    [Header("Statut")]
    public bool isCreator = false;
    private bool timeStopped = false;

    void Update()
    {
        if (!isCreator) return;

        // 🎯 Téléportation — Touche T
        if (Input.GetKeyDown(KeyCode.T))
            TeleportToMouse();

        // 💥 Explosion divine — Touche E
        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(DivineExplosion());

        // 🕰️ Stop Time — Touche Y
        if (Input.GetKeyDown(KeyCode.Y))
            StartCoroutine(StopTime(3f));

        // 💀 One Hit Kill — Touche H
        if (Input.GetKeyDown(KeyCode.H))
            OneHitKill();

        // 🌌 Mode Dieu — Touche G
        if (Input.GetKeyDown(KeyCode.G))
            StartCoroutine(GodMode(5f));
    }

    // === ⚡ Téléportation ===
    void TeleportToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
        Debug.Log("⚡ Téléportation divine !");
    }

    // === 💥 Explosion divine ===
    IEnumerator DivineExplosion()
    {
        Debug.Log("🔥 Explosion divine déclenchée !");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5f, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(99999);
        }

        // Effet visuel temporaire
        Camera.main.backgroundColor = Color.white;
        yield return new WaitForSeconds(0.1f);
        Camera.main.backgroundColor = Color.black;
    }

    // === 🕰️ Stop Time ===
    IEnumerator StopTime(float duration)
    {
        if (timeStopped) yield break;
        timeStopped = true;

        Debug.Log("🕰️ Temps figé !");
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        timeStopped = false;
        Debug.Log("▶️ Temps relancé !");
    }

    // === 💀 One Hit Kill ===
    void OneHitKill()
    {
        Debug.Log("💀 Mode One-Hit activé !");
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
        {
            enemy.TakeDamage(99999);
        }
    }

    // === 🌌 Mode Dieu ===
    IEnumerator GodMode(float duration)
    {
        Debug.Log("🌌 Mode Dieu activé !");
        player.health = 999999;
        player.moveSpeed = 15f;

        Color baseColor = player.GetComponent<SpriteRenderer>().color;
        player.GetComponent<SpriteRenderer>().color = Color.cyan;

        yield return new WaitForSeconds(duration);

        player.moveSpeed = 5f;
        player.GetComponent<SpriteRenderer>().color = baseColor;

        Debug.Log("🌌 Mode Dieu désactivé.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
