using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDetection : MonoBehaviour
{
    public Terrain terrain; // Reference to the terrain

    public float detectionRadius = 1f; // Radius to detect trees

    private void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();   
    }

    private void Update()
    {
        
    }

    public void CheckTrees(MonsterUnit monsterUnit)
    {
        if (terrain == null)
        {
            Debug.LogWarning("Terrain or player not assigned in the inspector!");
            return;
            
        }

        TerrainData terrainData = terrain.terrainData;
        TreeInstance[] trees = terrainData.treeInstances;

        for (int i = 0; i < trees.Length; i++)
        {
            Vector3 treeWorldPosition = Vector3.Scale(trees[i].position, terrainData.size) + terrain.transform.position;

            // Check the distance between the player and the tree
            float distanceToTree = Vector3.Distance(transform.position, treeWorldPosition);

            if (distanceToTree < detectionRadius)
            {
                // Player is close to the tree
                Debug.Log("Player is close to a tree!");
                monsterUnit.treeHit(i, treeWorldPosition);
            }
        }
        
    }
}
