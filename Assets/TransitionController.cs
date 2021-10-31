using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    [SerializeField] Image _transitionPanel;
    [SerializeField] Vector3 _initialPosition;
    [SerializeField] Vector3 _middlePosition;
    [SerializeField] Vector3 _endPosition;
    [SerializeField] float _transitionDuration;

    void Start()
    {
        _transitionPanel.gameObject.SetActive(false);
    }

    public void TranslateTransition(Action transitionCallback)
    { 
         _transitionPanel.gameObject.SetActive(true);
         _transitionPanel.transform.localPosition = _initialPosition;
         _transitionPanel.transform.DOLocalMove(_middlePosition, _transitionDuration / 2).OnComplete(() =>
         {
            transitionCallback?.Invoke();
            _transitionPanel.transform.DOLocalMove(_endPosition, _transitionDuration / 2).OnComplete((() =>
            {
                _transitionPanel.gameObject.SetActive(false);
            }));
         });
    }

}