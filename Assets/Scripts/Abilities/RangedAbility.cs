using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RangedAbility : Ability
{
    public Transform attackPoint;
    public GameObject missilePrefab;
    public float missileForce = 10f;
    public bool isAi;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (!isAi && !onCooldown && (keyboard.nKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame))
        {
            Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
            Use(new Vector2(worldPos.x - attackPoint.position.x, worldPos.y - attackPoint.position.y).normalized);
        }
    }

    override public void Use(Vector2 target)
    {
        if (onCooldown)
        {
            return;
        }
        base.Use(target);
        if (target != null)
        {
            Vector2 t = (Vector2)target;
            GameObject missile = Instantiate(missilePrefab, attackPoint.position, attackPoint.rotation);
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
            rb.AddForce(t * missileForce, ForceMode2D.Impulse);
        }
    }
}
