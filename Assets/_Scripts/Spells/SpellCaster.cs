using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Transform castPoint;
    [SerializeField] private Spell[] spells;

    public bool CanCast(int index)
    {
        if (index < 0 || index >= spells.Length) return false;

        return spells[index].CanCast();
    }

    public void CastSpell(int index, Vector2 dir)
    {
        if (index < 0 || index >= spells.Length) return;

        spells[index].Cast(castPoint, dir);
    }
}