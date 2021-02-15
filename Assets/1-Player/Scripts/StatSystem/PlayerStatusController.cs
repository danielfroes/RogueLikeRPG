using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Squeak
{
    public class PlayerStatusController : MonoBehaviour
    {
        public static PlayerStatusController Instance { get; private set; }
        public PlayerStatusPreset _preset;

        public delegate void OnPlayerDeath();
        public static event OnPlayerDeath OnDeathEvent;

        public delegate void OnPlayerDamage();
        public static event OnPlayerDamage OnDamageEvent;

        private StatBar _health;
        private Stat _defense;

        public Slider _healthBar;

        // +-------------------------+
        // | MonoBehaviour lifecycle |
        // +-------------------------+
        void Awake()
        {
            if (Instance == null)
                Instance = this;

            _health = new StatBar(_preset.maxHealth);
            _defense = new Stat(_preset.defense);
        }

        // +-------------+
        // | Other stuff |
        // +-------------+

        /// <summary>
        /// Damages player based on defense calculations.
        /// </summary>
        /// <param name="rawDamage">Raw damage value.</param>
        public void Damage(float rawDamage)
        {
            float damage = rawDamage - _defense.Value;
            if (damage > 0)
                _health.Decrease(damage);
            
            if (_health.Value > 0.0f) {
                Debug.Log($"{damage} de dano, vida atual {_health.Value}");
                OnDamageEvent?.Invoke();
            } else {
                Debug.Log("Morte");
                OnDeathEvent?.Invoke();
            }

            _healthBar.value = _health.GetHealthPercentage();
        }

    }

}