using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Squeak
{
    public class PlayerStatusController : MonoBehaviour
    {
        public static PlayerStatusController Instance { get; private set; }
        public PlayerStatusPreset preset;

        public delegate void OnPlayerDeath();

        public static event OnPlayerDeath OnDeathEvent;

        public delegate void OnPlayerDamage();

        public static event OnPlayerDamage OnDamageEvent;

        private StatBar _health;
        private Stat _defense;

        public Slider _healthBar;
        public static int damageCnt = 1;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            _health = new StatBar(preset.maxHealth);
            _defense = new Stat(preset.defense);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ActionCaster.instance.CancelCasting();
            Damage(25f);
            Destroy(other.gameObject);
        }

        /// <summary>
        /// Damages player based on defense calculations.
        /// </summary>
        /// <param name="damage">Raw damage value.</param>
        public void TrueDamage(float damage)
        {
            damageCnt++;
            if (damage > 0 && !PlayerController.riposte)
                _health.Decrease(damage);

            if (_health.Value > 0.0f)
            {
                //Debug.Log($"{damage} de dano, vida atual {_health.Value}");
                OnDamageEvent?.Invoke();
            }
            else
            {
                //Debug.Log("Morte");
                OnDeathEvent?.Invoke();
            }

            _healthBar.value = _health.GetHealthPercentage();
        }

        public void Damage(float rawDamage)
        {
            float damage = rawDamage - _defense.Value;
            TrueDamage(damage);
        }

        public void Heal(float rawHeal)
        {
            _health.Regen(rawHeal);
            _healthBar.value = _health.GetHealthPercentage();

        }

        void OnDestroy()
        {
            damageCnt = 1;
            OnDeathEvent = null;
            OnDamageEvent = null;
        }
    }
}