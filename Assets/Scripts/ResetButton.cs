using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetButton : MonoBehaviour
{
    [SerializeField] float resetTime = 10f;
    [SerializeField] string buttonName;

    Button button;
    TMP_Text text;
    AudioSource audioSource;

    bool startTimer = false;
    float timer = 0f;

    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        timer = resetTime;
    }

    public IEnumerator Reset()
    {
        button.interactable = false;
        audioSource.Play();
        timer = resetTime;
        startTimer = true;
        yield return new WaitForSeconds(resetTime);
    }

    void Update()
    {
        if(startTimer)
        {
            timer -= Time.deltaTime;
            text.text = (Mathf.Round(timer * 10) / 10).ToString();
        }
        if(timer <= 0)
        {
            startTimer = false;
            button.interactable = true;
            text.text = buttonName;
            StopCoroutine(Reset());
        }
    }
}
