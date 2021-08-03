using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class WinMenuManager : MonoBehaviour
{

    [SerializeField] UnityEvent _onWin = null;

    [SerializeField] GameObject _winMenu = null;

    [SerializeField] private EnemyStatusController _enemyStatusController;


    [SerializeField] private EnemySelector _enemySelector;
    // Start is called before the first frame update
    void Awake()
    {
        _winMenu.SetActive(false);
    }

    void Update()
    {
        if (_enemySelector.enemyList.Count == 0)
        {
            StartCoroutine(nameof(OnWinCoroutine));
        }
    }

    IEnumerator OnWinCoroutine()
    {
        _onWin.Invoke();
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        _winMenu.SetActive(true);
    }

}
