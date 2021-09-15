using System;
using UnityEngine;

namespace Scripts.ActionSystem
{
    [CreateAssetMenu(fileName = "new PlayerAction Button Color Setting", menuName = "PlayerAction Menu SO/Settings/Buttons Color Settings", order = 0)]
    public class ActionButtonColorSettings : ScriptableObject
    {
        public Color interactableTextColor;
        public Color uniteractableTextColor;
        public Color attackColor;
        public Color spellColor;


        public Color GetActionColor(PlayerAction action)
        {
            return action.actionType == ActionType.Attack ? attackColor : spellColor;
        }
    }
}