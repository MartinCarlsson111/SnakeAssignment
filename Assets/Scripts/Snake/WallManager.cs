using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    GameObject wallPrefab;
    public Vector2Int bounds;

    [SerializeField]
    List<GameObject> walls;

    [ContextMenu("RemoveWalls")]
    void RemoveWalls()
    {
        foreach (var wall in walls)
        {
            DestroyImmediate(wall);
        }
        walls.Clear();
    }

    [ContextMenu("Spawn walls")]
    void Spawn()
    {
        if(walls.Count > 0)
        {
            RemoveWalls();
        }

        wallPrefab = Resources.Load("Prefab/Wall") as GameObject;
        if (wallPrefab == null)
        {
            Debug.Log("Wall prefab could not be found");
        }

        for (int i = -bounds.x; i < bounds.x; i++)
        {
            walls.Add(GameObject.Instantiate(wallPrefab, new Vector2(i + this.transform.position.x, -bounds.y + this.transform.position.y), Quaternion.identity, this.transform));
        }

        for (int i = -bounds.y; i < bounds.y; i++)
        {
            walls.Add(GameObject.Instantiate(wallPrefab, new Vector2(-bounds.x + this.transform.position.x, i + this.transform.position.y), Quaternion.identity, this.transform));
        }

        for (int i = -bounds.x; i < bounds.x; i++)
        {
            walls.Add(GameObject.Instantiate(wallPrefab, new Vector2(i + this.transform.position.x, bounds.y + this.transform.position.y), Quaternion.identity, this.transform));
        }

        for (int i = -bounds.y; i < bounds.y + 1; i++)
        {
            walls.Add(GameObject.Instantiate(wallPrefab, new Vector2(bounds.x + this.transform.position.x, i + this.transform.position.y), Quaternion.identity, this.transform));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(walls.Count == 0)
        {
            Spawn();
        }


    }
}
