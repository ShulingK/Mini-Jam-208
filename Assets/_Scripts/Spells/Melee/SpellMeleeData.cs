using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spell Melee", menuName = "Spell/Melee")]
public class SpellMeleeData : SpellData
{
    [Header("Melee Settings")]
    public float radius = 1.5f;
    public int damage = 10;
    public LayerMask enemyLayer;
    public GameObject _prefabVFX;

    public override void Cast(Transform origin, Vector2 target, bool isFacingRight)
    {
        GameObject proj = Instantiate(_prefab, origin.transform.position + new Vector3(target.x, target.y, 0), Quaternion.identity);

        if (proj.TryGetComponent<SpellMeleeBehavior>(out var behavior))
        {
            behavior.Init(origin, target, radius, damage, enemyLayer, _prefabVFX);
            behavior.Cast();
        }
    }
}
