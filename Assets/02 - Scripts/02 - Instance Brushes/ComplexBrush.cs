using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
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
    public float minimumDistance = 1f;
    TreeType treeType = TreeType.BROADLEAF_TREE;

    public override void draw(float x, float z)
    {
        float distance = UnityEngine.Random.Range(0, radius);
        float theta = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        Vector2 resultVector = new Vector2(0, distance);
        resultVector = Quaternion.Euler(0, 0, theta) * resultVector;
        resultVector += new Vector2(x, z);
        float resultX = resultVector.x;
        float resultZ = resultVector.y;
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
        int amountOfObjects = terrain.getObjectCount();
        float smallestDinstance = Single.MaxValue;
        for(int i = 0; i < amountOfObjects; i++)
        {
            Vector3 position = terrain.getObjectLoc(i);
            Vector2 planePos = new Vector2(position.x, position.z);
            smallestDinstance = Mathf.Min(smallestDinstance, Vector2.Distance(planePos, resultVector));
        }
        if(smallestDinstance < minimumDistance)
        {
            return;
        }
        print((int)(4));
        setPrefab_idx((int)(5));
        spawnObject(resultX, resultZ);

    }
}