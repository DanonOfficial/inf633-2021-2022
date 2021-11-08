using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBrush : TerrainBrush
{
    public float min = 0;
    public float max = 1;

    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            { 
                terrain.set(x + xi, z + zi, Random.Range(min, max));
            }
        }
    }
}
