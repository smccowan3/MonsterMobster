using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TerrainStartStatePreserver: MonoBehaviour
{
    private static TreeInstance[] initialTrees;

    private void Start()
    {
        RecordInitialState();
    }

    private void RecordInitialState()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            TerrainData terrainData = terrain.terrainData;
            initialTrees = terrainData.treeInstances.Clone() as TreeInstance[];
        }
    }

    private void OnApplicationQuit()
    {
        ResetTerrain();
    }

#if UNITY_EDITOR
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        ResetTerrain();
    }
#endif

    private static void ResetTerrain()
    {
        Terrain terrain = FindObjectOfType<Terrain>();
        if (terrain != null)
        {
            TerrainData terrainData = terrain.terrainData;
            terrainData.treeInstances = initialTrees.Clone() as TreeInstance[];
            terrain.Flush();
        }
    }
}
