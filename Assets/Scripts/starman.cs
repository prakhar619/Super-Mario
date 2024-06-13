using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starman : MonoBehaviour
{

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D phyCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        phyCollider.enabled = false;
        triggerCollider.enabled = false;
        sp.enabled = false;
        
        yield return new  WaitForSeconds(0.25f);
        sp.enabled  = true;

        float elapsed  = 0f;
        float duration = 0.5f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + Vector3.up;

        while(elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(startPos,endPos,t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rigidbody.isKinematic = false;
        phyCollider.enabled = true;
        triggerCollider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        Destroy(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
