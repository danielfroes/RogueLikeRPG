using System;
using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] UnityEvent _onGameOver = null;

    [SerializeField] GameObject _gameOverMenu = null;
    // Start is called before the first frame update
    void Awake()
    {
        _gameOverMenu.SetActive(false);
        PlayerStatusController.OnDeathEvent += () =>
        {
            StartCoroutine(nameof(OnGameOverCoroutine));
        };
    }

    IEnumerator OnGameOverCoroutine()
    {
        _onGameOver.Invoke();
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        _gameOverMenu.SetActive(true);
    }
    

}
