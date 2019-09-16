using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(wall == null)
        {
            Debug.LogError("Game object did not have a wall component");
        }
        if(agent == null)
        {
            Debug.LogError("Game object did not have an agent component");
        }
    }

    private void Update()
    {
        updateAccu += Time.deltaTime;
        if(updateTimer < updateAccu )
        {
            var gen = UnityEngine.Random.Range(0.0f, 100.0f);
            if (gen <= chance)
            {
                if (agent != null)
                {
                    agent.enabled = true;
                }
                if (wall != null)
                {
                    wall.Unregister();
                    wall.enabled = false;
                }
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
