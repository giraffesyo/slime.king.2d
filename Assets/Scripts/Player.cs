using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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

        slimeKingActions.Player.Slap.performed += (InputAction.CallbackContext ctx) => abilities[0].RequestUse(ctx, aimingDirection);
        slimeKingActions.Player.Slap.Enable();
        slimeKingActions.Player.Shoot.performed += (InputAction.CallbackContext ctx) => abilities[1].RequestUse(ctx, aimingDirection);
        slimeKingActions.Player.Shoot.Enable();

        slimeKingActions.Player.Engulf.performed += (InputAction.CallbackContext ctx) => abilities[2].RequestUse(ctx, aimingDirection);
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

    protected new void Start()
    {
        base.Start();
        abilities[(int)Ability.BasicAbilityKeys.Melee] = GetComponent<MeleeAbility>();
        abilities[(int)Ability.BasicAbilityKeys.Engulf] = GetComponent<EngulfAbility>();
        abilities[(int)Ability.BasicAbilityKeys.Ranged] = GetComponent<RangedAbility>();
    }

    void Update()
    {
        moveAttackPoint();
    }

    protected override void Die()
    {
        base.Die();
        // restart the level?
        // for now teleporting to spawn.
        transform.position = spawnPoint.position;
        // restore HP
        SetCurrentHealth(maxHealth);
    }

}
