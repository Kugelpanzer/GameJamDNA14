﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{


    private bool flagMoving;// if moving is true cant move in ant other direction
    private Vector2 vLeft, vRight, vUp, vDown;
    private Vector2 moveVector;

    public int health;
    private bool inv; //while true player is invurnable

    public float moveSpeed=1;

    [Tooltip("Dash reload in frames")]
    public int dashReload = 60;
    public int dashPower = 10;

    public int dashDuration = 20;
    private int currDashTime,currDash,currDashReload;

    public float laserDistance = 0.2f;
    private List<Vector2> point = new List<Vector2>();


    // public int protectedTimer;// time while imune to demage

    [Tooltip("Time that player cant take demage, starts when player takes demage")]
    public int invurnableTimer;
    private int currInvTime;

    public void TakeDemage(int demage)
    {
        if (!inv)
        {
           
            if (health > 0)
            {
                health -= demage;
                inv = true;
            }
            else
            {
                DeathTrigger();
            }
        }
    }

    private void DeathTrigger()
    {

    }

    private void InvTime()
    {
        if(inv && currInvTime!=0)
        {
            currInvTime--;
        }
        else if(inv && currInvTime == 0)
        {
            currInvTime = invurnableTimer;
            inv = false;
        }
    }
    private void Dashing()
    {
        if (currDashTime > 0)
        {
            currDash = dashPower;
            currDashTime--;
        }
        else
        {
            currDash = 0;
        }
        if (currDashReload > 0)
        {
            currDashReload--;
        }

    }

    private void MoveScript()
    {
        vLeft = Vector2.zero;
        vRight = Vector2.zero;
        vUp = Vector2.zero;
        vDown = Vector2.zero;


        if (Input.GetKey(KeyCode.A))
        {
            vRight =-Vector2.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            vUp = Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vLeft = Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vUp = -Vector2.up;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (currDashReload <= 0)
            { 
                currDashTime = dashDuration;
                currDashReload = dashReload;
            }
        }
        moveVector = (vLeft + vRight + vUp + vDown).normalized * (moveSpeed+ currDash);
        transform.Translate(moveVector*Time.deltaTime);
    }
    void Start()
    {
        for(float i=-2* laserDistance; i<=2* laserDistance; i+= laserDistance)
        {
            point.Add(new Vector2(i, 2*laserDistance));
        }
        foreach (Vector2 ii in point)
            Debug.Log(ii);
    }


    // Update is called once per frame
    void Update()
    {
        Dashing();
        InvTime();
        MoveScript();
    }
}
