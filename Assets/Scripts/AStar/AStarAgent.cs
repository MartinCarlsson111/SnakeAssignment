using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarAgent : MonoBehaviour
{
    public List<Vector2> path;

    WorldGrid worldGridInstance;

    public Transform target;
    float errDistance = 0.01f;
    public float updateTimer = 0.3f;
    float updateAccu;

    void Start()
    {
        worldGridInstance = WorldGrid.Instance;
        path = new List<Vector2>();
    }

    void Update()
    {
        updateAccu += Time.deltaTime;
        if (updateTimer < updateAccu)
        {
            if (path.Count > 0)
            {
                if (ValidatePath(path[0]))
                {
                    if (updateTimer < updateAccu)
                    {
                        Move(path[0]);
                        path.Clear();
                        updateAccu = 0;
                    }
                }
                else
                {
                    path.Clear();
                }
            }
            else if (target != null)
            {
                GetPath(target.position);
                updateAccu = 0;
            }
            else
            {
                //TODO: Write better aquire target function
                var go = GameObject.Find("SnakeHead(Clone)"); //Ew..
                if (go != null)
                {
                    target = go.transform;
                }
                updateAccu = 0;
            }
        }
    }

    void Move(Vector2 targetPos)
    {
        var dir = targetPos - new Vector2(transform.position.x, transform.position.y);
        var normalDir = Vector3.Normalize(dir);

        transform.Translate(normalDir);
        var diff = (new Vector2(transform.position.x, transform.position.y) - targetPos);
        if (diff.x < errDistance && diff.x > -errDistance && diff.y < errDistance && diff.y > -errDistance)
        {
            transform.position = targetPos; 
            path.RemoveAt(0);
        }
    }

    bool ValidatePath(Vector2 path)
    {
        worldGridInstance.Check((int)path.x, (int)path.y, out var state);
        if (state == (int)WorldGrid.CellState.OPEN)
        {
            return true;
        }
        return false;
    }


    void GetPath(Vector2 target)
    {
        if(!AStarPathGenerator.Instance.GetPath(transform.position, target, ref path))
        {
            updateAccu = -1.0f;
            path.Clear();
        }


    }
}