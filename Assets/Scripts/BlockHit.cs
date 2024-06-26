using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public int maxHits = -1;
    public Sprite emptyBlock;
    private bool animating;
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(!animating && maxHits != 0 && collider.gameObject.CompareTag("Player"))
        {
            if(collider.transform.DotTest(transform,Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        maxHits--;
        if(maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }
        if(item != null)
        {
            Instantiate(item,transform.position, Quaternion.identity);

        }
        StartCoroutine(Animate());

    }

    private IEnumerator Animate()
    {
        animating = true;
        Vector3 restingPos = transform.localPosition;
        Vector3 animatedPos = restingPos + Vector3.up * 0.5f;
        yield return Move(restingPos,animatedPos);
        yield return Move(animatedPos,restingPos);
        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;
        while(elapsed < duration)
        {
            float t= elapsed/duration;
            transform.localPosition = Vector3.Lerp(from,to,t);
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        transform.localPosition = to;
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
