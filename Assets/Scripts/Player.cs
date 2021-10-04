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

    public float moveX = 0;
    public float moveY = 0;
    public Transform attackPoint;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        currentCell = getCurrentCell();
        currentScreen = getCurrentScreen();
        attackPoint = GetComponent<Combat>().attackPoint;
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

    void moveAttackPoint()
    {
        if(moveX == 0 && moveY == 0){
            attackPoint.localPosition = new Vector3(1, 0, 0);
            return;
        }
        // Absoulte value used since when player turns left and right , the whole object gets rotated
        // meaning we dont need to worry about placing attackPoint behind the object
        attackPoint.localPosition = new Vector3(Mathf.Abs(moveX), moveY, 0);
    }
}
