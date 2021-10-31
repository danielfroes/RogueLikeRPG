using System.Collections.Generic;
using System.Collections;
using DirectionSystem;
using UnityEngine;


public class EnemySelector : MonoBehaviour
{
					 public  List<Transform> enemyList;
	[SerializeField] private Transform  selectionCursor;
	[SerializeField] private GameObject enemyHolder;
					 public  GameObject selectedEnemy;
	[SerializeField] private int selectedEnemyIndex = 0;
	[SerializeField] private float heightCorrection;
					 private Vector2 selectionCursorPosition;


	private void Start()
	{
		enemyList = new List<Transform>();
		StartCoroutine(UpdateEnemyList());
	}


	public IEnumerator UpdateEnemyList()
	{
		enemyList.Clear();

		for (int i = 0; i < enemyHolder.transform.childCount; i++)
		{
			if (!enemyHolder.transform.GetChild(i).gameObject.GetComponent<Squeak.EnemyStatusController>().isDead)
			{
				enemyList.Add(enemyHolder.transform.GetChild(i));
			}
		}

		selectedEnemyIndex = 0;
		selectedEnemy = enemyList[selectedEnemyIndex].gameObject;
		selectionCursorPosition = new Vector2 (enemyList[selectedEnemyIndex].position.x, 
											   enemyList[selectedEnemyIndex].position.y + heightCorrection);
		selectionCursor.position = selectionCursorPosition;

		enemyList.TrimExcess();

		yield return null;
	}


	public void SelectEnemy(Direction direction)
	{		
		if (enemyList[selectedEnemyIndex].gameObject.GetComponent<Squeak.EnemyStatusController>().isDead
			|| enemyList[selectedEnemyIndex] == null)
		{
			StartCoroutine(UpdateEnemyList());
			return;
		}

		switch(direction)
		{
			case Direction.left:
				int tmp1 = selectedEnemyIndex - 1;

				if (tmp1 >= 0)
				{
					selectedEnemyIndex -= 1;
				}
				else
				{
					selectedEnemyIndex = enemyList.Count - 1;
				}

				break;

			case Direction.right:
				int tmp2 = selectedEnemyIndex + 1;

				if (tmp2 < enemyList.Count)
				{
					selectedEnemyIndex += 1;
				}
				else
				{
					selectedEnemyIndex = 0;
				}

				break;
		}

		selectedEnemy = enemyList[selectedEnemyIndex].gameObject;
		selectionCursorPosition = new Vector2 (enemyList[selectedEnemyIndex].position.x, 
											   enemyList[selectedEnemyIndex].position.y + heightCorrection);
		selectionCursor.position = selectionCursorPosition;
	}
}
