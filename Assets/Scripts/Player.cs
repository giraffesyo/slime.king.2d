using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class Player : BaseCharacter
{
    [SerializeField] private Transform spawnPoint;
    public delegate void ScreenExitHandler(Vector2Int nextScreen);
    public event ScreenExitHandler ScreenExited;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private int xScreenSize = 20;
    [SerializeField] private int yScreenSize = 20;
    [SerializeField] private float damageInvincibilitySeconds = 1.0f;


    [SerializeField] private Vector3Int currentCell;

    [SerializeField] private Vector2Int currentScreen;
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
        currentCell = getCurrentCell();
        currentScreen = getCurrentScreen();
        abilities[(int)Ability.BasicAbilityKeys.Melee] = GetComponent<MeleeAbility>();
        abilities[(int)Ability.BasicAbilityKeys.Engulf] = GetComponent<EngulfAbility>();
        abilities[(int)Ability.BasicAbilityKeys.Ranged] = GetComponent<RangedAbility>();


    }

    private Vector2Int getCurrentScreen()
    {
        int x = (int)Mathf.Round((float)currentCell.x / xScreenSize);
        int y = (int)Mathf.Round((float)currentCell.y / yScreenSize);
        return new Vector2Int(x, y);
    }

    private Vector3Int getCurrentCell()
    {

        return gridLayout.WorldToCell(transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        moveAttackPoint();
        Move(new Vector2(moveX, moveY));
        currentCell = getCurrentCell();
        Vector2Int nextScreen = getCurrentScreen();
        if (currentScreen.x != nextScreen.x || currentScreen.y != nextScreen.y)
        {
            currentScreen = nextScreen;
            if (ScreenExited != null)
            {
                ScreenExited.Invoke(nextScreen);
            }
        }
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
