using System;
using System.Collections.Generic;
using System.Threading;
using Enemy;
using Squeak;
using UnityEngine;

public class BattleStateController : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] CountdownUIController _countdownUIController;
    [SerializeField] ActionMenuController _actionMenu;
    [SerializeField] EnemyAttackSpawnerController _attackSpawnerController;
    [SerializeField] ActionBar _actionBar;
    [SerializeField] ActionTargetSelector _targetSelector;
    [SerializeField] TransitionController _transitionController;
    [SerializeField] PlayerWinHandler _playerWinHandler;
    [SerializeField] PlayerController _playerController;
    
    public List<EnemyStatusController> SpawnedEnemies { get; private set; } = new List<EnemyStatusController>();

    void Start()
    {
        StartNewBattle();
    }

    void StartNewBattle()
    {
        _playerController.enabled = true;
        _actionMenu.gameObject.SetActive(false);
        _actionBar.enabled = false;
        SpawnedEnemies = _enemySpawner.SpawnEnemies();
        SetOnEnemyDeathCallback();
        
        _countdownUIController.StartCountdown(() =>
        {
            _targetSelector.InitializeSelection();
            _actionMenu.gameObject.SetActive(true);
            _actionBar.enabled = true;
            _attackSpawnerController.StartAttacks();
        });
    }

    void WinBattle()
    {
        _playerController.ActivateWinComemoration();
        _attackSpawnerController.StopAttacks();
        Invoke(nameof(RestartBattle), 2f  );
    }

    void SetOnEnemyDeathCallback()
    {
        foreach (var enemy in SpawnedEnemies)
        {
            enemy.OnDeathEvent += () =>
            {
                SpawnedEnemies.Remove(enemy);
                if (SpawnedEnemies.Count == 0)
                {
                    WinBattle();
                }
            };
        }
    }

    public void RestartBattle()
    {
        _playerController.DeactivateWinComemoration();
        _attackSpawnerController.StopAttacks();
        _transitionController.TranslateTransition(() =>
        {
            StartNewBattle();
            _playerController.DeactivateWinComemoration();
        });
    }
    
    

    public void LoseBattle()
    {
        
    }
}