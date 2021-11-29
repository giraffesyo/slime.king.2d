using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    SpriteRenderer meleeAOE_sr;
    CapsuleCollider2D meleeAOE_Collider;
    ContactFilter2D enemyFilter;

    // Start is called before the first frame update
    void Start()
    {
        meleeAOE_sr = transform.Find("meleeAOE").GetComponent<SpriteRenderer>();
        meleeAOE_Collider = transform.Find("meleeAOE").GetComponent<CapsuleCollider2D>();
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Player"));
    }

    public void Use()    // Animation Event
    {
        meleeAOE_sr.enabled = true;
        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(meleeAOE_Collider, enemyFilter, hitEnemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.gameObject.GetComponent<BaseCharacter>();

            if (enemyChar != null)
            {
                enemyChar.Knockback(0.3f, this.transform);

                enemyChar.TakeDamage(1);
            }
        }
        StartCoroutine(disableMeleeAOE()); // To show player aoe radius
    }
    IEnumerator disableMeleeAOE()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        meleeAOE_sr.enabled = false;
    }
}
