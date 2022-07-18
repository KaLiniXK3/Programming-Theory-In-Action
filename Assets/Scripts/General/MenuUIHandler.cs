using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuUIHandler : MonoBehaviour
{
    //References
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI mouseSensitivityValueText;
    [SerializeField] TextMeshProUGUI volumeValueText;

    //Variables

    void Start()
    {
        mouseSensitivitySlider.maxValue = 5;
        mouseSensitivitySlider.minValue = 0.1f;
        SetMouseSensitivity();
        SetVolume();
    }

    //Abstractions
    public void SetMouseSensitivity()
    {
        MouseLook.mouseSensitivity = mouseSensitivitySlider.value;
        mouseSensitivityValueText.text = MouseLook.mouseSensitivity.ToString("n2");
    }
    public void SetVolume()
    {
        SoundManager.instance.audioSource.volume = volumeSlider.value;
        volumeValueText.text = volumeSlider.value.ToString("n2");
    }
    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
