using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : BaseCharacter
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float damageInvincibilitySeconds = 1.0f;

    [SerializeField] private List<Ability> abilities = new List<Ability>();

    private SlimeKingActions slimeKingActions;
    private InputAction movement;
    private InputAction aiming;

    private Vector2 aimingDirection = new Vector2();

    private void Awake()
    {
        slimeKingActions = new SlimeKingActions();
    }
    private void OnEnable()
    {
        movement = slimeKingActions.Player.Move;
        movement.Enable();

        aiming = slimeKingActions.Player.Aim;
        aiming.Enable();

        slimeKingActions.Player.Slap.performed += (InputAction.CallbackContext ctx) => RequestUse(ctx, (int)Ability.AbilityKey.Slap);
        slimeKingActions.Player.Slap.Enable();
        slimeKingActions.Player.Shoot.performed += (InputAction.CallbackContext ctx) => RequestUse(ctx, (int)Ability.AbilityKey.Shoot);
        slimeKingActions.Player.Shoot.Enable();

        slimeKingActions.Player.Engulf.performed += (InputAction.CallbackContext ctx) => RequestUse(ctx, (int)Ability.AbilityKey.Engulf);
        slimeKingActions.Player.Engulf.Enable();
    }

    private void OnDisable()
    {
        aiming.Disable();
        movement.Disable();
        slimeKingActions.Player.Slap.Disable();
        slimeKingActions.Player.Shoot.Disable();
        slimeKingActions.Player.Engulf.Disable();

    }

    override protected void FixedUpdate()
    {
        Move(movement.ReadValue<Vector2>());
        aimingDirection = aiming.ReadValue<Vector2>();
        base.FixedUpdate();
    }

    override protected void Start()
    {
        base.Start();
        abilities[(int)Ability.AbilityKey.Slap] = GetComponent<MeleeAbility>();
        abilities[(int)Ability.AbilityKey.Engulf] = GetComponent<EngulfAbility>();
        abilities[(int)Ability.AbilityKey.Shoot] = GetComponent<RangedAbility>();
    }

    public override void Die()
    {
        base.Die();
        // restart the level?
        // for now teleporting to spawn.
        transform.position = spawnPoint.position;
        // restore HP
        SetCurrentHealth(_maxHealth);
    }

    private void RequestUse(InputAction.CallbackContext ctx, int abilityIndex)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(aimingDirection);
        aimingDirection = new Vector2(worldPos.x - transform.position.x, worldPos.y - transform.position.y).normalized;
        abilities[abilityIndex].RequestUse(ctx, aimingDirection);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        ChargeAbility ca = GetComponent<ChargeAbility>();
        if (ca != null)
        {
            ca.stopCharging();
        }
    }

}
