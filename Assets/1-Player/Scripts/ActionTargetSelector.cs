using Squeak;
using UnityEngine;


public class ActionTargetSelector : MonoBehaviour
{

	[SerializeField] BattleStateController _battleStateController;
	
	int _selectedEnemyIndex = 0;

	public EnemyStatusController SelectedEnemy { get; private set; }

	public void InitializeSelection()
	{
		_selectedEnemyIndex = 0;
		var enemy = _battleStateController.SpawnedEnemies[_selectedEnemyIndex];
		SelectEnemy(enemy);
	}

	void SelectEnemy(EnemyStatusController enemy)
	{
		if (SelectedEnemy != null)
		{
			SelectedEnemy.OnDeathEvent -= OnSelectedEnemyDeath;
			SelectedEnemy.GetComponent<EnemyTarget>().DeactivateTarget();
		}
		
		enemy.OnDeathEvent += OnSelectedEnemyDeath;
		enemy.GetComponent<EnemyTarget>().ActivateTarget();
		
		SelectedEnemy = enemy;
	}

	public void MoveTargetToLeft()
	{
		if(_battleStateController.SpawnedEnemies.Count <= 0) return;
		
		_selectedEnemyIndex--;
		if (_selectedEnemyIndex < 0)
			_selectedEnemyIndex = _battleStateController.SpawnedEnemies.Count - 1;

		var enemy = _battleStateController.SpawnedEnemies[_selectedEnemyIndex];

		SelectEnemy(enemy);
	}
	
	public void MoveTargetToRight()
	{
		if(_battleStateController.SpawnedEnemies.Count <= 0) return;
		
		_selectedEnemyIndex++;
		_selectedEnemyIndex %= _battleStateController.SpawnedEnemies.Count;

		var enemy = _battleStateController.SpawnedEnemies[_selectedEnemyIndex];

		SelectEnemy(enemy);
	}

	void OnSelectedEnemyDeath()
	{
		if (_battleStateController.SpawnedEnemies.Count == 0) return;
		
		_selectedEnemyIndex %= _battleStateController.SpawnedEnemies.Count;
		
		SelectEnemy(_battleStateController.SpawnedEnemies[_selectedEnemyIndex]);
	}
	
}