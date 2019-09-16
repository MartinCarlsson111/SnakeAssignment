using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionInterface
{
    void CollisionResponse(string tag, GameObject obj);
}
