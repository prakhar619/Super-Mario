
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;
    private new Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }


    public float moveSpeed = 10f;
    private Vector2 velocity;
    private void HorizontalMovement()
    {
        float inputAxis = Input.GetAxis("Horizontal");   //-1 to 1 (negative enabled for horizontal axes input)
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis*moveSpeed, moveSpeed*Time.deltaTime);

        if(rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if(velocity.x >= 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
    }


    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpVelocity => (2f* maxJumpHeight)/(maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f),2);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > .25f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f ;
    public bool sliding =>(Input.GetAxis("Horizontal") > 0f && velocity.x < 0) || (Input.GetAxis("Horizontal") < 0f && velocity.x > 0f);
    private void GroundedMovement()
    {

        jumping  = velocity.y > 0f;
        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpVelocity;
            jumping  = true;
        }
    }
    private void ApplyGravity()
    {
        bool falling  = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * Time.deltaTime * multiplier;
        velocity.y  = Mathf.Max(velocity.y , gravity/2f);
    }

    private void FixedUpdate()
    {
        Vector2 pos= rigidbody.position;
        pos += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        pos.x = Mathf.Clamp(pos.x, leftEdge.x + 0.5f,rightEdge.x- 0.5f);
        rigidbody.MovePosition(pos);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        grounded  = rigidbody.Raycast(Vector2.down);
        if(grounded)
        {
            GroundedMovement();
        }
        ApplyGravity();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.transform.DotTest(transform,Vector2.down))
            {
                velocity.y = jumpVelocity/2;
                jumping = true;
            }
        }
        else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotTest(collision.transform,Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}


