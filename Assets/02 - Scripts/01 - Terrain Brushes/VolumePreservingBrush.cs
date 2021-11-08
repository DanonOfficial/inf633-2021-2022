using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumePreservingBrush : TerrainBrush
{
    public float sigma = 1;
    public float multiplier = 1;
    private float preservingFunction(float x)
    {
        return 5.0f * Mathf.Sin(x * 0.7f) / (x * 0.7f) + Mathf.Cos(x / 1.4f) - 1.17f;
    }

    public override void draw(int x, int z)
    {
        float maximumRadius = radius;
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                if (xi * xi + zi * zi <= radius * radius)
                {

                    float distanceFromOrigin = new Vector2(xi, zi).magnitude;
                    float currentValue = terrain.get(x + xi, z + zi);
                    float normalizedValue = (distanceFromOrigin / maximumRadius * 9.5f); // from -1 to 1
                    print("test");
                    print(xi);
                    print(zi);
                    print(distanceFromOrigin);
                    print(normalizedValue);
                    print(preservingFunction(normalizedValue));
                    if (normalizedValue == 0)
                    {
                        terrain.set(x + xi, z + zi, 4.83f);
                    }
                    else
                    {
                        terrain.set(x + xi, z + zi, currentValue + preservingFunction(normalizedValue));
                    }
                }

            }
        }
    }
}
