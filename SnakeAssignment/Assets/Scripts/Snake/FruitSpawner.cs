using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    GameObject fruitPrefab;
    public Vector2Int spawnBounds = new Vector2Int(10, 10);
    GameObject activeFruit = null;

    // Start is called before the first frame update
    void Start()
    {
        fruitPrefab = Resources.Load("Prefab/Fruit") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeFruit == null)
        {
            var halfSpawnBounds = new Vector2Int(spawnBounds.x / 2, spawnBounds.y / 2);

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-halfSpawnBounds.x+1, halfSpawnBounds.x), UnityEngine.Random.Range(-halfSpawnBounds.y+1, halfSpawnBounds.y));
            if(WorldGrid.Instance.Check((int)pos.x, (int)pos.y, out int cellState))
            {
                if(cellState == (int)WorldGrid.CellState.OPEN)
                {
                    activeFruit = GameObject.Instantiate(fruitPrefab, pos, Quaternion.identity, this.transform);
                }            
            }

        }
    }

    public void FruitWasDestroyed(GameObject fruit)
    {
        activeFruit = null;
    }
}
