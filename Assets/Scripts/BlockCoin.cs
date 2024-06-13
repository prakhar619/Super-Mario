using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    
    // Start is called before the first frame update
    private IEnumerator Animate()
    {
        Vector3 restingPos = transform.localPosition;
        Vector3 animatedPos = restingPos + Vector3.up * 2f;
        yield return Move(restingPos,animatedPos);
        yield return Move(animatedPos,restingPos);
        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.25f;
        while(elapsed < duration)
        {
            float t= elapsed/duration;
            transform.localPosition = Vector3.Lerp(from,to,t);
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        transform.localPosition = to;
    }
    void Start()
    {
        GameManager.Instance.AddCoin();
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
