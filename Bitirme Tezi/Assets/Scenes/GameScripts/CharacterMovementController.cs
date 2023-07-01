using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class CallAfterDelay : MonoBehaviour
{
    float delay;
    System.Action action;

    // Will never call this frame, always the next frame at the earliest
    public static CallAfterDelay Create(float delay, System.Action action)
    {
        CallAfterDelay cad = new GameObject("CallAfterDelay").AddComponent<CallAfterDelay>();
        cad.delay = delay;
        cad.action = action;
        return cad;
    }

    float age;

    void Update()
    {
        if (age > delay)
        {
            action();
            Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        age += Time.deltaTime;
    }
}

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] //Editorden erismek icin
    public Tilemap groundTilemap;
    [SerializeField]
    public Tilemap obstaclesTilemap;
    [SerializeField]
    public Tilemap waterTilemap;
    [SerializeField]
    public Tilemap chestPositionTilemap;
    [SerializeField]
    //public GameObject character;
    private Vector2 up;
    private Vector2 down;
    private Vector2 left;
    private Vector2 right;
    //private float moveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
      //  moveSpeed = 5f;
        up = new Vector2(0, 1);
        down = new Vector2(0, -1);
        left = new Vector2(-1, 0);
        right = new Vector2(1, 0);
        Debug.Log(right.x);
        //Move(dr);
        //Move(dr);
        //Move(dr);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Kodda yazılanlara göre Move() parametreleriyle cagrilacak.
    public void Move(Vector2 direction)
    {
        if(CanMove(direction))
        {
            Debug.Log(transform.position.x);
            Debug.Log(direction.x + " " +direction.y);
            //character.transform.position += (Vector3)direction;
            transform.position += (Vector3)direction;
            //transform.position = Vector3.MoveTowards(transform.position, (Vector3)direction, moveSpeed * Time.deltaTime);
            Debug.Log(direction.x);
            Debug.Log(transform.position.x);
        }
    }

    public void Swim(Vector2 direction)
    {
        if (CanSwim(direction))
        {
            //character.transform.position += (Vector3)direction;
            transform.position += (Vector3)direction;
        }
    }


    public void MoveUp()
    {
        //Move(up);
        Invoke("Move_Up", 1f);
    }
    public void Move_Up()
    {
        Move(up);
    }

    public void MoveDown()
    {
        Move(down);
    }
    public void MoveLeft()
    {
        Move(left);
    }
    public void MoveRight()
    {
        //Debug.Log("moveright");
        //Invoke("Move_Right", 1f);
        //CallAfterDelay.Create(2.5f, () => {
        //    // put the code here you want to do in 2.5 seconds, call a function, etc.
        //    Move(right);
        //});
        Move(right);
    }

    public void Move_Right()
    {
        Move(right);
    }

    public void SwimUp()
    {
        Swim(up);
    }
    public void SwimDown()
    {
        Swim(down);
    }
    public void SwimLeft()
    {
        Swim(left);
    }
    public void SwimRight()
    {
        Swim(right);
    }

    //direction degeri iki boyutlu vektör (0,1) veya (-1,0) gibi
    private bool CanMove(Vector2 direction)
    {
        Debug.Log("canmove??");
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        //eger gridPosition'da tile yoksa veya bir obstacle varsa oraya gidilemez
        if(!groundTilemap.HasTile(gridPosition) || obstaclesTilemap.HasTile(gridPosition))
        {
            Debug.Log("Cant move");
            return false;
        }
        return true;
    }

    private bool CanSwim(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        //eger gridPosition'da tile yoksa veya bir obstacle varsa oraya gidilemez
        if (!waterTilemap.HasTile(gridPosition) || obstaclesTilemap.HasTile(gridPosition))
        {
            Debug.Log("Cant swim");
            return false;
        }
        return true;
    }
}
