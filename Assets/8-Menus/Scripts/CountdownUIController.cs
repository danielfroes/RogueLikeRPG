using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CountdownUIController : MonoBehaviour
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

    // void Update()
    // {
    //     if (_isTimerActive) return;
    //     
    //     if (Keyboard.current != null)
    //     {
    //         if (Keyboard.current.wasUpdatedThisFrame )
    //         {
    //             var startCountdownCoroutine = CountdownCoroutine();
    //             StartCoroutine(startCountdownCoroutine);
    //         }
    //     }
    //
    //     if (Gamepad.current != null)
    //     {
    //         if (Gamepad.current.wasUpdatedThisFrame )
    //         {
    //             var startCountdownCoroutine = CountdownCoroutine();
    //             StartCoroutine(startCountdownCoroutine);
    //         }
    //     }
    // }

    public void StartCountdown(Action onCountdownEnd)
    {
        StartCoroutine(CountdownCoroutine(onCountdownEnd));
    }


    IEnumerator CountdownCoroutine(Action onCountdownEnd)
    {
        _mainMenu.SetActive(false);
        _isTimerActive = true;
        Time.timeScale = 1;
        _timerText.gameObject.SetActive(true);
        for (var i = countdownTime; i >= 0; i--)
        {
            _timerText.SetText(i.ToString());
            yield return new WaitForSeconds(1);
        }

        onCountdownEnd?.Invoke();
        _timerText.gameObject.SetActive(false);
    }

}
