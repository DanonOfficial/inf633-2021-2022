using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GridBrush : InstanceBrush
{
    public float gridSize = 1.0f;
    public override void draw(float x, float z)
    {
        for (float zi = -radius; zi <= radius; zi += gridSize)
        {
            for (float xi = -radius; xi <= radius; xi += gridSize)
            {
                spawnObject(xi, zi);
            }
        }
    }
}