using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBarrier : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.ResetLvl(3);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
