using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTreeReplacer : MonoBehaviour
{
    Terrain terrain;
    public List<GameObject> replacementTrees = new List<GameObject>(); //make sure this list matches the terrain tree prototypes list
    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void RemoveAndReplaceTree(MonsterUnit monsterUnit, int treeIndex, Vector3 treeWorldPosition)
    {
        print("removing tree");
        TerrainData terrainData = terrain.terrainData;
        List<TreeInstance> trees = new List<TreeInstance>(terrainData.treeInstances);
        // Remove the tree instance at the specified index
 
        // Get the prototype index of the removed tree
        int prototypeIndex = trees[treeIndex].prototypeIndex;
        Quaternion treeRotation = Quaternion.Euler(0f, trees[treeIndex].rotation, 0f);
        trees.RemoveAt(treeIndex);
        Debug.Log("Removed tree type - Prototype Index: " + prototypeIndex);

        terrain.terrainData.treeInstances = trees.ToArray();
        terrain.Flush();
        // Instantiate a new tree prefab at the same position
        GameObject tree = Instantiate(replacementTrees[prototypeIndex], treeWorldPosition, treeRotation);
        monsterUnit.startCut(tree.GetComponent<Animator>());
    }

}
