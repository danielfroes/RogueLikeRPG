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

        public int combo;
        private StatBar _health;
        private Stat _defense;
        private Stat _attack;
        public Stat _velocity;
        public Stat _cast_velocity;
        public Stat _energy_charge_speed;

        public Slider _healthBar;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            combo = 0;
            _health = new StatBar(preset.maxHealth);
            _defense = new Stat(preset.defense);
            _attack = new Stat(preset.attack);
            _velocity = new Stat(preset.velocity);
            _cast_velocity = new Stat(preset.castVelocity);
            _energy_charge_speed = new Stat(preset.energyChargeSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Damage(25f);
            Destroy(other.gameObject);
        }

        /// <summary>
        /// Damages player based on defense calculations.
        /// </summary>
        /// <param name="rawDamage">Raw damage value.</param>
        public void Damage(float rawDamage)
        {
            float damage = rawDamage / _defense.Value;
            if (damage > 0)
                _health.Decrease(damage);

            if (_health.Value > 0.0f)
            {
                Debug.Log($"{damage} de dano, vida atual {_health.Value}");
                OnDamageEvent?.Invoke();
            }
            else
            {
                Debug.Log("Morte");
                OnDeathEvent?.Invoke();
            }

            _healthBar.value = _health.GetHealthPercentage();
        }


        public void ComboIncrement()
        {
            StartCoroutine(ComboIncrementCoroutine());
        }

        public IEnumerator ComboIncrementCoroutine()
        {
            combo++;
            yield return new WaitForSeconds(0.5f);
            combo--;
        }


        public void SkillStatusUpdate(int type, float amount, float duration)
        {
            StatModifier modifier;
            modifier.type = ModifierType.PERCENTAGE_MUL;
            modifier.value = amount;
            switch (type)
            {
                case 1:
                    StatusUpdate(_attack, modifier, duration);
                    break;
                case 2:
                    StatusUpdate(_defense, modifier, duration);
                    break;
                case 3:
                    Debug.Log("case velocity");
                    break;
                case 4:
                    StatusUpdate(_cast_velocity, modifier, duration);
                    break;
                case 5:
                    Debug.Log("case energy charge speed");
                    break;
                default:
                    Debug.Log("default case");
                    break;
            }

        }

        public void StatusUpdate(Stat status, StatModifier mod, float duration)
        {
            StartCoroutine(SkillStatusUpdateCoroutine(status, mod, duration));
        }

        public IEnumerator SkillStatusUpdateCoroutine(Stat status, StatModifier mod, float duration)
        {
            status.AddModifier(mod);
            yield return new WaitForSeconds(duration);
            status.RemoveModifier(mod);
            yield break;
        }


        public float get_player_attack()
        {
            return _attack.Value;
        }

    }
}