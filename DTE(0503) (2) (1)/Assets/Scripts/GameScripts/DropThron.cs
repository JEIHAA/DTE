using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThron : InteractiveObject
{
    public Sprite coldSprite;
    public Sprite hotSprite;


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        string tag = _collision.gameObject.tag;
        if (tag == "Rock" || tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }
    }

    
    public override void CounterGravity()
    {
        SetGravityState(GRAVITY_STATE.COUNTER, gravityPower_Counter);
    }

    public override void NormalGravity()
    {
        SetGravityState(GRAVITY_STATE.NORMAL, gravityPower_Normal);
    }
    public override void ZeroGravity()
    {
        SetGravityState(GRAVITY_STATE.ZERO, gravityPower_Zero);
    }

    public override void DownSizing()
    {
    }
    public override void UpSizing()
    {
    }
    public void MovingDrop()
    {
        rb.gravityScale = CurrentGravity;
        rb.bodyType = RigidbodyType2D.Dynamic;
        //StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSecondsRealtime(5f);
        this.gameObject.SetActive(false);
    }

    public override void ChangeColdWeather()
    {
        spriteRenderer.sprite = coldSprite;
    }
    public override void ChangeHotWeather()
    {
        spriteRenderer.sprite = hotSprite;
    }
}
