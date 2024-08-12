using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rock : InteractiveObject
{
    private Transform rock;
    //[SerializeField]private SpriteRenderer rocksSpriteRenderer;
    private Vector3 smallSize = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 defaultSize = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 bigSize = new Vector3(1.5f, 1.5f, 1.5f);
    private int sizeState = 1;

    public Sprite coldSprite;
    public Sprite hotSprite;
    // 0 스몰사이즈, 1 기본사이즈, 2빅사이즈
    private void Awake()
    {
        rock = GetComponent<Transform>();
        //rocksSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        hotSprite = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
    }



    override public void CounterGravity()
    {
        if (GravityState == GRAVITY_STATE.COUNTER)
            return;

        float y = this.transform.GetComponent<PolygonCollider2D>().bounds.max.y - this.transform.GetComponent<PolygonCollider2D>().bounds.min.y;
        this.transform.position += new Vector3(0f, y, 0f);
        this.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        base.SetGravityState(GRAVITY_STATE.COUNTER, gravityPower_Counter);
    }

    public override void NormalGravity()
    {
        if (GravityState == GRAVITY_STATE.NORMAL)
            return;
        if (GravityState != GRAVITY_STATE.ZERO)
        {
            float y = this.transform.GetComponent<PolygonCollider2D>().bounds.max.y - this.transform.GetComponent<PolygonCollider2D>().bounds.min.y;
            this.transform.position -= new Vector3(0f, y, 0f);
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
           
        base.SetGravityState(GRAVITY_STATE.NORMAL, gravityPower_Normal);
    }

    public override void ZeroGravity()
    {
        if (GravityState == GRAVITY_STATE.ZERO)
            return;
        if(GravityState != GRAVITY_STATE.NORMAL)
        {
            float y = this.transform.GetComponent<PolygonCollider2D>().bounds.max.y - this.transform.GetComponent<PolygonCollider2D>().bounds.min.y;
            this.transform.position -= new Vector3(0f, y, 0f);
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        base.SetGravityState(GRAVITY_STATE.ZERO, gravityPower_Zero);
    }


   

   
  


    override public void ChangeColdWeather()
    {
        //부서지는 블럭 의 길이를 검사하고 그 블럭의 스프라이트 렌더가 널이 아니면 스프라이트 교체
        SpriteRenderer.sprite = coldSprite;
    }

    public override void ChangeHotWeather()
    {
        SpriteRenderer.sprite = hotSprite;
    }
}
