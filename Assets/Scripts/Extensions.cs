using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 dir)
    {
        if(rigidbody.isKinematic)
        return false;

        float radius = .25f;
        float distance = 0.3f;
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, dir.normalized, distance, layerMask);

        return hit.collider != null;
    }

    public static bool DotTest(this Transform t1, Transform t2 , Vector2 dir)
    {
        Vector2 direction  = t2.position - t1.position;
        return Vector2.Dot(direction.normalized,dir) > 0.25f;
    }
}