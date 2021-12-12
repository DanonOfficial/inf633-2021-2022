using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GPUGardener : MonoBehaviour
{
	public int count = 10;
	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;
	public bool gridSpawn = true;
	public float gridSize = 10.0f;
	public float waterHeight = 13.5f;
	protected CustomTerrain terrain;
	void Start()
	{
		terrain = GetComponent<CustomTerrain>();
		Vector3 terrainSize = terrain.terrainSize();
		var terrainData = Terrain.activeTerrain.terrainData;
		float width = terrainSize.x;
		float height = width;
		print(width);
		print(height);
		if (gridSpawn)
		{
			for (float zi = 0; zi <= width; zi += gridSize)
			{
				for (float xi = 0; xi <= height; xi += gridSize)
				{
					if (terrain.get(xi,zi) > waterHeight)
                    {
						var instance = Instantiate(getRandomGrass()) as GameObject;
						float x = xi + UnityEngine.Random.Range(0f, gridSize/3.0f);
						float z = zi + UnityEngine.Random.Range(0f, gridSize / 3.0f);
						Vector3 position = terrain.getInterp3(x, z);// new Vector3(xi, terrain.getInterp(xi, zi), zi);\
						position.y = terrainData.GetInterpolatedHeight(x / width, z / height);
						instance.transform.position = position;
						float scale_diff = Mathf.Abs(terrain.max_scale - terrain.min_scale);
						float scale_min = Mathf.Min(terrain.max_scale, terrain.min_scale);
						float scale = (float)CustomTerrain.rnd.NextDouble() * scale_diff + scale_min;
						instance.transform.localScale = new Vector3(scale, scale, scale);
					}

				}
			}
		}
		else
		{
			for (int i = 0; i < count; i++)
			{
				var instance = Instantiate(getRandomGrass()) as GameObject;
				instance.transform.position = GetRandomPosition(width, height);
				instance.transform.localScale = GetRandomScale();
			}
		}

	}

	private GameObject getRandomGrass()
    {
		float value = UnityEngine.Random.Range(0.0f, 1f);
		if (value < 0.25f)
		{
			return prefab2;
		} else if (value < 0.75f)
        {
			return prefab1;
        }
        else
        {
			return prefab3;
        }
	}

	private Vector3 GetRandomScale()
	{
		float scale = UnityEngine.Random.Range(0.9f, 1.5f);
		return new Vector3(1, 1, 1);
	}

	private Vector3 GetRandomPosition(float width, float height)
	{
		float x = UnityEngine.Random.Range(0f, width);
		float z = UnityEngine.Random.Range(0f, height);
		float currentValue = terrain.get(x, z);
		return new Vector3(x, currentValue, z);
	}
}