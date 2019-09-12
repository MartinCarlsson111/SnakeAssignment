using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, CollisionInterface
{
    public FruitSpawner spawner;
    public void CollisionResponse(string tag, GameObject obj)
    {
        spawner.FruitWasDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponentInParent<FruitSpawner>();
        if (spawner == null)
        {
            Debug.LogError("Fruit spawned without parent fruit spawner component, destroying game object");
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        //animation
    }
}
