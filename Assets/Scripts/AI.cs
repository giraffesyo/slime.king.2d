using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : BaseCharacter
{
    Transform playerPos;    // Transform object of the controllable player

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        setSpeed(2f);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
    }


    protected void Move()
    {
        int moveX = 0;
        int moveY = 0;
        if(transform.localPosition.x < playerPos.localPosition.x)
        {
            moveX = 1;
        }
        else if(transform.position.x > playerPos.position.x)
        {
            moveX = -1;
        }
        if (transform.position.y < playerPos.position.y)
        {
            moveY = 1;
        }
        else if (transform.position.y > playerPos.position.y)
        {
            moveY = -1;
        }
        base.Move(moveX, moveY);
    }
}
