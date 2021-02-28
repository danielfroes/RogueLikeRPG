using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour {
    [SerializeField] TextMeshProUGUI title = null;
    [SerializeField] TextMeshProUGUI details = null;
    [SerializeField] TextMeshProUGUI castTime = null;
    [SerializeField] TextMeshProUGUI damage = null;
    [SerializeField] TextMeshProUGUI actionsNeeded = null;

    // Start is called before the first frame update
    public void SetInfoPanelText(Action action) {
        title.SetText(action.actionName);
        details.SetText(action.details);
        castTime.SetText("<b>"+action.castTime + "</b>s");
        actionsNeeded.SetText("<b>"+action.actionBarsNeeded + "</b>a");
        
        if (action is AttackAction attackAction)
            damage.SetText(attackAction.physicalDamage.ToString());

        if (action is SpellAction spellAction)
        {
            damage.SetText(spellAction.magicalDamage.ToString());
        }
    }
}
