using System;
using System.Collections;
using DirectionSystem;
using UnityEngine;

namespace Squeak
{
    public class PlayerController : MonoBehaviour
    {
        public InputListener inputListener;

        // movement stuff
        public float movementDuration;
        public float movementBufferTime;
        public AnimationCurve transitionCurve;

        private Vector2 currentInputDirection = Vector2.zero;

        // lots of bools for state management
        private bool _moving;
        private bool _casting;
        private bool _damaged;
        private bool _canBuffer;
        private bool _dead;

        // components
        private Animator _animator;

        // other stuff
        private Vector2 Position => CombatGrid.Instance.PositionToCellCenter(transform.position);

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            PlayerStatusController.OnDamageEvent += () =>
            {
                CancelCasting();
                StopAllCoroutines();
                StartCoroutine(Damage());
            };
            PlayerStatusController.OnDeathEvent += Die;
        }

        private void Update()
        {
            if (!_moving && currentInputDirection != Vector2.zero)
            {
                StartCoroutine(Move(DirectionUtils.Vec3ToDir(currentInputDirection)));
                currentInputDirection = Vector2.zero;
            }
        }

        public void ManageInput(Vector2 direction)
        {
            if (Time.time == 0f)
                return;

            if (_damaged)
                return;

            if (_casting)
            {
                CancelCasting();
                currentInputDirection = direction;
            }

            if (!_moving || _canBuffer)
            {
                currentInputDirection = direction;
            }
        }

        private IEnumerator Move(Direction direction)
        {
            Vector2 origin = Position;
            Vector2 destination =
                Position + DirectionUtils.DirToVec3(direction) * CombatGrid.Instance.DistanceBetweenTiles;

            if (!CombatGrid.Instance.IsPositionInGrid(destination))
            {
                _animator.Play("Idle");
                yield break;
            }

            // Animation
            switch (direction)
            {
                case Direction.up:
                    _animator.Play("MovingUp");
                    break;
                case Direction.down:
                    _animator.Play("MovingDown");
                    break;
                case Direction.left:
                    _animator.Play("MovingLeft");
                    break;
                case Direction.right:
                    _animator.Play("MovingRight");
                    break;
            }

            // Movement
            _moving = true;
            float time = movementDuration;

            while (time > 0f)
            {
                if (time <= movementBufferTime)
                    _canBuffer = true;

                float t = 1f - (time / movementDuration);
                t = transitionCurve.Evaluate(t);

                Vector3 currentPosition = Vector3.zero;
                currentPosition.x = Mathf.Lerp(origin.x, destination.x, t);
                currentPosition.y = Mathf.Lerp(origin.y, destination.y, t);

                transform.position = currentPosition;

                yield return null;
                time -= Time.deltaTime;
            }

            transform.position = destination;
            _canBuffer = false;

            if (currentInputDirection == Vector2.zero)
                _animator.Play("Idle");

            _moving = false;
        }

        private IEnumerator Damage()
        {
            inputListener.enabled = false;
            _damaged = true;
            _animator.Play("Damage");
            transform.position = Position;

            StartCoroutine(FreezeFrame(0.25f));

            yield return new WaitForSeconds(0.25f);
            currentInputDirection = Vector2.zero;
            _moving = false;
            _canBuffer = false;

            _animator.Play("Idle");
            _damaged = false;
            inputListener.enabled = true;
        }

        private void Die()
        {
            inputListener.enabled = false;

            CancelCasting();
            StopAllCoroutines();
            _animator.Play("Death");
            enabled = false;
        }

        public void Cast(Action action)
        {
            _casting = true;
            StartCoroutine(WaitCast((action)));
        }

        private void CancelCasting()
        {
            if (!_casting)
                return;
            _casting = false;
            StopAllCoroutines();
            ActionCaster.instance.CancelCasting();
        }

        private IEnumerator WaitCast(Action action)
        {
            _animator.Play("Charge");
            yield return new WaitForSeconds(action.castTime);
            _animator.Play("Attack");
            _casting = false;
        }

        private IEnumerator FreezeFrame(float time)
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = 1f;
        }
    }
}