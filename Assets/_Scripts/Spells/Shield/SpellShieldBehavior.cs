using System;
using System.Collections;
using UnityEngine;

public class SpellShieldBehavior : SpellBehavior
{
    float _shieldTime;

    internal void Init(float shieldTime)
    {
        _shieldTime = shieldTime;
    }

    protected override IEnumerator SpellCoroutine()
    {
        yield return new WaitForSeconds(_shieldTime);

        Destroy(gameObject);

        yield return null;
    }

}
