using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    private SpriteRenderer sr;

    private int damage;
    [SerializeField] private string targetLayerName = "Enemy";


    private float arrowSpeed;
    private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private CharacterStats stats;
    private int facingDir = 1;


    private void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(arrowSpeed, rb.velocity.y);

        if (facingDir == 1 && rb.velocity.x < 0)
        {
            facingDir = -1;
            sr.flipX = true;
        }
    }

    public void SetupArrow(float _speed, CharacterStats _stats)
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        arrowSpeed = _speed;
        stats = _stats;
        // AudioManager.instance.PlaySFX(35, null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStats>()?.isInvincible == true)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {

            collision.GetComponent<CharacterStats>()?.TakeDamage(damage);


            stats.DoDamage(collision.GetComponent<CharacterStats>());

            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        // GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        // canMove = false;
        // rb.isKinematic = true;
        // rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // transform.parent = collision.transform;
        // AudioManager.instance.PlaySFX(36, null);
        Destroy(gameObject, Random.Range(5, 7));
    }

    public void FlipArrow()
    {
        if (flipped)
            return;

        arrowSpeed = arrowSpeed * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayerName = "Enemy";
    }
}
