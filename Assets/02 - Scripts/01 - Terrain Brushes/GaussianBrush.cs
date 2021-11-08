using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBrush : TerrainBrush
{
    public float sigma = 1;
    public float multiplier = 1;
    private float gaussian(int x, int y, float sigma)
    {
        float exponentialPart = (float)Mathf.Exp(-((x * x) + (y * y)) / (2 * sigma * sigma));
        return 1 / (2 * 3.14f * sigma * sigma) * exponentialPart;
    }

    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float currentValue = terrain.get(x + xi, z + zi);
                float gaussianValue = gaussian(Mathf.Abs(xi), Mathf.Abs(zi), sigma);
                terrain.set(x + xi, z + zi, currentValue + gaussianValue * multiplier);
            }
        }
    }
}
