using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Combat : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject missilePrefab;
    public float missileForce = .5f;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public bool isAi;
    private Animator animator;

    public LayerMask enemyLayers;       // All enemies must be in a layer

    int attackCooldown = 50; //Temporary variables
    int cooldownCounter = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (cooldownCounter != 0)
        {
            cooldownCounter = (cooldownCounter + 1) % attackCooldown;   // fixedUpdate gets called 50 times per second
            if (cooldownCounter == 0)
            {
                transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (!isAi && (keyboard.spaceKey.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
        {
            MeleeAttack();
        }
        if (!isAi && (keyboard.nKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame))
        {
            missileForce = 10f;
            Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
            RangedAttack(new Vector2(worldPos.x - attackPoint.position.x, worldPos.y - attackPoint.position.y).normalized);
        }
    }

    public void MeleeAttack()
    {

        // if (cooldownCounter != 0)
        // {
        //     return;
        // }
        if (animator != null)
        {
            animator.SetTrigger("Melee");
        }

        // Temporary, flickers white circle showing hitboxes of attacks
        cooldownCounter = 1;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // FIXME: Won't this make it so enemies can kill each other?
        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.GetComponent<BaseCharacter>();
            enemyChar.TakeDamage(attackDamage);
            StartCoroutine(enemyChar.Knockback(5, 1f, attackPoint.transform));
        }

    }

    public void RangedAttack(Vector2 direction)
    {
        GameObject missile = Instantiate(missilePrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * missileForce, ForceMode2D.Impulse);
    }

    // For debugging. Draws circle when in editing mode showing attack range (Must click on ooey to see circle)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            Debug.Log("Attack point is null");

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
