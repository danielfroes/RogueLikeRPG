using System;
using UnityEngine;

namespace Scripts.ActionSystem
{
    [CreateAssetMenu(fileName = "new Action Button Color Setting", menuName = "Action Menu SO/Settings/Buttons Color Settings", order = 0)]
    public class ActionButtonColorSettings : ScriptableObject
    {
        public Color interactableTextColor;
        public Color uniteractableTextColor;
        public Color attackColor;
        public Color spellColor;
        public Color comboColor;


        //public Color GetActionColor(Action action)
        //{
        //    return action.actionType == ActionType.Attack ? attackColor : spellColor;
        //}
        public Color GetActionColor(Action action)
        {
            if (ActionCaster.instance.GonnaCombo() && (action.comboDamage>0 || action.combo != null)) { return comboColor; }
            return action.actionType == ActionType.Attack ? attackColor : spellColor;
        }
    }
}