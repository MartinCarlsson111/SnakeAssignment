using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, CollisionInterface, RegisterToWorldGrid
{
    public void CollisionResponse(string tag, GameObject obj)
    {

    }

    public void Register()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        WorldGrid.Instance.Set(x, y, WorldGrid.CellState.BLOCKED);
    }

    public void Unregister()
    {
        if(WorldGrid.Instance != null)
        {
            WorldGrid.Instance.Set((int)transform.position.x, (int)transform.position.y, WorldGrid.CellState.OPEN);
        }
    }

    void Start()
    {
        Register();
    }

    private void OnDestroy()
    {
        Unregister();
    }

}
