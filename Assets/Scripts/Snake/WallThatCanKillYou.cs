using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wall))]
[RequireComponent(typeof(AStarAgent))]
public class WallThatCanKillYou : MonoBehaviour
{
    public float chance = 0.001f;
    public float updateTimer = 1.0f;
    float updateAccu = 0.0f;

    AStarAgent agent;
    Wall wall;
    new SpriteRenderer renderer;
    [SerializeField]
    public Color col;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<AStarAgent>();
        wall = GetComponent<Wall>();
    }

    private void Update()
    {
        updateAccu += Time.deltaTime;
        if(updateTimer < updateAccu )
        {
            var gen = UnityEngine.Random.Range(0.0f, 100.0f);
            if (gen <= chance)
            {
                agent.enabled = true;
                wall.Unregister();
                wall.enabled = false;

                if (renderer != null)
                {
                    renderer.color = col;
                    renderer.sortingOrder = 4;
                }

                this.enabled = false;
            }
            updateAccu = 0;
        }
    }
}
