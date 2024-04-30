using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractiveObject : MonoBehaviour
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
    protected Rigidbody2D rb;
    private GRAVITY_STATE gravityState = GRAVITY_STATE.NORMAL;
    private SCALE_STATE scaleState = SCALE_STATE.NORMAL;
    public float CurrentGravity { get => currentGravity; set { currentGravity = value; } }
    public Vector2 CurrentScale { get => currentScale; set { currentScale = value; } }
    public GRAVITY_STATE GravityState { get => gravityState; }
    public SCALE_STATE ScaleState { get => scaleState; }
    virtual public void CounterGravity()
    {
        SetGravityState(GRAVITY_STATE.COUNTER, true, gravityPower_Counter);
    }
    virtual public void ZeroGravity()
    {
        SetGravityState(GRAVITY_STATE.ZERO, false, gravityPower_Zero);
    }
    virtual public void NormalGravity()
    {
        SetGravityState(GRAVITY_STATE.NORMAL, false, gravityPower_Normal);
    }
    private void SetGravityState(GRAVITY_STATE _state, bool _rot, float _gravity)
    {
        gravityState = _state;
        this.GetComponent<SpriteRenderer>().flipY = _rot;
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
        this.transform.localScale = largeScale;
    }
    virtual public void DownSizing()
    {
        this.transform.localScale = smallScale;
    }
    protected void ResetSize()
    {
        this.transform.localScale = normalScale;
    }
}