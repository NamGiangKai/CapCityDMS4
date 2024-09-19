using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    private GameObject spawnedBoss;
    private Animator bossAnimator; // Reference to the Animator component
    public float bossDuration = 10f;  // Time the boss stays active
    private bool isBossActive = false;

    void Start()
    {
        GameManager.Instance.onPlayerDie.AddListener(ResetBoss); // Listen to the player's death event
        GameManager.Instance.onPlay.AddListener(ResetBoss); // Listen to the replay button event
    }

    void Update()
    {
        CheckScore();
    }

    public void CheckScore()
    {
        int score = Mathf.RoundToInt(GameManager.Instance.currentScore);

        // Check if the score is exactly 20 and if the boss is not already active
        if (score == 20 && !isBossActive)
        {
            StartCoroutine(SpawnBossForDuration(bossDuration));
        }
    }

    private IEnumerator SpawnBossForDuration(float duration)
    {
        SpawnBoss();

        // Play idle animation during the boss's active time
        yield return new WaitForSeconds(duration - 1f); // Subtracting 1 second to leave time for the disappearance animation

        // Play the boss disappear animation before despawning
        PlayDisappearAnimation();
        yield return new WaitForSeconds(1f); // Assuming the disappear animation lasts 1 second

        DespawnBoss();
    }

    private void SpawnBoss()
    {
        spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        bossAnimator = spawnedBoss.GetComponent<Animator>(); // Get the Animator component from the spawned boss

        if (bossAnimator != null)
        {
            PlayAppearAnimation(); // Play the appear animation only when the score is 20
        }

        isBossActive = true;
        Debug.Log("Boss xuất hiện!");
    }

    private void DespawnBoss()
    {
        if (spawnedBoss != null)
        {
            Destroy(spawnedBoss);
            spawnedBoss = null;
        }
        isBossActive = false;
        Debug.Log("Boss biến mất!");
    }

    private void PlayAppearAnimation()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Appear"); // Trigger the appear animation
        }
    }

    private void PlayDisappearAnimation()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Disappear"); // Trigger the disappear animation
        }
    }

    private void ResetBoss()
    {
        // Despawn the boss if the player dies during the encounter or if the replay button is pressed
        DespawnBoss();

        // Reset the flag so the boss can appear again when the player scores 20 points
        isBossActive = false;
    }
}
