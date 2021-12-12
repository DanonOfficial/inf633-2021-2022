using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTerrainGenerator : TerrainBrush
{
    public float minNoiseHeight;
    public float maxNoiseHeight;
    public float scale = 1.0f;
    public int octaves = 1;
    public float frequencyMultiplier = 0.5f;
    public float amplitudeMultiplier = 0.5f;
    private float[,] generateNoiseMap(int width, int height, float minNoiseHeight, float maxNoiseHeight, float scale, int octavesCount, float frequencyMultiplier, float amplitudeMultiplier)
    {
        float[,] result = new float[height, width];
        
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++) {

                float frequency = 1.0f;
                float perlinNoise = 0.0f;
                float amplitude = 1.0f;
                for (int k = 0; k < octavesCount; k++)
                {
                    float x = (j / (float)width / scale) * frequency;
                    float y = (i / (float)height / scale) * frequency;

                    perlinNoise += Mathf.PerlinNoise(x, y) * 10 * amplitude;

                    frequency *= frequencyMultiplier;
                    amplitude *= amplitudeMultiplier;
                }
                result[i, j] = perlinNoise;
            }
        }
        return result;
    }
    public override void draw(int x, int z)
    {
        Vector3 terrainSize = terrain.terrainSize();
        float width = terrainSize.y;
        float height = width;
        print(width);
        print(height);
        float[,] heightMap = generateNoiseMap((int)width, (int)height, minNoiseHeight, maxNoiseHeight, scale, octaves, frequencyMultiplier, amplitudeMultiplier );
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                terrain.set(i, j, heightMap[i,j]);
                //terrain.set(i, j, scale);
            }
        }
        //terrain.set(0, 0, 1);
    }
}
