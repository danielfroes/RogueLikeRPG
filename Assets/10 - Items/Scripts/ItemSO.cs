using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ItemSO")]
public class Item : ScriptableObject {
    public string nome;
    public string descricao;
    public Squeak.StatType statType;
    public Squeak.StatModifier statModifier;
}