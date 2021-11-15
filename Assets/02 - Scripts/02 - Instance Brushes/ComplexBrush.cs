using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ComplexBrush : InstanceBrush
{
    public enum TreeType
    {
        BROADLEAF_TREE = 0,
        PALM_TREE = 1,
        BUSH = 2
    }
    public float maxAltitude = 10.0f;
    public float maxSteepnes = 1.0f;
    public float broadLeafMaxHeight = 10f;
    public float broadPalmTreeMaxHeight = 20f;
    TreeType treeType = TreeType.BROADLEAF_TREE;

    public override void draw(float x, float z)
    {
        float distance = Random.Range(0, radius);
        float theta = Random.Range(0, 2 * Mathf.PI);
        Vector2 resultVector = new Vector2(0, distance);
        resultVector = Quaternion.Euler(0, 0, theta) * resultVector;
        float resultX = x + resultVector.x;
        float resultZ = z + resultVector.y;
        float currentHeight = terrain.get(resultX, resultZ);
        print(currentHeight);
        if (currentHeight < broadLeafMaxHeight)
        {
            treeType = TreeType.BROADLEAF_TREE;
        }
        else if (currentHeight < broadPalmTreeMaxHeight)
        {
            treeType = TreeType.PALM_TREE;
        }
        else
        {
            treeType = TreeType.BUSH;
        }
        if (maxSteepnes < terrain.getSteepness(resultX, resultZ))
        {
            return;
        }
        print((int)(treeType));
        setPrefab_idx((int)(treeType));
        spawnObject(resultX, resultZ);

    }
}