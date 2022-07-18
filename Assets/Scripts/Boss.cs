using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyCube
{
    [SerializeField] GameObject thanksForPlayScreen;
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (health < 0)
        {
            thanksForPlayScreen.SetActive(true);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
