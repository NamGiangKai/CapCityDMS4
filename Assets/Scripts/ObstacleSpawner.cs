using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;               // Array for regular obstacles
    [SerializeField] private GameObject[] specialObstaclePrefabs;        // Array for special obstacles
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
    private bool spawnSpecialObstacle = false;

    private Dictionary<GameObject, Queue<GameObject>> obstaclePools;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
        GameManager.Instance.onPlay.AddListener(ResetSpawner);

        InitializePools();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;
            CalculateFactors();
            CheckScore();
            SpawnLoop();
        }
    }

    private void InitializePools()
    {
        obstaclePools = new Dictionary<GameObject, Queue<GameObject>>();

        // Initialize pool for regular obstacles
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

        // Initialize pool for special obstacles
        foreach (var specialPrefab in specialObstaclePrefabs)
        {
            var specialPool = new Queue<GameObject>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(specialPrefab, obstacleParent);
                obj.SetActive(false);
                specialPool.Enqueue(obj);
            }

            obstaclePools[specialPrefab] = specialPool;
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
        GameObject obstacleToSpawn;

        if (spawnSpecialObstacle)
        {
            obstacleToSpawn = specialObstaclePrefabs[Random.Range(0, specialObstaclePrefabs.Length)];
        }
        else
        {
            obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        }

        GameObject spawnedObstacle = GetPooledObject(obstacleToSpawn);

        if (spawnedObstacle != null)
        {
            spawnedObstacle.transform.position = transform.position;
            spawnedObstacle.SetActive(true);

            Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();

            // Debug log to check the Rigidbody2D component
            if (obstacleRB == null)
            {
                Debug.LogError("Rigidbody2D component not found on " + spawnedObstacle.name);
                return;
            }

            Debug.Log("Setting velocity. Obstacle speed: " + _obstacleSpeed);
            Vector2 movement = new Vector2(-_obstacleSpeed, obstacleRB.velocity.y);
            obstacleRB.velocity = movement;

            // Debug log to verify velocity assignment
            Debug.Log("Velocity set to: " + obstacleRB.velocity);
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
        }
    }

    private void CalculateFactors()
    {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void ResetSpawner()
    {
        // Reset the time alive and factors
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;

        // Reset the special obstacle flag
        spawnSpecialObstacle = false;

        // Ensure all obstacles are cleared
        ClearObstacles();
    }

    public void CheckScore()
    {
        int score = Mathf.RoundToInt(GameManager.Instance.currentScore);

        // Check if the score is a multiple of 20
        if (score % 20 == 0 && score != 0)
        {
            if (!spawnSpecialObstacle)
            {
                StartCoroutine(SpawnSpecialObstacleForDuration(20f)); // Adjust the duration as needed
            }
        }
    }

    private IEnumerator SpawnSpecialObstacleForDuration(float duration)
    {
        spawnSpecialObstacle = true;
        yield return new WaitForSeconds(duration);
        spawnSpecialObstacle = false;
    }
}
