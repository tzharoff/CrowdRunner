using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Elements")]
    [SerializeField] private Chunk[] chunksPrefabs;
    [SerializeField] private LevelSO[] levels;

    [Header("Settings")]
    [SerializeField] private float yOffset;
    [SerializeField] private bool OrderedList = false;
    [SerializeField] private int RandomLevelSize = 10;

    [Header("DEBUG")]
    [SerializeField] private bool testing = false;

    private GameObject finishLine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        if (!testing)
        {
            GenerateLevel();
            finishLine = GameObject.FindWithTag("Finish");
            return;
        }


        if (!OrderedList)
        {
            CreateRandomLevel();
        } else
        {
            CreateOrderedLevel();
            finishLine = GameObject.FindWithTag("Finish");
        }
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel;
        Debug.Log($"current level = {currentLevel}");
        currentLevel = currentLevel % levels.Length;

        LevelSO level = levels[currentLevel];

        CreatLevel(level.chunks);
    }

    private void CreatLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;
        chunkPosition.y = yOffset;

        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;

        }
    }

    private void CreateOrderedLevel()
    {
        Vector3 chunkPosition = Vector3.zero;
        chunkPosition.y = yOffset;

        for (int i = 0; i < chunksPrefabs.Length; i++)
        {
            Chunk chunkToCreate = chunksPrefabs[i];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;

        }
    }

    private void CreateRandomLevel()
    {
        Vector3 chunkPosition = Vector3.zero;
        chunkPosition.y = yOffset;

        for (int i = 0; i < RandomLevelSize; i++)
        {
            Chunk chunkToCreate = chunksPrefabs[Random.Range(0, chunksPrefabs.Length)];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;

        }
    }

    public float GetFinishZ
    {
        get {
            return finishLine.transform.position.z;
        }
    }

    public int GetLevel
    {
        get { return PlayerPrefs.GetInt("Level",0); }
    }
}
