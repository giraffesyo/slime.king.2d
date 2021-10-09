using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// using UnityEngine.Til
public class Player : BaseCharacter
{
    public delegate void ScreenExitHandler(Vector2Int nextScreen);
    public event ScreenExitHandler ScreenExited;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private int xScreenSize = 20;
    [SerializeField] private int yScreenSize = 20;

    
    [SerializeField] private Vector3Int currentCell;

    [SerializeField] private Vector2Int currentScreen;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        currentCell = getCurrentCell();
        currentScreen = getCurrentScreen();
    }

    private Vector2Int getCurrentScreen() {
        int x = (int) Mathf.Round( (float)currentCell.x / xScreenSize);
        int y = (int) Mathf.Round((float)currentCell.y / yScreenSize);
        return new Vector2Int(x, y);
    }

    private Vector3Int getCurrentCell() {
        
        return gridLayout.WorldToCell(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveAttackPoint();
        Move(moveX, moveY);
        currentCell = getCurrentCell();
        Vector2Int nextScreen = getCurrentScreen();
        if(currentScreen.x != nextScreen.x || currentScreen.y != nextScreen.y){
            currentScreen = nextScreen;
            ScreenExited.Invoke(nextScreen);
        }
    }

}
