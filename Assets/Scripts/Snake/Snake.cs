using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IEnumerable, CollisionInterface
{
    public Snake next;
    new public BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    public IEnumerator GetEnumerator()
    {
        var nextNode = this;
        do
        {
            yield return nextNode;
            nextNode = next;
        }
        while (nextNode.next != null);
    }

    public void Move(Vector3 targetPos)
    {
        var position = transform.position;
        transform.position = targetPos;
        if(next != null)
        {
            next.Move(position);
        }
    }

    public void CollisionResponse(string tag, GameObject obj)
    {
        //tell the snake controller to do something
    }
}