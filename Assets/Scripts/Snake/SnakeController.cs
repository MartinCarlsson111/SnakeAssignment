using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour, ICollisionResponse
{
    public GameObject snakePrefabHead;
    public GameObject snakePrefabBody;

    public float updateTimer = 0.3f;
    float updateAccu = 0;

    Vector3 moveDir = new Vector3(1, 0, 0);

    public bool lost = false;
    bool movementDirSet = false;
    Snake root;
    Snake tail;
    Vector3 lastTailPosition;

    Collider2D rootCollider;
    LayerMask snakeBodyLayer;
    LayerMask fruitLayer;
    LayerMask wallLayer;

    void Start()
    {
        snakeBodyLayer = LayerMask.NameToLayer("SnakeBody");
        fruitLayer = LayerMask.NameToLayer("Fruit");
        wallLayer = LayerMask.NameToLayer("Wall");
        if (snakePrefabBody == null)
        {
            snakePrefabBody = Resources.Load("Prefab/SnakeBody") as GameObject;
        }
        if (snakePrefabHead == null)
        {
            snakePrefabHead = Resources.Load("Prefab/SnakeHead") as GameObject;
        }

        var go = GameObject.Instantiate(snakePrefabHead, this.transform);
        root = go.GetComponent<Snake>();
        root.next = null;
        rootCollider = root.GetComponent<Collider2D>();
        tail = root;
    }

    void AddSnakePart()
    {
        var go = GameObject.Instantiate(snakePrefabBody, lastTailPosition, Quaternion.identity, this.transform);
        var snakeComp = go.GetComponent<Snake>();
        snakeComp.next = null;

        var nextNode = root;
        while(nextNode.next != null)
        {
            nextNode = nextNode.next;
        }
        nextNode.next = snakeComp;
        tail = snakeComp;
    }

    void TestCollision()
    {
        var pos = new Vector2(root.transform.position.x, root.transform.position.y);
        var size = rootCollider.bounds.size * 0.3f;

        var hits = Physics2D.BoxCastAll(pos, size, 0, new Vector2(0,0));
        foreach(var hit in hits)
        {
            CollisionResponse(hit.collider.gameObject);
        }
    }

    public void CollisionResponse(GameObject obj)
    {

        if(snakeBodyLayer.value == obj.layer)
        {
            lost = true;
        }

        if (wallLayer.value == obj.layer)
        {
            lost = true;
        }

        if (fruitLayer.value == obj.layer)
        {
            AddSnakePart();
            obj.GetComponent<Fruit>().CollisionResponse(this.gameObject);
        }
    }

    void Update()
    {
        if(!lost)
        {
            if (Input.GetKey(KeyCode.A) && moveDir.x == 0 && !movementDirSet)
            {
                moveDir.x = -1;
                moveDir.y = 0;
                movementDirSet = true;
            }
            if (Input.GetKey(KeyCode.D) && moveDir.x == 0 && !movementDirSet)
            {
                moveDir.x = 1;
                moveDir.y = 0;

                movementDirSet = true;
            }

            if (Input.GetKey(KeyCode.S) && moveDir.y == 0 && !movementDirSet)
            {
                moveDir.y = -1;
                moveDir.x = 0;

                movementDirSet = true;
            }

            if (Input.GetKey(KeyCode.W) && moveDir.y == 0 && !movementDirSet)
            {
                moveDir.y = 1;
                moveDir.x = 0;

                movementDirSet = true;
            }

            updateAccu += Time.deltaTime;
            if (updateTimer < updateAccu)
            {
                lastTailPosition = tail.transform.position;
                root.Move(root.transform.position + (moveDir));
                updateAccu = 0;
                TestCollision();

                movementDirSet = false;
            }
        }
    }
}
