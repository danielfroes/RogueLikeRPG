using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Squeak
{
    public abstract class BaseStat {
        public abstract float Value { get; }
    }

    public class Stat : BaseStat
    {
        private float _baseValue;
        private List<StatModifier> _modifiers;
        private bool _dirty;

        private float _currentValue;

        public override float Value
        {
            get {
                if (_dirty) {
                    CalculateValue();
                    _dirty = false;
                }
                return _currentValue;
            }
        }

        public Stat(float baseValue) {
            _baseValue = baseValue;
            _modifiers = new List<StatModifier>();
            _dirty = true;
        }

        private void CalculateValue() {
            float totalValue = _baseValue;

            float totalPercentageAdd = 1.0f;
            float totalPercentageMul = 1.0f;

            foreach (StatModifier modifier in _modifiers) {
                switch(modifier.type) {
                    case ModifierType.FLAT:
                        totalValue += modifier.value;
                        break;
                    case ModifierType.PERCENTAGE_ADD:
                        totalPercentageAdd += modifier.value;
                        break;
                    case ModifierType.PERCENTAGE_MUL:
                        totalPercentageMul *= 1.0f + modifier.value;
                        break;
                }
            }

            totalValue *= totalPercentageAdd;
            totalValue *= totalPercentageMul;
            _currentValue = totalValue;
        }

        /// <summary>
        /// Adds a modifier to the Stat.
        /// </summary>
        /// <param name="modifier">Struct modifier to be added.</param>
        public void AddModifier(StatModifier modifier) {
            _modifiers.Add(modifier);
            _dirty = true;
        }

        /// <summary>
        /// Remove modifier from Stat. If Stat doesn't exist, does nothing.
        /// </summary>
        /// <param name="modifier">Modifier to be removed.</param>
        public void RemoveModifier(StatModifier modifier) {
            _modifiers.Remove(modifier);
            _dirty = true;
        }

        /// <summary>
        /// Changes base value.
        /// </summary>
        /// <param name="newBaseValue">Base values's new value.</param>
        public void ChangeBaseValue(float newBaseValue) {
            _baseValue = newBaseValue;
            _dirty = true;
        }

    }
    
    public class StatBar : BaseStat {
        private float _currentValue;
        private float _maxValue;

        public override float Value {
            get {
                return _currentValue;
            }
        }

        public StatBar(float maxValue) {
            _maxValue = maxValue;
            _currentValue = maxValue;
        }

        /// <summary>
        /// Returns current value divided by maximum value.
        /// </summary>
        /// <returns>Current health percentage as a float between 0 and 1.</returns>
        public float GetHealthPercentage() {
            return _currentValue / _maxValue;
        }

        /// <summary>
        /// Changes maximum value.
        /// </summary>
        /// <param name="newMaxValue">New maximum value.</param>
        public void ChangeMaxValue(float newMaxValue) {
            _maxValue = newMaxValue;
        }

        /// <summary>
        /// Decreases from current value.
        /// </summary>
        /// <param name="amount">Value to be decreased.</param>
        public void Decrease(float amount) {
            _currentValue -= amount;
            if (_currentValue < 0.0f)
                _currentValue = 0.0f;
        }

        /// <summary>
        /// Restores to maximum value.
        /// </summary>
        public void Regen() {
            _currentValue = _maxValue;
        }

        /// <summary>
        /// Restores the given amount. Does not increase over maximum value.
        /// </summary>
        /// <param name="amount">Amount to be recovered.</param>
        public void Regen(float amount) {
            _currentValue += amount;
            Debug.Log(_currentValue);
            if (_currentValue > _maxValue)
                _currentValue = _maxValue;
        }

    }

    public struct StatModifier {
        public ModifierType type;
        public float value;
    };

    public enum ModifierType {
        FLAT,
        PERCENTAGE_MUL,
        PERCENTAGE_ADD,
    }
    
}
