using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;

    public delegate void CallbackFunc();

    Dictionary<string, CallbackFunc> dicCommands = new Dictionary<string, CallbackFunc>();

    [SerializeField]
    private float jumpSpeed = 0.5f;
    [SerializeField]
    private float speed = 10f;


    private bool isJump = false;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y <= 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                Debug.Log(rayHit.distance);
                if (rayHit.distance < 1.6f)
                {
                    Debug.Log("Ground");
                    isJump = false;
                }
            }
        }
            
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(rb.gravityScale < 0f)
                rb.gravityScale = 1f;
            else
                rb.gravityScale = -1f;


        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.W))
        {
            Jump();
        }

        
    }


    private void Jump()
    {
        if (isJump == true) return;

        isJump = true;

        

        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
            isJump = false;
    }
}
