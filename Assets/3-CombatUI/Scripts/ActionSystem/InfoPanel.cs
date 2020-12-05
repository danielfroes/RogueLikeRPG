using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI details;
    [SerializeField] private TextMeshProUGUI castTime;
    [SerializeField] private TextMeshProUGUI damage;
    
    // Start is called before the first frame update
    public void SetInfoPanelText(Action action) {
        title.SetText(action.actionName);
        details.SetText(action.details);
        castTime.SetText(action.castTime + "s");
        if (action is AttackAction)
            damage.SetText((action as AttackAction).BaseDamage.ToString());
    }
}
