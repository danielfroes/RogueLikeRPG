using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

namespace Squeak
{
    public class PlayerController : MonoBehaviour
    {
        // Movement
        [SerializeField] private float _timeToComplete = 0.18f;
        [SerializeField] private float _bufferTime = 0.04f;
        private Vector2 _currentPosition = Vector2.zero;
        private Vector2 _currentInputDirection = Vector2.zero;
        private Tweener _movementTweener = null;
        private bool _isMoving = false;
        private bool _bufferedMovement = false;

        // Animation
        [SerializeField] private EaseFunc _easeFunction = EaseFunc.Quad;
        private Animator _animator;
        private SpriteRenderer _renderer;
        private bool _wasCasting;

        // Damage
        [SerializeField] private float _invencibilityTime = 2f;
        private bool _invencibility;

        // Values from DG.Tweening.Ease out functions
        private enum EaseFunc
        {
            Linear = 1,
            Quad = 6,
            Cubic = 9,
            Quart = 12,
            Quint = 15,
            Sine = 3,
            Expo = 18,
            Circ = 21,
        }

        // MonoBehaviour lifecycle functions

        protected void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        protected void Start()
        {
            transform.position = CombatGrid.Instance.PositionToCellCenter(transform.position);
            _currentPosition = transform.position;

            PlayerStatusController.OnDamageEvent += Damage;
            PlayerStatusController.OnDeathEvent += Die;
        }

        protected void FixedUpdate()
        {
            if (!_isMoving && _currentInputDirection == Vector2.zero)
                _animator.Play("Idle");

            // Movement
            if (!_isMoving && _currentInputDirection != Vector2.zero)
            {
                if (ActionCaster.instance.isCasting)
                {
                    ActionCaster.instance.CancelCasting();
                }

                _currentPosition += _currentInputDirection * CombatGrid.Instance.DistanceBetweenTiles();

                if (!CombatGrid.Instance.IsPositionInGrid(_currentPosition))
                {
                    _currentPosition -= _currentInputDirection * CombatGrid.Instance.DistanceBetweenTiles();
                    _currentInputDirection = Vector2.zero;
                    _bufferedMovement = false;

                    _animator.Play("Idle");
                }

                if (_currentInputDirection == Vector2.left)
                {
                    _animator.Play("MovingLeft");
                }
                else if (_currentInputDirection == Vector2.right)
                {
                    _animator.Play("MovingRight");
                }
                else if (_currentInputDirection == Vector2.up)
                {
                    _animator.Play("Idle");
                }
                else if (_currentInputDirection == Vector2.down)
                {
                    _animator.Play("MovingDown");
                }

                if (_currentInputDirection == Vector2.up || _currentInputDirection == Vector2.down)
                {
                    SquashStretch(0.75f, 1.25f, _timeToComplete);
                }
                else if (_currentInputDirection == Vector2.left || _currentInputDirection == Vector2.right)
                {
                    SquashStretch(1.25f, 0.75f, _timeToComplete);
                }

                if (_bufferedMovement && _easeFunction != EaseFunc.Linear)
                {
                    // Debug.Log($"Buffered Movement");
                    Move(_currentPosition, _timeToComplete, (Ease)_easeFunction + 1);
                }
                else
                {
                    // Debug.Log($"Regular Movement");
                    Move(_currentPosition, _timeToComplete, (Ease)_easeFunction);
                }

                _isMoving = true;
                _bufferedMovement = false;
                _currentInputDirection = Vector2.zero;
            }

        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!_invencibility)
            {
                PlayerStatusController.Instance.Damage(25f);
            }
        }

        public void Die()
        {
            _animator.Play("Death");
            _invencibility = true;
            enabled = false;
        }

        public void Damage()
        {
            StartCoroutine(FreezeFrame(0.15f));

            transform.position = CombatGrid.Instance.PositionToCellCenter(transform.position);
            _currentPosition = transform.position;

            _animator.Play("Damage");
            StartCoroutine(DamageTimer(_invencibilityTime));
            
            float originalY = transform.position.y;
            transform.DOMoveY(originalY - CombatGrid.Instance.DistanceBetweenTiles().y / 2, 0.25f)
                .OnComplete(() =>
                {
                    transform.DOMoveY(originalY, 0.25f);
                });
        }

        public void GetInput(Vector2 direction)
        {
            if (!_isMoving)
            {
                HandleInput(direction);
            }
            else if (_movementTweener != null && _movementTweener.IsActive() && _movementTweener.Elapsed() >= _timeToComplete - _bufferTime)
            {
                HandleInput(direction);
                if (_currentInputDirection != Vector2.zero)
                    _bufferedMovement = true;
            }
        }

        // Other functions
        private void HandleInput(Vector2 dir)
        {
            // Character movement
            float dirX = dir.x;
            float dirY = dir.y;

            if (Mathf.Approximately(dirX, 0.0f) && Mathf.Approximately(dirY, 0.0f))
                return;

            if (Mathf.Abs(dirX) > Mathf.Abs(dirY))
            {
                _currentInputDirection = dirX > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                _currentInputDirection = dirY > 0 ? Vector2.up : Vector2.down;
            }
        }

        private void Move(Vector2 finalPosition, float duration, Ease easeFunction)
        {
            _movementTweener = transform.DOMove(finalPosition, duration)
                .SetEase(easeFunction)
                .OnComplete(() => { _isMoving = false; });
        }

        private void SquashStretch(float x, float y, float time)
        {
            Vector2 originalScale = transform.localScale;
            _renderer.transform.DOScale(new Vector2(x, y), time * 0.5f)
                .OnComplete(() =>
                {
                    _renderer.transform.DOScale(originalScale, time * 0.5f);
                });
        }

        private IEnumerator DamageTimer(float time) {
            _invencibility = true;
            yield return new WaitForSeconds(time);
            _invencibility = false;
        }

        private IEnumerator FreezeFrame(float time)
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = 1f;
        }

    }
}
