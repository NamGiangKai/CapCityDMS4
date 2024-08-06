using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTime = 3f;
    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    public float obstacleSpeed = 4f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;
    public int initialPoolSize = 10;

    private float _obstacleSpawnTime;
    private float _obstacleSpeed;
    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private Dictionary<GameObject, Queue<GameObject>> obstaclePools;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
        GameManager.Instance.onPlay.AddListener(ResetFactors);

        InitializePools();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;
            CalculateFactors();
            SpawnLoop();
        }
    }

    private void InitializePools()
    {
        obstaclePools = new Dictionary<GameObject, Queue<GameObject>>();

        foreach (var prefab in obstaclePrefabs)
        {
            var pool = new Queue<GameObject>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab, obstacleParent);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            obstaclePools[prefab] = pool;
        }
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = GetPooledObject(obstacleToSpawn);

        if (spawnedObstacle != null)
        {
            spawnedObstacle.transform.position = transform.position;
            spawnedObstacle.SetActive(true);

            Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
            obstacleRB.velocity = Vector2.left * _obstacleSpeed;
        }
    }

    private GameObject GetPooledObject(GameObject prefab)
    {
        if (obstaclePools.ContainsKey(prefab))
        {
            if (obstaclePools[prefab].Count > 0)
            {
                GameObject obj = obstaclePools[prefab].Dequeue();
                return obj;
            }

            // If the pool is empty, instantiate a new one and add it to the pool
            GameObject newObj = Instantiate(prefab, obstacleParent);
            newObj.SetActive(false);
            obstaclePools[prefab].Enqueue(newObj);
            return newObj;
        }

        return null;
    }

    private void ClearObstacles()
    {
        foreach (Transform child in obstacleParent)
        {
            child.gameObject.SetActive(false);
            // Optionally, you can return the objects to the pool here if needed
        }
    }

    private void CalculateFactors()
    {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void ResetFactors()
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    }
}
