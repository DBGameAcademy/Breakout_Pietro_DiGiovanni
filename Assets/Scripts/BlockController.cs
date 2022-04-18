using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject BlockPrefab;
    public GameObject ExtraBlockPrefab;

    List<Vector3> BlockPositions = new List<Vector3>();
    public Block[] Blocks;

    public static BlockController Instance = null;

    void Awake()
    {
        Instance = Instance == null ? this : null;

        Blocks = GetComponentsInChildren<Block>();

        foreach (Block block in Blocks)
        {
            BlockPositions.Add(block.transform.position);
        }

        ResetBlocks();

    }

    void Start()
    {
        StartCoroutine(SpawnRandomBlock());
    }

    public void ResetBlocks()
    {
        for (int i = Blocks.Length - 1; i >= 0; i--)
        {
            if (Blocks[i] != null)
            {
                Destroy(Blocks[i].gameObject);
            }
        }


        for (int i = 0; i < BlockPositions.Count; i++)
        {
            GameObject newBlockObj = GameObject.Instantiate(BlockPrefab, BlockPositions[i], Quaternion.identity, transform);
            Blocks[i] = newBlockObj.GetComponent<Block>();
        }
    }

    public int BlocksCount()
    {
        return gameObject.transform.childCount;
    }


    IEnumerator SpawnRandomBlock()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 15));

            Instantiate(ExtraBlockPrefab, RandomSpawnPosition(), Quaternion.identity);
        }
    }

    Vector2 RandomSpawnPosition()
    {
        return new Vector2(Random.Range(-2f, 2f), Random.Range(-0.5f, 0.8f));
    }
}
