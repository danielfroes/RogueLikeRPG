﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat/PlayerStatPreset")]
public class PlayerStatusPreset : ScriptableObject
{
    public float maxHealth;
    public float defense;
    public float attack;
    public float velocity;
    public float castVelocity;
    public float energyChargeSpeed;
}
