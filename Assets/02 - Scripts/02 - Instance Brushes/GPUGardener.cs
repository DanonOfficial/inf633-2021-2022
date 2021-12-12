using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GPUGardener : MonoBehaviour
{
	public int count = 10;
	public GameObject prefab;
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
						var instance = Instantiate(prefab) as GameObject;

						Vector3 position = terrain.getInterp3(xi, zi);// new Vector3(xi, terrain.getInterp(xi, zi), zi);\
						position.y = terrainData.GetInterpolatedHeight(xi / width, zi / height);
						//position.y = terrain.get(xi, zi);
						instance.transform.position = position;
						if (zi < 100 && xi < 100)
                        {
							print(instance.transform.position);
                        }
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
				var instance = Instantiate(prefab) as GameObject;
				instance.transform.position = GetRandomPosition(width, height);
				instance.transform.localScale = GetRandomScale();
			}
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