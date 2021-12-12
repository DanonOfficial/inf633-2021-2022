using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GridBrush : InstanceBrush
{
    public float gridSize = 1.0f;
    private float waterHeight = 13.5f;
    public bool fullMap = true;
    public override void draw(float x, float z)
    {
        setPrefab_idx((int)(3));
        if (!fullMap)
        {
            for (float zi = -radius; zi <= radius; zi += gridSize)
            {
                for (float xi = -radius; xi <= radius; xi += gridSize)
                {
                    if (terrain.get(x + xi, z + zi) > waterHeight)
                    {
                        spawnObject(x + xi, z + zi);
                    }

                }
            }
        }
        else
        {
            Vector3 terrainSize = terrain.terrainSize();
            float width = terrainSize.y;
            float height = width;
            for (float zi = 0; zi <= width; zi += gridSize)
            {
                for (float xi = 0; xi <= height; xi += gridSize)
                {
                    if (terrain.get(x + xi, z + zi) > waterHeight)
                    {
                        spawnObject(x + xi, z + zi);
                    }

                }
            }
        }
    }
}