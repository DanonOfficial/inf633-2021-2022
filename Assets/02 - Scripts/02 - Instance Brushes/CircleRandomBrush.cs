using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CircleRandomBrush : InstanceBrush
{

    public override void draw(float x, float z)
    {
        float distance = Random.Range(0, radius);
        float theta = Random.Range(0, 2 * Mathf.PI);
        Vector2 resultVector = new Vector2(0, distance);
        resultVector = Quaternion.Euler(0, 0, theta) * resultVector;
        
        spawnObject(x + resultVector.x, z + resultVector.y);
    }
}