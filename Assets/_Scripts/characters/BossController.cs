using System;
using System.Collections;
using UnityEngine;

public class BossController : EnemyController
{
    [SerializeField] Animator WallOz;

    protected override void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        GetComponent<Animator>().SetTrigger("Death");

        yield return new WaitForSeconds(0.1f);

        WallOz.SetTrigger("Open");

        Debug.Log("Win");

        yield return new WaitForSeconds(5f);

        GameManager.Instance.GoMainMenu();
    }
}
