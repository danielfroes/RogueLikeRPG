using System.Collections.Generic;
using System.Collections;
using DirectionSystem;
using UnityEngine;


public class EnemySelector : MonoBehaviour
{
	[SerializeField] private float testTimer;
					 public  bool  menuIsOpen;


	[SerializeField] private List<Transform> enemyList;
	[SerializeField] private GameObject selectionCursor;
	[SerializeField] private GameObject enemyHolder;
	[SerializeField] private int selectedEnemyIndex = 0;


	private void Start()
	{
		enemyList = new List<Transform>();
		for (int i = 0; i < enemyHolder.transform.childCount; i++)
		{
			enemyList.Add(enemyHolder.transform.GetChild(i));
		}
	}


	public bool SelectEnemy(Direction direction)
	{
		if (menuIsOpen)
		{
			if (transform.GetChild(selectedEnemyIndex) == null) selectedEnemyIndex = 0;
			
			switch (direction)
            {
                case Direction.left:
                    int tmp1 = selectedEnemyIndex - 1;
					if (tmp1 >= 0) selectedEnemyIndex -= 1;
					else selectedEnemyIndex = enemyHolder.transform.childCount - 1;
                    break;
                case Direction.right:
                    int tmp2 = selectedEnemyIndex + 1;
					if (tmp2 < enemyHolder.transform.childCount) selectedEnemyIndex += 1;
					else selectedEnemyIndex = 0;
                    break;
            }
	
			selectionCursor.transform.position = transform.GetChild(selectedEnemyIndex).position;
		}

		return true;
	}
}
