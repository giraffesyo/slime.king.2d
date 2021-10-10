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

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        currentCell = getCurrentCell();
        currentScreen = getCurrentScreen();
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

        moveX = 0;
        moveY = 0;
        var keyboard = Keyboard.current;
        if (keyboard.wKey.isPressed)
        {
            moveY = 1;
        }
        else if (keyboard.sKey.isPressed)
        {
            moveY = -1;
        }
        if (keyboard.aKey.isPressed)
        {
            moveX = -1;
        }
        else if (keyboard.dKey.isPressed)
        {
            moveX = 1;
        }


        moveAttackPoint();
        Move(moveX, moveY);
        currentCell = getCurrentCell();
        Vector2Int nextScreen = getCurrentScreen();
        if (currentScreen.x != nextScreen.x || currentScreen.y != nextScreen.y)
        {
            currentScreen = nextScreen;
            ScreenExited.Invoke(nextScreen);
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

    // protected override void SetMaxHealth(int amount)
    // {
    //     base.SetMaxHealth(amount);
    //     MaxHealthSet.Invoke(amount);
    // }
    // override public void SetCurrentHealth(int hp)
    // {
    //     base.SetCurrentHealth(hp);
    //     HealthSet.Invoke(hp);
    // }
    // public override void TakeDamage(int damage)
    // {
    //     base.TakeDamage(damage);
    //     ActivateInvincibility(damageInvincibilitySeconds);

    // }


}
