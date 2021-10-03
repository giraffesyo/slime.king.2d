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
    
    [SerializeField] private Vector3Int currentCell;
    private Vector2Int currentScreen;
    // Start is called before the first frame update
    void Start()
    {
         currentCell = getCurrentCell();
         currentScreen = getCurrentScreen();
    }

    private Vector2Int getCurrentScreen() {
        int x = (int) currentCell.x / 10;
        int y = (int) currentCell.y / 10;
        return new Vector2Int(x, y);
    }

    private Vector3Int getCurrentCell() {
        
        return gridLayout.WorldToCell(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Move(moveX, moveY);
        currentCell = getCurrentCell();
        Vector2Int nextScreen = getCurrentScreen();
        if(currentScreen.x != nextScreen.x || currentScreen.y != nextScreen.y){
            currentScreen = nextScreen;
            ScreenExited.Invoke(nextScreen);
        }
    }
}
