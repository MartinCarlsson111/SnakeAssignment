using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, ICollisionInterface
{
    public FruitSpawner spawner;
    public void CollisionResponse(string tag, GameObject obj)
    {
        spawner.FruitWasDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    void Start()
    {
        spawner = GetComponentInParent<FruitSpawner>();
        if (spawner == null)
        {
            Debug.LogError("Fruit spawned without parent fruit spawner component, destroying game object");
            Destroy(this.gameObject);
        }
    }
}
