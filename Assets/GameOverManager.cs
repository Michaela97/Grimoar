using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Image gameOverBackground;
    [SerializeField]
    private Text gameOverText;
    public static bool gameOver;
    private AudioManager _audioManager;

    private void Awake()
    {
        gameOver = false;
    }

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        GameOver(false);
    }

    void Update()
    {
        if (gameOver)
        {
            _audioManager.Stop("Skellig");
            _audioManager.PlayOnce("WastedSound");
            GameOver(true);
            StartCoroutine(GoToMenu());
            enabled = false;
        }
    }

    //this is setting Game Over image and text through scene 
    private void GameOver(bool value)
    {
        gameOverBackground.gameObject.SetActive(value);
        gameOverText.gameObject.SetActive(value);
    }

 
    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Menu");
    }

}
