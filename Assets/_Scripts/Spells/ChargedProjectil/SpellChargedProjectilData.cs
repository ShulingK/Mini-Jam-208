using UnityEngine;

[CreateAssetMenu(fileName = "New Charged Projectil", menuName = "Spell/Charged Projectil")]
public class SpellChargedProjectilData : SpellData
{
    public override void Cast(Transform origin, Vector2 dir, bool isFacingRight)
    {
        GameObject proj = Instantiate(_prefab, origin.position, Quaternion.identity);

        if (proj.TryGetComponent<SpellChargedProjectilBehavior>(out var behavior))
        {
            behavior.Init(dir);
            behavior.Cast();
        }
    }
}
