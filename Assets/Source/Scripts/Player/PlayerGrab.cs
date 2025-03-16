using Additions;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private Transform _grabParent;
        [SerializeField] private Rigidbody2D _grabRb;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _grabRadius;

        [Header("Line")]
        [SerializeField] private LineRenderer _line;
        [SerializeField] private int _linePointsCount = 5;
        [SerializeField] private float _bezierOffset;

        private PlayerInput _input;

        private Transform _grabObject;
        private Rigidbody2D _grabObjectRb;
        bool _isGrabbing = false;

        internal void Initialize(PlayerInput input)
        {
            _input = input;
            _input.GrabStateChanged += OnGrab;
        }

        private void Update()
        {
            UpdateTrail();
        }

        private void UpdateTrail()
        {
            if (_isGrabbing && _grabObject != null)
            {
                Vector3 directionFromPlayer = _input.MoveDirection;
                Vector3 directionFromObject = _grabObjectRb.linearVelocity.normalized;

                Vector2 playerOffsetPoint = transform.position + directionFromPlayer * _bezierOffset;
                Vector2 objectOffsetPoint = _grabObject.position + directionFromObject * _bezierOffset;

                for (int i = 0; i < _linePointsCount; i++)
                {
                    float t = i / (float)(_linePointsCount - 1);

                    _line.SetPosition(i, BezierCurveByT(transform.position, playerOffsetPoint, objectOffsetPoint, _grabObject.position, t));
                }
            }
        }

        private void OnGrab(bool isGrab)
        {
            if (isGrab)
                Grab();
            else
                Realize();
        }

        private void Grab()
        {
            _isGrabbing = true;

            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _grabRadius, Vector2.zero, 0f, _layerMask);

            if (hits.Length == 0 )
            {
                return;
            }

            float minDistance = hits[0].distance;
            RaycastHit2D nearestHit = hits[0];

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.distance < minDistance)
                {
                    nearestHit = hit;
                    minDistance = hit.distance;
                }
            }

            _grabObject = nearestHit.collider.transform;

            _grabParent.position = _grabObject.position;

            _grabObject.parent = _grabParent;
            _grabObjectRb = _grabObject.GetComponent<Rigidbody2D>();
            _grabObjectRb.bodyType = RigidbodyType2D.Kinematic;

            _line.positionCount = _linePointsCount;
        }

        private void Realize()
        {
            if (_isGrabbing && _grabObject != null)
            {
                _isGrabbing = false;

                _grabObject.parent = null;
                _grabObjectRb.bodyType = RigidbodyType2D.Dynamic;
                _grabObjectRb.angularVelocity = _grabRb.angularVelocity;
                _grabObjectRb.linearVelocity = _grabRb.linearVelocity;

            }
            _grabObject = null;

            _line.positionCount = 0;
        }

        private Vector2 BezierCurveByT(Vector2 pos1,  Vector2 pos2, Vector2 pos3, Vector2 pos4, float t)
        {
            print(3 * t * (1 - t) * (1 - t));
            return ((1 - t) * (1 - t) * (1 - t) * pos1) + (3 * t * (1 - t) * (1 - t) * pos2) + (3 * t * t * (1 - t) * pos3) + (t * t * t * pos4);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(transform.position, _grabRadius);
        }
    }
}
