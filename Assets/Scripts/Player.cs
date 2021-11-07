using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable
public class Player : BaseCharacter
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float damageInvincibilitySeconds = 3.0f;

    private List<Ability> _abilities;
    // Hide the ability list from the inspector
    public ReadOnlyCollection<Ability> abilities => _abilities.AsReadOnly();

    private SlimeKingActions slimeKingActions;
    private InputAction movement;
    private InputAction aiming;
    public delegate void UpdateAbilitiesHandler(Ability.AbilityKey abilityAdded, int atIndex);
    public event UpdateAbilitiesHandler? AbilitiesUpdated;
    public LayerMask enemyLayer;

    int? activeAbility;

    private Vector2 aimingDirection = new Vector2();

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    private void Awake()
    {
        slimeKingActions = new SlimeKingActions();
        _abilities = new List<Ability>();
        ObtainAbility(Ability.AbilityKey.Slap);
        ObtainAbility(Ability.AbilityKey.Shoot);
        ObtainAbility(Ability.AbilityKey.Engulf);
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
        // Add 2 to index to account for the fact that the basic abilities are at indices 0, 1, 2
        slimeKingActions.Player.SelectAbility1.performed += (InputAction.CallbackContext ctx) => SelectAbility(ctx, 3);
        slimeKingActions.Player.SelectAbility1.Enable();
        slimeKingActions.Player.SelectAbility2.performed += (InputAction.CallbackContext ctx) => SelectAbility(ctx, 4);
        slimeKingActions.Player.SelectAbility2.Enable();
        slimeKingActions.Player.SelectAbility3.performed += (InputAction.CallbackContext ctx) => SelectAbility(ctx, 5);
        slimeKingActions.Player.SelectAbility3.Enable();
        slimeKingActions.Player.SelectAbility4.performed += (InputAction.CallbackContext ctx) => SelectAbility(ctx, 6);
        slimeKingActions.Player.SelectAbility4.Enable();
        slimeKingActions.Player.UseAbility.performed += (InputAction.CallbackContext ctx) =>
        {
            if (activeAbility != null)
            {
                RequestUse(ctx, (int)activeAbility);
            }
        };
        slimeKingActions.Player.UseAbility.Enable();
    }


    private void SelectAbility(InputAction.CallbackContext ctx, int index)
    {

        if (index < _abilities.Count)
        {
            activeAbility = index;
        }
        else
        {
            activeAbility = null;
        }
    }


    private void OnDisable()
    {
        aiming.Disable();
        movement.Disable();
        slimeKingActions.Player.Slap.Disable();
        slimeKingActions.Player.Shoot.Disable();
        slimeKingActions.Player.Engulf.Disable();
        slimeKingActions.Player.SelectAbility1.Disable();
        slimeKingActions.Player.SelectAbility2.Disable();
        slimeKingActions.Player.SelectAbility3.Disable();
        slimeKingActions.Player.SelectAbility4.Disable();
        slimeKingActions.Player.UseAbility.Disable();
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
        //ObtainAbility(Ability.AbilityKey.Tornado);
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
        _abilities[abilityIndex].RequestUse(ctx, worldPos);
    }

    public void ObtainAbility(Ability.AbilityKey key)
    {
        // check that we don't have the ability already
        bool haveAbility = _abilities.Exists(ability => ability.abilityKey == key);
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
        _abilities.Add(ability);
        AbilitiesUpdated?.Invoke(key, _abilities.Count - 1);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!isInvincible) { 
            StartCoroutine(ActivateInvincibility(damageInvincibilitySeconds));
            StartCoroutine(PlayerInvincibility(damageInvincibilitySeconds));
        }
    }

    private IEnumerator PlayerInvincibility(float invincibilityTime)
    {

        for(int i = 0; i < 360 * invincibilityTime; i += 1)
        {   
            // Opacity oscillates between 1 and 0, 3 times throughout invincibility time
            float opacity = ((Mathf.Cos(3*i * Mathf.Deg2Rad) / 2f) + 0.5f);

            Color newColor = new Color(255, 255, 255, opacity);
            spriteRenderer.color = newColor;
            yield return new WaitForSecondsRealtime(1f / 360f);
        }

        // In case damageInvincibilitySeconds gets changed to a decimal and for loop doesnt end on opacity = 1
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }
}
