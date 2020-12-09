using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject startBlock;
    public List<GameObject> blocks;

    int blocksCount = 2;
    float blockLength;

    List<GameObject> currentBlocks = new List<GameObject>();

    void Start()
    {
        blockLength = startBlock.GetComponent<BoxCollider>().bounds.size.x;
        StartGame();
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
        if (blocks.Count > 0 && currentBlocks[0].transform.position.x < -10)
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    void SpawnBlock()
    {
        GameObject block = Instantiate(blocks[0], transform);
        Vector3 blockPos;

        blockPos = currentBlocks[currentBlocks.Count - 1].transform.position + new Vector3(blockLength, 0, 0);

        block.transform.position = blockPos;
        currentBlocks.Add(block);
        blocks.RemoveAt(0);
    }

    void DestroyBlock()
    {
        Destroy(currentBlocks[0]);
        currentBlocks.RemoveAt(0);
    }
}
