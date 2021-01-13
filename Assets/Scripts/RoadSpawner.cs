using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject startBlock;
    public List<GameObject> blocks;

    int blocksCount = 2;
    float blockLength = 10;
    bool initialized = false;

    List<GameObject> currentBlocks = new List<GameObject>();

    public void Initialaze()
    {
        if (!initialized)
        {
            initialized = true;
            StartGame();
        }
    }

    public void StartGame()
    {
        currentBlocks.Add(startBlock);
        for (int i = 0; i < blocksCount; i++)
        {
            SpawnBlock();
        }
    }

    void LateUpdate()
    {
        CheckForSpawn();
    }

    void CheckForSpawn()
    {
        if (blocks.Count > 0 && currentBlocks.Count > 0)
        {
            if (currentBlocks[0].transform.localPosition.z < -10)
            {
                SpawnBlock();
                DestroyBlock();
            }
        }
    }

    void SpawnBlock()
    {
        GameObject block = Instantiate(blocks[0], transform);

        float ZPos = currentBlocks[currentBlocks.Count - 1].transform.localPosition.z + blockLength;

        block.transform.localPosition = new Vector3(0, 0, ZPos);
        currentBlocks.Add(block);
        blocks.RemoveAt(0);
    }

    void DestroyBlock()
    {
        Destroy(currentBlocks[0]);
        currentBlocks.RemoveAt(0);
    }
}
