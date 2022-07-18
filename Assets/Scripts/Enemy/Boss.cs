using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyCube
{
    //Inheritance
    [SerializeField] GameObject thanksForPlayScreen;
    
    //Polymorphism
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (health < 0)
        {
            thanksForPlayScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            MouseLook.isPauseScreenActive = true;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
