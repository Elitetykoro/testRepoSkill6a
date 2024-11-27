using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
//Dit script is een hergebruikte script van een andere project (Vertical Slice) dit is voor de platformer die kan spawnen 
    [SerializeField] private GameObject platformPrefab; 
    [SerializeField] private int platformsPerRound = 4; 
    [SerializeField] private float spawnInterval = 2f; 
    [SerializeField] private Vector2 spawnRangeX = new Vector2(-5, 5); 
    [SerializeField] private Vector2 spawnRangeY = new Vector2(-3, 3); 
    [SerializeField] private float minDistance = 2f;

    private List<GameObject> activePlatforms = new List<GameObject>(); 

    private void Start()
    {
        StartCoroutine(SpawnPlatforms());
    }

    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            foreach (GameObject platform in activePlatforms)
            {
                if (platform != null) platform.GetComponent<platform>().Die(); 
            }

            activePlatforms.Clear();


            for (int i = 0; i < platformsPerRound; i++)
            {
                Vector2 spawnPosition;
                int attempts = 0;
                do
                {
  
                    float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
                    float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
                    spawnPosition = new Vector2(randomX, randomY);
                    attempts++;
                } while (!IsPositionValid(spawnPosition) && attempts < 10); 

                GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                activePlatforms.Add(newPlatform);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private bool IsPositionValid(Vector2 position)
    {
        foreach (GameObject platform in activePlatforms)
        {
            if (platform == null) continue; 
            float distance = Vector2.Distance(position, platform.transform.position);
            if (distance < minDistance) return false; 
        }
        return true;
    }
}
