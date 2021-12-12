using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class RemovalBrush : InstanceBrush
{


    public override void draw(float x, float z)
    {
       
        int amountOfObjects = terrain.getObjectCount();
        float smallestDinstance = Single.MaxValue;
        for (int i = 0; i < amountOfObjects; i++)
        {

            var fullTerrain = terrain.GetComponent<Terrain>();
            var array = new TreeInstance[0];
            fullTerrain.terrainData.SetTreeInstances(array, false);
        }


    }
}