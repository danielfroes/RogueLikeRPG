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
    [SerializeField] UnityEvent _onTimerBegin = null;
    [SerializeField] UnityEvent _onTimerEnd = null;
    [SerializeField] GameObject _mainMenu = null;
    [SerializeField] TextMeshProUGUI _timerText = null;
    [SerializeField] int countdownTime = 0;

    bool _isTimerActive;
    void Awake()
    {
        _timerText.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (_isTimerActive ) return;

        if (CheckAnyInput())
        {
            var startCountdownCoroutine = StartCountdown();
            StartCoroutine(startCountdownCoroutine);
        }
    }


    bool CheckAnyInput()
    {
        if (Keyboard.current != null)
            return Keyboard.current.wasUpdatedThisFrame;

        if (Gamepad.current != null)
            return Gamepad.current.wasUpdatedThisFrame;

        return false;
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
