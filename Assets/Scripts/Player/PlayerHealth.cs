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
            DeathEvents();
            StartCoroutine(RestartLevel());
        }
    }
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        health = 100;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void DeathEvents()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.Move(Vector3.up);
    }
    public static void UpdateHealthText()
    {
        healthText.text = health + " HP";
    }
}
