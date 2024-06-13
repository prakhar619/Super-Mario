using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spRenderer;
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
        run = GetComponent<AnimatedSprite>();
    }

    private void OnEnable()
    {
        spRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        if(movement.jumping)
        {
            spRenderer.sprite = jump;
        }
        else if(movement.sliding)
        {
            spRenderer.sprite = slide;
        }
        else if(movement.running)
        {
            run.enabled = true;
        }
        else
        {
            spRenderer.sprite = idle;
        }
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
