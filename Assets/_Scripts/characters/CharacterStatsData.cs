using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats/CharacterStatsData")]
public class CharacterStatsData : ScriptableObject
{
    public float maxHP = 100f;
    public float moveSpeed = 5f;
    public float damage = 10f;
}