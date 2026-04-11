using UnityEngine;

[CreateAssetMenu(fileName = "New Projectil", menuName = "Spell/Projectil")]
public class SpellDataProjectil : SpellData
{
    [SerializeField] GameObject _prefab;

    public override void Cast(Transform origin, Vector2 dir, bool isFacingRight)
    {
        GameObject proj = Instantiate(_prefab, origin.position, Quaternion.identity);

        if (proj.TryGetComponent<SpellProjectilBehavior>(out var behavior))
        {
            behavior.Init(dir);
            behavior.Cast();
        }
    }
}
