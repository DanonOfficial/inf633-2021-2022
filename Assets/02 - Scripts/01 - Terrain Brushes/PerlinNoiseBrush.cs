using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseBrush : TerrainBrush
{
    public float multiplier = 10;
    
    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float perlinNoiseValue = Mathf.PerlinNoise( ( (float)xi + radius + Random.Range(0, 100)) / (2 * radius + 100), ((float)zi + radius + Random.Range(0, 100)) / (2 * radius + 100));
                print(perlinNoiseValue);
                print(Random.Range(0, 100));
                terrain.set(x + xi, z + zi, perlinNoiseValue * multiplier);
            }
        }
    }
}
