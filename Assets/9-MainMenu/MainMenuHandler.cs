using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField] private float transitionTime;
	private RectTransform rectTransform;


	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}


	public void OpenHowToPlay()
	{
		StartCoroutine(MoveMenu(1));
	}


	public void CloseHowToPlay()
	{
		StartCoroutine(MoveMenu(-1));
	}


	public void OpenArchetype()
	{
		StartCoroutine(MoveMenu(-1));
	}


	public void CloseArchetype()
	{
		StartCoroutine(MoveMenu(1));
	}


	public void SelectArchetype(int archetypeScene)
	{
		SceneManager.LoadScene(archetypeScene, LoadSceneMode.Single);
	}


	private IEnumerator MoveMenu(int dir)
	{
		var startPosition = rectTransform.localPosition;
		var endPosition   = startPosition + (dir * 490f * Vector3.right);

		float elapsedTime = 0f;

		while (elapsedTime < transitionTime)
		{
			rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / transitionTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		rectTransform.localPosition = endPosition;
	}
}
