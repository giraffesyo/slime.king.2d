using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TornadoAI : AI
{
    Vector2 targetLocation;

    [SerializeField] float walkAmount;   // Max walk distance after choosing location
    bool hitWall;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    protected override bool Move()
    {
        if (!base.Move())
            return false;

        if (moveDirection == Vector2.zero)
        {
            targetLocation = getTargetLocation();
            base.Move(targetLocation);
            targetLocation = targetLocation + (Vector2)transform.position;
            //Debug.DrawLine(transform.position, targetLocation + (Vector2)transform.position, Color.white, 5f, false);

        }
        else if (Vector2.Distance((Vector2)transform.position, targetLocation) < .1f || hitWall)
        {
            hitWall = false;
            moveDirection = Vector2.zero;
        }
        else if(Vector3.Distance(transform.position, playerPos.position) < attackRange)
        {
            StartCoroutine(DoAbility());
        }
        return true;
    }

    private Vector2 getTargetLocation()
    {
        /* The way this works:
         * Searches 67.5 degrees to the left or to the right of ooey
         * Takes off 15 degrees closest to ooey 
         * +70° +40°   -40°   -70°
         * \XXXXXXX|00000|XXXXXXX/
         *  \XXXXXX|00P00|XXXXXX/       radius/height = walkAmount
         *   \XXXXX|00000|XXXXX/
         *    
         *            T
         * X = possible, 0 = not possible, P = player, T = tornado enemy
         * Just imagine that middle part above is circular instead of rectangular
         * 
        */

        // Get vector from tornado to ooey
        Vector2 playerDirection = (Vector2)(playerPos.position - transform.position);
        // Turn to polar coordinate (dont need radius)
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        // if within certain distance, will back away instead of getting closer
        if (Vector3.Distance(transform.position, playerPos.position) < attackRange)
            playerAngle += 180f;

        Vector2 loc;    
        float angle1 = playerAngle + 40f;       // Angles for right side of ooey
        float angle2 = playerAngle + 70f;
        float random = Random.Range(0f, 2f);

        // Choosing left or right side of ooey
        if (random < 1f)
        { // Left side of ooey
            angle1 = playerAngle - 40f;
            angle2 = playerAngle - 70f;
        }

        random = Mathf.Deg2Rad * Random.Range(angle1, angle2);      // getting angle angle1 < random < angle2
        float radius = Random.Range(1f, walkAmount);                // getting radius

        loc.x = radius * Mathf.Cos(random);         // Getting cartesian coordinates relative to tornado guy(x,y)
        loc.y = radius * Mathf.Sin(random);

        return loc;
    }

    private IEnumerator DoAbility()
    {
        aiAbility.rotation = Mathf.Atan2(playerPos.position.y, playerPos.position.x) * Mathf.Rad2Deg;
        animator.SetTrigger("Tornado");
        yield return new WaitForSeconds(0.5f);
        aiAbility.Use((Vector2)playerPos.position - (Vector2)transform.position, (Vector2)playerPos.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitWall = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        hitWall = true;
    }
}
