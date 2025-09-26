using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float runSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Checks")]
    public Transform groundCheck;
    public Transform ceilingCheck;
    public float checkRadius = 0.12f;
    public LayerMask groundLayer;

    [Header("Vidas e invuln")]
    public int maxLives = 2;
    public float invulnerabilityTime = 1f;

    [HideInInspector] public bool isGravityInverted = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isInvulnerable = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (rb.gravityScale == 0) rb.gravityScale = 1f;
    }

    void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.IsGameOver) return;

        if (IsJumpInput() && IsGrounded())
            Jump();
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;
        vel.x = runSpeed;
        rb.linearVelocity = vel;
    }

    bool IsJumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

   

    public bool IsGrounded()
    {
        if (!isGravityInverted)
            return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        else
            return Physics2D.OverlapCircle(ceilingCheck.position, checkRadius, groundLayer);
    }

    public void StartGravityPower(float seconds)
    {
        StopCoroutine("GravityPowerCoroutine");
        StartCoroutine(GravityPowerCoroutine(seconds));
    }

    private IEnumerator GravityPowerCoroutine(float seconds)
    {
        ToggleGravity();
        yield return new WaitForSeconds(seconds);
        ToggleGravity();
    }

    public void ToggleGravity()
    {
        isGravityInverted = !isGravityInverted;
        rb.gravityScale *= -1f;
        if (sr != null) sr.flipY = isGravityInverted;
    }

    public void ReceiveHit()
    {
        if (isInvulnerable) return;
        GameManager.Instance.LoseLife();
        StartCoroutine(InvulnerabilityRoutine());
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        float end = Time.time + invulnerabilityTime;
        while (Time.time < end)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.12f);
        }
        sr.enabled = true;
        isInvulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
            ReceiveHit();
        else if (other.CompareTag("PowerUp"))
        {
            PowerUp p = other.GetComponent<PowerUp>();
            if (p != null) p.Collect(this);
            else Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (groundCheck != null) Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.color = Color.cyan;
        if (ceilingCheck != null) Gizmos.DrawWireSphere(ceilingCheck.position, checkRadius);
    }

    public void Jump()
    {
        int gravityDir = isGravityInverted ? -1 : 1;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce * gravityDir, ForceMode2D.Impulse);
    }

}