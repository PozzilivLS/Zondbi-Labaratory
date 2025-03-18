using System.Collections;
using UnityEngine;
using Items;

namespace Triggers
{
    public class FireplaceLogic : MonoBehaviour
    {
        [Header("Item parameters")]
        [SerializeField] private Transform _targetPosition;
        [SerializeField] private float _durationMove = 2f;
        [SerializeField] private AnimationCurve _curveWay;

        private bool _potionInTrigger;
        private Item _activePotion;

        private void OnPlayerDropPotion(bool IsDrag)
        {
            if (_potionInTrigger && IsDrag is false)
            {
                StartCoroutine(MoveToTarget());
            }
        }

        private IEnumerator MoveToTarget()
        {
            var startPosition = _activePotion.transform.position;
            var elapsedTime = 0f;

            while (elapsedTime <= 1f)
            {
                elapsedTime += Time.deltaTime / _durationMove;
                var curveValue = _curveWay.Evaluate(elapsedTime);

                _activePotion.transform.position = Vector2.Lerp(startPosition, _targetPosition.position, curveValue);
                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var item = collision.GetComponentInParent<Item>();

            if (item)
            {
                _potionInTrigger = true;
                _activePotion = item;
                _activePotion.GrabItemStatus += OnPlayerDropPotion;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var item = collision.GetComponentInParent<Item>();

            if (item)
            {
                _potionInTrigger = false;
                _activePotion.GrabItemStatus -= OnPlayerDropPotion;
                _activePotion = null;
            }
        }
    }
}