using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable
public class Player : BaseCharacter
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float damageInvincibilitySeconds = 1.0f;

    [SerializeField] public List<Ability> abilities;

    private SlimeKingActions slimeKingActions;
    private InputAction movement;
    private InputAction aiming;
    public delegate void UpdateAbilitiesHandler();
    public event UpdateAbilitiesHandler? AbilitiesUpdated;
    public LayerMask enemyLayer;

    private Vector2 aimingDirection = new Vector2();

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    private void Awake()
    {
        slimeKingActions = new SlimeKingActions();
        abilities = new List<Ability>();
        ObtainAbility(Ability.AbilityKey.Slap);
        ObtainAbility(Ability.AbilityKey.Shoot);
        ObtainAbility(Ability.AbilityKey.Engulf);
        AbilitiesUpdated?.Invoke();
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

    public void ObtainAbility(Ability.AbilityKey key)
    {
        // check that we don't have the ability already
        bool haveAbility = abilities.Exists(ability => ability.abilityKey == key);
        Debug.Log($"We {(haveAbility ? "do" : "do not")} have the ability {key} already");
        // we already have this ability, do nothing.
        if (haveAbility) return;
        // we don't have the ability, so lets get it!
        System.Type abilityType = System.Type.GetType($"{key.ToString()}Ability");
        Ability? ability = gameObject.AddComponent(abilityType) as Ability;
        if (ability == null)
        {
            Debug.LogError($"Could not add ability {key}");
            return;
        }
        ability.enemyLayers = enemyLayer;
        abilities.Add(ability);
        AbilitiesUpdated?.Invoke();
    }

}
