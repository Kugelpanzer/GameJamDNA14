﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject bulletObj;

    public float moveSpeed = 3;
    public int health;

    [Tooltip("reload time in frames")]
    public int reloadTime=30;
    protected int currReloadTime;

    public int invurnableTimer;
    protected int currInvTime;
    protected bool inv;

    protected Vector2 moveVector;

    protected void ConstMove()
    {
        transform.Translate(moveVector.normalized*Time.deltaTime);
    }
    protected void ShootWeapon()
    {
        if (currReloadTime > 0)
        {
            currReloadTime--;
        }
        else
        {
            currReloadTime = reloadTime;
            RealShoot();
        }
    }
    
    protected void RealShoot(/*GameObject target*/)
    {
        if (bulletObj != null)
        {
            GameObject currBullet;
            currBullet = Instantiate(bulletObj);
            currBullet.transform.position = transform.position;
            //currBullet.GetComponent<BulletScript>().target
        }


    }
    protected void EnemyMove()
    {
        moveVector = -Vector2.up *moveSpeed;
    }

    #region invurnable

    public void TakeDemage(int demage)
    {
        if (!inv)
        {
            if (health <= 0)
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

    protected void DeathTrigger()
    {

    }

    protected void InvTime()
    {
        if (inv && currInvTime != 0)
        {
            currInvTime--;
        }
        else if (inv && currInvTime == 0)
        {
            currInvTime = invurnableTimer;
            inv = false;
        }
    }
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        ConstMove();
        ShootWeapon();
        InvTime();
    }
}