using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static int health = 100;
    [SerializeField] TextMeshProUGUI healthTextRef;
    public static TextMeshProUGUI healthText;

    private void Start()
    {
        healthText = healthTextRef;
        UpdateHealthText();
    }
    private void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public static void UpdateHealthText()
    {
        healthText.text = health + " HP";
    }
}
