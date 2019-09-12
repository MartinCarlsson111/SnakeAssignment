using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AStarPathGenerator : Singleton<AStarPathGenerator>
{
    protected AStarPathGenerator() { }
    WorldGrid worldGridInstance;

    internal class Node : IComparable<Node>
    {
        public Node(Node parent, Vector2 position, float g, float h)
        {
            this.parent = parent;
            f = g + h;
            this.g = g;
            this.h = h;
            this.position = position;
        }
        public Vector2 position;
        public float f, g, h;
        public Node parent;

        //sorts by f cost
        public int CompareTo(Node other)
        {
            if (f == other.f) return 0;
            return f < other.f ? -1 : 1;         
        }
    }

    List<Node> open = new List<Node>();
    List<Node> closed = new List<Node>();

    //TODO: Find error! if the system uses fractions, the whole thing breaks...
    //TODO: Cache used paths

    //Manhattan distance
    float h(Vector2 currentCell, Vector2 target)
    {
        return Mathf.Abs(currentCell.x - target.x) + Mathf.Abs(currentCell.y - target.y);
    }


    //To support 8 directional movement
    /*
    float DistanceBetween(Vector2 pos, Vector2 target)
    {

        int dx = (int)Mathf.Abs(target.x - pos.x);
        int dy = (int)Mathf.Abs(target.y - pos.y);

        int min = Mathf.Min(dx, dy);
        int max = Mathf.Max(dx, dy);

        int diagonalSteps = min;
        int straightSteps = max - min;

        return Mathf.Sqrt(2) * diagonalSteps + straightSteps;
    }
    */

    //TODO: Support 8 directional movement  //3x3 for loop, -1 to 1, skip if i == 0 && y== 0
    void GetNeighbors(Vector2 position, out List<Vector2> neighbors)
    {
        neighbors = new List<Vector2>();

        worldGridInstance.Check((int)position.x, (int)position.y + 1, out int north);
        if (north == (int)WorldGrid.CellState.OPEN)
        {
            neighbors.Add(position + new Vector2(0, 1));
        }
        worldGridInstance.Check((int)position.x, (int)position.y - 1, out int south);
        if (south == (int)WorldGrid.CellState.OPEN)
        {
            neighbors.Add(position + new Vector2(0, -1));
        }
        worldGridInstance.Check((int)position.x-1, (int)position.y, out int west);
        if (west == (int)WorldGrid.CellState.OPEN)
        {
            neighbors.Add(position + new Vector2(-1, 0));
        }
        worldGridInstance.Check((int)position.x+1, (int)position.y, out int east);
        if (east == (int)WorldGrid.CellState.OPEN)
        {
            neighbors.Add(position + new Vector2(1, 0));
        }
    }

    //test if node already exists in open or closed with better f cost
    bool ShouldBeSkipped(Node node)
    {
        foreach (var n in open)
        {
            if (n.position == node.position)   
            {
                if (node.f > n.f)
                {
                    return true;
                }
            }
        }
        foreach (var n in closed)
        {
            if (n.position == node.position)
            {
                if (node.f > n.f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Returns true if a path could be made to the target
    //returns false if start is target
    //returns false if target is blocked
    public bool GetPath(Vector2 start, Vector2 target, ref List<Vector2> path, int maxIter = 255)
    {
        if (worldGridInstance == null) worldGridInstance = WorldGrid.Instance;

        if (start == target) return false;
        worldGridInstance.Check(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y), out int cellstate);
        if (cellstate == (int)WorldGrid.CellState.BLOCKED) return false;

        open.Add(new Node(null, start, 0, 0));
        Node currentNode;
        int iterations = 0;
        bool targetFound = false;
        while (open.Count > 0 && iterations < maxIter)
        {
            open.Sort();  
            currentNode = open[0];
            open.RemoveAt(0);
            closed.Add(currentNode);
            GetNeighbors(currentNode.position, out List<Vector2> neighbors);
            foreach (var neighbor in neighbors)
            {
                Node newNode = new Node(currentNode, neighbor, currentNode.g + Vector2.Distance(neighbor, target), h(neighbor, target));
                if (newNode.position != target)
                {
                    if (!ShouldBeSkipped(newNode))
                    {
                        open.Add(newNode);
                    }
                }
                else
                {
                    closed.Add(newNode);
                    targetFound = true;
                    break;
                }
            }
            iterations++;
        }

        if(iterations < maxIter-1)
        {
            var tempNode = closed[closed.Count - 1];
            while (tempNode != null)
            {
                if (tempNode.parent != null)
                {
                    path.Add(tempNode.position);
                }
                tempNode = tempNode.parent;
            }
            path.Reverse();
        }
        open.Clear();
        closed.Clear();
        return targetFound;
    }
}