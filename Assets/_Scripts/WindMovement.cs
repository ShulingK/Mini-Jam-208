using UnityEngine;

public class WindMovement : MonoBehaviour
{
    [SerializeField] private Vector2 baseDirection = Vector2.left;
    [SerializeField] private float speed = 5f;

    [Header("Variation")]
    [SerializeField] private float noiseStrength = 1.5f;
    [SerializeField] private float noiseSpeed = 1f;

    private float randomOffset;

    private void Start()
    {
        // chaque particule a son propre mouvement
        randomOffset = Random.Range(0f, 100f);

        // légère variation de vitesse
        speed *= Random.Range(0.8f, 1.2f);
    }

    private void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * noiseSpeed, randomOffset);

        // transforme le noise en oscillation (-1 à 1)
        float offset = (noise - 0.5f) * 2f;

        Vector2 perpendicular = new Vector2(-baseDirection.y, baseDirection.x);

        Vector2 finalDir = baseDirection + perpendicular * offset * noiseStrength;

        transform.position += (Vector3)(finalDir.normalized * speed * Time.deltaTime);
    }
}