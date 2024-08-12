using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : CharacterStateManager
{
    public enum GRAVITY_STATE
    {
        NORMAL, ZERO, COUNTER,
    }

    public enum SCALE_STATE
    {
        NORMAL, SMALL, LARGE,
    }

    [SerializeField] protected float gravityPower_Normal = 1f;              // 기본중력
    [SerializeField] protected float gravityPower_Zero = 0.1f;              // 무중력
    [SerializeField] protected float gravityPower_Counter = -1f;            // 역중력
    private float currentGravity = 1f;

    [SerializeField] protected Vector2 smallScale = new Vector2(0.5f, 0.5f);// 작아진 크기
    [SerializeField] protected Vector2 largeScale = new Vector2(1.5f, 1.5f);// 커진 크기

    protected Vector2 normalScale = new Vector2(1f, 1f);
    private Vector2 currentScale;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private GRAVITY_STATE gravityState = GRAVITY_STATE.NORMAL;
    [SerializeField] private SCALE_STATE scaleState = SCALE_STATE.NORMAL;


    public float CurrentGravity { get => currentGravity < 0 ? -1 : 1; set { currentGravity = value; } }
    public Vector2 CurrentScale { get => currentScale; set { currentScale = value; } }
    public GRAVITY_STATE GravityState { get => gravityState; }
    public SCALE_STATE ScaleState { get => scaleState; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; }


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    virtual public void CounterGravity()
    {
        float prevGravity = CurrentGravity;
        SetGravityState(GRAVITY_STATE.COUNTER, gravityPower_Counter);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * prevGravity * CurrentGravity, transform.localScale.z);
    }

    virtual public void ZeroGravity()
    {
        float prevGravity = CurrentGravity;
        SetGravityState(GRAVITY_STATE.ZERO, gravityPower_Zero);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * prevGravity * CurrentGravity, transform.localScale.z);
    }

    virtual public void NormalGravity()
    {
        float prevGravity = CurrentGravity;
        SetGravityState(GRAVITY_STATE.NORMAL, gravityPower_Normal);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * prevGravity * CurrentGravity, transform.localScale.z);
    }

    public void SetGravityState(GRAVITY_STATE _state, float _gravity)
    {
        gravityState = _state;
        //spriteRenderer.flipY = _rot;

        SetGravity(_gravity);
    }

    protected void SetGravity(float _gravity)
    {
        currentGravity = _gravity;
        ResetGravity();
    }

    protected void ResetGravity()
    {
        rb.gravityScale = currentGravity;
    }
    virtual public void UpSizing()
    {
        if (transform.localScale.x == largeScale.x)
            return;
        this.transform.position += new Vector3(0f, largeScale.y * 0.5f * CurrentGravity, 0f);
        this.transform.localScale = largeScale;
        
        scaleState = SCALE_STATE.LARGE;
    }

    virtual public void DownSizing()
    {
        if (transform.localScale.x == smallScale.x)
            return;
        this.transform.localScale = smallScale;
        rb.drag = 0f;
        scaleState = SCALE_STATE.SMALL;
    }

    virtual public void NormalSize()
    {
        if (transform.localScale.x == normalScale.x)
            return;
        if (transform.localScale.x == smallScale.x)
            this.transform.position += new Vector3(0f, normalScale.y * 0.5f * CurrentGravity, 0f);
        rb.drag = 0f;
        this.transform.localScale = normalScale;
        scaleState = SCALE_STATE.NORMAL;
    }
    protected void ResetSize()
    {
        this.transform.localScale = normalScale;
        scaleState = SCALE_STATE.NORMAL;
    }

    virtual public void ChangeColdWeather()
    {

    }

    virtual public void ChangeHotWeather()
    {

    }
}
