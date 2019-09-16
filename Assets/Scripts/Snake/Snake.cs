using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Snake : MonoBehaviour, IEnumerable
{
    public Snake next;

    private void Start()
    {
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
}