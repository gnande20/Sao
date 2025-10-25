using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Paramètres du joueur")]
    public string playerName = "Joueur";
    public float moveSpeed = 5f;
    public int health = 100;
    public int attackPower = 10;

    [Header("Mode Créateur")]
    public bool isCreator = false;
    private string creatorName = "Camille";  // Ton nom spécial
    private string creatorCode = "ADMINSAO2025"; // Ton code secret

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Active automatiquement le mode créateur si le nom correspond
        if (playerName.ToLower() == creatorName.ToLower())
        {
            ActivateCreatorMode();
        }
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Test manuel du mode créateur
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("Entrez le code du mode créateur dans la console :");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    public void ActivateCreatorMode(string code = "")
    {
        if (code == creatorCode || playerName.ToLower() == creatorName.ToLower())
        {
            isCreator = true;
            moveSpeed = 20f;
            health = 999999;
            attackPower = 9999;

            if (spriteRenderer != null)
                spriteRenderer.color = new Color(1f, 0.4f, 0.4f, 1f); // aura rouge

            Debug.Log("🔥 Mode Créateur activé pour " + playerName + " !");
        }
        else
        {
            Debug.Log("❌ Code invalide !");
        }
    }
}
