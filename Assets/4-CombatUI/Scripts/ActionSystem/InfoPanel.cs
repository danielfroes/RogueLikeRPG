using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private TextMeshProUGUI details = null;
    [SerializeField] private TextMeshProUGUI castTime = null;
    [SerializeField] private TextMeshProUGUI damage = null;
    
    // Start is called before the first frame update
    public void SetInfoPanelText(Action action) {
        title.SetText(action.actionName);
        details.SetText(action.details);
        castTime.SetText(action.castTime + "s");
        if (action is AttackAction attackAction)
            damage.SetText(attackAction.physicalDamage.ToString());

        if (action is SpellAction spellAction)
        {
            damage.SetText(spellAction.magicalDamage.ToString());
        }
    }
}
