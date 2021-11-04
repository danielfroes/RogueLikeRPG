using UnityEngine;
using UnityEngine.UI;

class EnemyTarget : MonoBehaviour
{
    [SerializeField] Image _selector;

    void Start()
    {
        DeactivateTarget();
    }

    public void ActivateTarget()
    {
        _selector.gameObject.SetActive(true);
    }
    public void DeactivateTarget()
    {
        _selector.gameObject.SetActive(false);
    }
}