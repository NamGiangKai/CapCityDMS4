using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;  
    public Transform bossSpawnPoint;  
    private GameObject spawnedBoss;  
    public float bossDuration = 10f;  // thời gian của boss
    private bool isBossActive = false;  

    void Update()
    {
        CheckScore();
    }

    public void CheckScore()
    {
        int score = Mathf.RoundToInt(GameManager.Instance.currentScore);

       
        if (score % 50 == 0 && score != 0)
        {
            if (!isBossActive)
            {
                StartCoroutine(SpawnBossForDuration(bossDuration));
            }
        }
    }

    private IEnumerator SpawnBossForDuration(float duration)
    {
        SpawnBoss();
        yield return new WaitForSeconds(duration);
        DespawnBoss();
    }

    private void SpawnBoss()
    {
        spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        isBossActive = true;      
        Debug.Log("Boss xuất hiện!");
    }

    private void DespawnBoss()
    {
        Destroy(spawnedBoss);
        spawnedBoss = null;
        isBossActive = false;
        Debug.Log("Boss biến mất!");
    }
}
