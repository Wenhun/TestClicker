using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UI : MonoBehaviour
{
    [SerializeField] TMP_Text hightScore;
    [SerializeField] TMP_Text currentScore;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        audioSource.Play();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Records()
    {
        audioSource.Play();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        hightScore.text = PlayerPrefs.GetInt("Hight Score").ToString();
        currentScore.text = PlayerPrefs.GetInt("Current Score").ToString();
    }

    public void Titles()
    {
        audioSource.Play();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }


    public void BackRecord()
    {
        audioSource.Play();
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void BackTitles()
    {
        audioSource.Play();
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
