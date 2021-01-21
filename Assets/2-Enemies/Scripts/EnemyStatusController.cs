using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Squeak
{
    public class EnemyStatusController : MonoBehaviour
    {
        public static EnemyStatusController Instance { get; private set; }
        public EnemyStatusPreset _preset;

        public delegate void OnEnemyDeath();
        public static event OnEnemyDeath OnDeathEvent;

        public delegate void OnEnemyDamage();
        public static event OnEnemyDamage OnDamageEvent;

        private StatBar _health;

        public Slider _healthBar;

        // +-------------------------+
        // | MonoBehaviour lifecycle |
        // +-------------------------+
        void Awake()
        {
            if (Instance == null)
                Instance = this;

            _health = new StatBar(_preset.maxHealth);
        }

        void Update()
        {
            //TESTANDO
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("dano dano");
                EnemyStatusController.Instance.Damage(120f);
            }

        }

        public void Damage(float damage)
        {
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

    }

}