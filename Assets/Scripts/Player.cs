using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer largeRenderer;
    private PlayerSpriteRenderer activeRenderer;
    private CapsuleCollider2D capCollider;
    private DeathAnimation deathAnimation;

    public bool big => largeRenderer.enabled;
    public bool small => smallRenderer.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }
    public void Hit()
    {
        if(big)
        {
            Shrink();
        }
        else if(small)
        {
            Death();
        }
    }
    private void Death()
        {
            smallRenderer.enabled = false;
            largeRenderer.enabled = false;
            deathAnimation.enabled = true;

            GameManager.Instance.ResetLvl(3);
        }

    public void Grow()
    {
        smallRenderer.enabled = false;
        largeRenderer.enabled = true;
        activeRenderer = largeRenderer;

        capCollider.size = new Vector2(1f,2f);
        capCollider.offset = new Vector2(0f,0.5f);
        StartCoroutine(ScaleAnimation());

    }
    public void Shrink()
    {
        smallRenderer.enabled = true;
        largeRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capCollider.size = new Vector2(1f,1f);
        capCollider.offset = new Vector2(0f,0.0f);
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = ! smallRenderer.enabled;
                largeRenderer.enabled = ! smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        largeRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
