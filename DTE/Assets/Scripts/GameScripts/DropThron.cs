using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThron : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("중력값")]
    public float gravityScale = 1f;    
    public SpriteRenderer spriteRenderer;

    public Sprite newSprite;
    public Sprite oldSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        string tag = _collision.gameObject.tag;
        if (tag == "Rock")
        {
            Debug.Log("asd");
            Destroy(this.gameObject);
            Destroy(_collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (gameObject.CompareTag("Thron") && _collision.gameObject.CompareTag("Rock"))
        {
            Destroy(_collision.gameObject);
            Destroy(gameObject);
        }
    }



    public void MovingDrop()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector2.up * 2f, LayerMask.GetMask("BreakBlock"));
        Debug.DrawRay(rb.position, Vector2.up * 2f, Color.green); // 레이를 그릴 때는 오브젝트 방향으로 그리는 게 좋습니다.
        if (rayHit.collider != null) // rayHit이 null이 아닌지 확인합니다.
        {
            rb.gravityScale = gravityScale;
        }
        if (rayHit.collider == null) // rayHit이 null이 아닌지 확인합니다.
        {
            rb.gravityScale = 0f;
        }
    }

    public void ChangeGravity(float _value)
    {
        gravityScale = _value;
    }
}
