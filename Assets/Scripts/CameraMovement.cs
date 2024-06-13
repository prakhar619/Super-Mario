using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float norm_height;
    public float underGround_height;
    // Start is called before the first frame update
    private Transform player;
    

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = Mathf.Max(player.position.x,cameraPos.x);
        transform.position = cameraPos;
    }

    public void SetUnderground(bool underground)
    {
        Vector3 cameraPos = transform.position;
        cameraPos.y = underground ? underGround_height : norm_height;
        transform.position = cameraPos;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
