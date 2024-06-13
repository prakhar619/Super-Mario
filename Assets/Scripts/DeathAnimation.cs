using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeathAnimation: MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false;
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhyicsx();
        StartCoroutine(nameof(Animate));
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
        spriteRenderer.sprite = deadSprite;
    }

    private void DisablePhyicsx()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = false;
        PlayerMovement playerMovement= GetComponent<PlayerMovement>();
        EntityMovement entityMovement= GetComponent<EntityMovement>();
        if(playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if(entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration  = 3f;
        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while(elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

    }
}