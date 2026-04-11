using UnityEngine;

[System.Serializable]
public class Spell
{
    public SpellData data;
    private float lastCastTime = -Mathf.Infinity;

    public bool CanCast()
    {
        return Time.time >= lastCastTime + data.cooldown;
    }

    public void Cast(Transform origin, Vector2 dir)
    {
        if (!CanCast()) return;

        lastCastTime = Time.time;

        data.Cast(origin, dir, dir.x > 0);
    }

    public float GetRemainingCooldown()
    {
        float remaining = (lastCastTime + data.cooldown) - Time.time;
        return Mathf.Max(0, remaining);
    }
}