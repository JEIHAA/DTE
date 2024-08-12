using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInformation : MonoBehaviour
{
    public struct Info
    {
        public float speed;
        public float jumpPower;
        public float jumpHeight;
        public float landingSpeed;
        public float airTime;

        public Info(float _speed, float _jumpPower, float _jumpHeight, float _landingSpeed, float _airTime)
        {
            speed = _speed;
            jumpPower = _jumpPower;
            jumpHeight = _jumpHeight;
            landingSpeed = _landingSpeed;
            airTime = _airTime;
        }
    }
    enum STATE
    {
        NN, SN, LN, NZ, SZ, LZ,
    }

    private Info[] info = new Info[6];


    [SerializeField] CharacterController player;
    [Space(7f)]
    [Header("일반사이즈 / 일반중력")]
    [SerializeField] float NN_speed = 10f;            //이속
    [SerializeField] float NN_jumpPower = 7f;       //점프파워
    [SerializeField] float NN_jumpHeight = 2.5f;      //점프높이
    [SerializeField] float NN_landingSpeed = -15f;    //착지속도
    [SerializeField] float NN_airTime = 2f;         //체공시간
    [Header("스몰사이즈 / 일반중력")]
    [SerializeField] float SN_speed = 15f;           //이속
    [SerializeField] float SN_jumpPower = 10f;       //점프파워
    [SerializeField] float SN_jumpHeight = 5f;      //점프높이
    [SerializeField] float SN_landingSpeed = -10f;    //착지속도
    [SerializeField] float SN_airTime = 2f;         //체공시간
    [Header("라지사이즈 / 일반중력")]
    [SerializeField] float LN_speed = 7f;           //이속
    [SerializeField] float LN_jumpPower = 12f;       //점프파워
    [SerializeField] float LN_jumpHeight = 2f;      //점프높이
    [SerializeField] float LN_landingSpeed = -5f;    //착지속도
    [SerializeField] float LN_airTime = 2f;         //체공시간
    [Header("일반사이즈 / 무중력")]
    [SerializeField] float NZ_speed = 10f;           //이속
    [SerializeField] float NZ_jumpPower = 20f;       //점프파워
    [SerializeField] float NZ_jumpHeight = 4f;      //점프높이
    [SerializeField] float NZ_landingSpeed = -30f;     //착지속도
    [SerializeField] float NZ_airTime = 5f;         //체공시간
    [Header("스몰사이즈 / 무중력")]
    [SerializeField] float SZ_speed = 15f;           //이속
    [SerializeField] float SZ_jumpPower = 10f;       //점프파워
    [SerializeField] float SZ_jumpHeight = 8f;      //점프높이
    [SerializeField] float SZ_landingSpeed = -20f;    //착지속도
    [SerializeField] float SZ_airTime = 5f;         //체공시간
    [Header("라지사이즈 / 무중력")]
    [SerializeField] float LZ_speed = 7f;           //이속
    [SerializeField] float LZ_jumpPower = 12f;       //점프파워
    [SerializeField] float LZ_jumpHeight = 4f;      //점프높이
    [SerializeField] float LZ_landingSpeed = -10f;    //착지속도
    [SerializeField] float LZ_airTime = 5f;         //체공시간


    

    private void Start()
    {
        info[(int)STATE.NN] = new Info(NN_speed, NN_jumpPower, NN_jumpHeight, NN_landingSpeed, NN_airTime);
        info[(int)STATE.SN] = new Info(SN_speed, SN_jumpPower, SN_jumpHeight, SN_landingSpeed, SN_airTime);
        info[(int)STATE.LN] = new Info(LN_speed, LN_jumpPower, LN_jumpHeight, LN_landingSpeed, LN_airTime);
        info[(int)STATE.NZ] = new Info(NZ_speed, NZ_jumpPower, NZ_jumpHeight, NZ_landingSpeed, NZ_airTime);
        info[(int)STATE.SZ] = new Info(SZ_speed, SZ_jumpPower, SZ_jumpHeight, SZ_landingSpeed, SZ_airTime);
        info[(int)STATE.LZ] = new Info(LZ_speed, LZ_jumpPower, LZ_jumpHeight, LZ_landingSpeed, LZ_airTime);

        ChangeGravity();
        ChangeSize();
    }

    private void SetPlayer(Info _info)
    {
        player.CurrentSpeed = _info.speed;
        player.CurrentJumpPower = _info.jumpPower;
        player.CurrentJumpHeight = _info.jumpHeight;
        player.CurrentLandingSpeed = _info.landingSpeed;
        player.FlyingTime = _info.airTime;
    }

    public void ChangeGravity()
    {
        if(player.ScaleState == InteractiveObject.SCALE_STATE.NORMAL)
        {
            if (player.GravityState == InteractiveObject.GRAVITY_STATE.ZERO)
                SetPlayer(info[(int)STATE.NZ]);
            else
                SetPlayer(info[(int)STATE.NN]);
        }
        else if(player.ScaleState == InteractiveObject.SCALE_STATE.LARGE)
        {
            if (player.GravityState == InteractiveObject.GRAVITY_STATE.ZERO)
                SetPlayer(info[(int)STATE.LZ]);
            else
                SetPlayer(info[(int)STATE.LN]);
        }
        else
        {
            if (player.GravityState == InteractiveObject.GRAVITY_STATE.ZERO)
                SetPlayer(info[(int)STATE.SZ]);
            else
                SetPlayer(info[(int)STATE.SN]);
        }
    }

    public void ChangeSize()
    {
        if (player.GravityState == InteractiveObject.GRAVITY_STATE.ZERO)
        {
            if (player.ScaleState == InteractiveObject.SCALE_STATE.NORMAL)
                SetPlayer(info[(int)STATE.NZ]);
            else if (player.ScaleState == InteractiveObject.SCALE_STATE.LARGE)
                SetPlayer(info[(int)STATE.LZ]);
            else
                SetPlayer(info[(int)STATE.SZ]);
        }
        else
        {
            if (player.ScaleState == InteractiveObject.SCALE_STATE.NORMAL)
                SetPlayer(info[(int)STATE.NN]);
            else if (player.ScaleState == InteractiveObject.SCALE_STATE.LARGE)
                SetPlayer(info[(int)STATE.LN]);
            else
                SetPlayer(info[(int)STATE.SN]);
        }
    }
    
}
