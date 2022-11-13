using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Reload()
    {
        audioSource.Play();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void BackMenu()
    {
        audioSource.Play();
        SceneManager.LoadScene(0);
    }
}
