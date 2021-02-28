using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StartTimer : MonoBehaviour
{
    [SerializeField] UnityEvent _onTimerBegin;
    [SerializeField] UnityEvent _onTimerEnd;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] int countdownTime;

    bool _isTimerActive;
    void Awake()
    {
        _timerText.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (_isTimerActive) return;
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wasUpdatedThisFrame )
            {
                var startCountdownCoroutine = StartCountdown();
                StartCoroutine(startCountdownCoroutine);
            }
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.wasUpdatedThisFrame )
            {
                var startCountdownCoroutine = StartCountdown();
                StartCoroutine(startCountdownCoroutine);
            }
        }
    }


    IEnumerator StartCountdown()
    {
        _mainMenu.SetActive(false);
        _isTimerActive = true;
        Time.timeScale = 1;
        _onTimerBegin.Invoke();
        _timerText.gameObject.SetActive(true);
        for (var i = countdownTime; i >= 0; i--)
        {
            _timerText.SetText(i.ToString());
            yield return new WaitForSeconds(1);
        }
        _onTimerEnd.Invoke();
        gameObject.SetActive(false);
    }

}
