using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPainter : MonoBehaviour
{
    public Terrain terrain;
    public int textureIndex = 0; // Index of the texture layer you want to paint
    public Rect paintArea = new Rect(0f, 0f, 10f, 10f); // Define the area to paint

    void Start()
    {
        if (terrain == null)
        {
            // If the terrain reference is not set, try to find it in the scene
            terrain = Terrain.activeTerrain;
        }

        PaintTerrain();
    }

    void PaintTerrain()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain reference is missing!");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        // Convert the paint area to terrain local space
        float normalizedX = paintArea.x / terrainData.size.x;
        float normalizedY = paintArea.y / terrainData.size.z;
        float normalizedWidth = paintArea.width / terrainData.size.x;
        float normalizedHeight = paintArea.height / terrainData.size.z;

        // Calculate the alpha map position and size
        int mapX = Mathf.FloorToInt(normalizedX * terrainData.alphamapWidth);
        int mapY = Mathf.FloorToInt(normalizedY * terrainData.alphamapHeight);
        int mapWidth = Mathf.FloorToInt(normalizedWidth * terrainData.alphamapWidth);
        int mapHeight = Mathf.FloorToInt(normalizedHeight * terrainData.alphamapHeight);

        // Get the current alpha map data
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapY, mapWidth, mapHeight);

        // Set the alpha value for the specified texture index (layer)
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                splatmapData[i, j, textureIndex] = 1f; // Set to 1 to fully paint the texture
            }
        }

        // Update the alpha map
        terrainData.SetAlphamaps(mapX, mapY, splatmapData);
    }
}
