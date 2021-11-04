using System.Collections;
using Squeak;
using UnityEngine;


public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] GameObject _gameOverMenu = null;
    [SerializeField] BattleStateController _battleStateController;
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
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        _gameOverMenu.SetActive(true);
        _battleStateController.LoseBattle();
    }
    

}
