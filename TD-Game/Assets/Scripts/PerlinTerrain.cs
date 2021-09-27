using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TerrainGeneration class
// Placed on a terrain game object
// Generates a Perlin noise-based heightmap
[RequireComponent(typeof(TerrainCollider))]
public class PerlinTerrain : MonoBehaviour
{

	private TerrainData myTerrainData;
	[Tooltip("The size of the terrain")]
	public Vector3 worldSize = new Vector3(200, 50, 200);
	[Tooltip("Number of vertices along X and Z axes")]
	[Min(1)]
	public int resolution = 129;
	float[,] heightArray;


	void Start()
	{
		myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;

		heightArray = new float[resolution, resolution];

		Perlin();

		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights(0, 0, heightArray);
	}

	void Update()
	{

	}

	void Perlin()
	{
		// Fill heightArray with Perlin-based values
		float xCoord = 0;
		float yCoord = 0;
		for (int i = 0; i < resolution; i++)
		{
			yCoord += 0.05f;
			xCoord = 0;
			for (int j = 0; j < resolution; j++)
			{
				xCoord += 0.05f;
				float sample = Mathf.PerlinNoise(xCoord, yCoord);
				heightArray[j, i] = sample;
			}
		}
	}
}
