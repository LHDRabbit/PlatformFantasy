using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerExtension
{
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.4f;

        Physics2D.CircleCast(rigidbody.position, radius, direction, distance);
        return true;
    }
}
