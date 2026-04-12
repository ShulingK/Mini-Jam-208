using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [SerializeField] private GameObject windPrefab;

    [Header("Zone de spawn")]
    [SerializeField] private Vector2 areaSize = new Vector2(20f, 10f);

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 1.2f;

    [Header("Direction")]
    [SerializeField] private Vector2 windDirection = Vector2.right;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWind), 0f, spawnInterval);
    }

    private void SpawnWind()
    {
        Vector2 randomOffset = new Vector2(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );

        Vector2 spawnPos = (Vector2)transform.position + randomOffset;

        GameObject wind = Instantiate(windPrefab, spawnPos, Quaternion.identity);

        // Orientation du vent
        float angle = Mathf.Atan2(windDirection.y, windDirection.x) * Mathf.Rad2Deg;
        wind.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(wind, 3f);
    }
}