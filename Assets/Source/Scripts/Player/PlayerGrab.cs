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
                Vector3 directionFromObject = _grabRb.linearVelocity.normalized;
                float objectSpeed = _grabRb.linearVelocity.magnitude;

                Vector2 objectOffsetPoint = _grabObject.position + directionFromObject * _bezierOffset * objectSpeed;

                for (int i = 0; i < _linePointsCount; i++)
                {
                    float t = i / (float)(_linePointsCount - 1);

                    _line.SetPosition(i, BezierCurveByT(transform.position, objectOffsetPoint, _grabObject.position, t));
                }
            }
        }

        private void OnGrab()
        {
            if (_isGrabbing)
                Realize();
            else
                Grab();
        }

        private void Grab()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _grabRadius, Vector2.zero, 0f, _layerMask);

            if (hits.Length == 0 )
            {
                return;
            }
            _isGrabbing = true;

            float minDistance = (transform.position - hits[0].collider.transform.position).sqrMagnitude;
            RaycastHit2D nearestHit = hits[0];

            foreach (RaycastHit2D hit in hits)
            {
                float distance = (transform.position - hit.collider.transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    nearestHit = hit;
                    minDistance = distance;
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
                _grabObject.parent = null;
                _grabObjectRb.bodyType = RigidbodyType2D.Dynamic;
                _grabObjectRb.angularVelocity = _grabRb.angularVelocity;
                _grabObjectRb.linearVelocity = _grabRb.linearVelocity;

            }
            _isGrabbing = false;

            _grabObject = null;

            _line.positionCount = 0;
        }

        private Vector2 BezierCurveByT(Vector2 pos1,  Vector2 pos2, Vector2 pos3, float t)
        {
            return ((1 - t) * (1 - t) * pos1) + (2 * t *  (1 - t) * pos2) + (t * t * pos3);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(transform.position, _grabRadius);
        }
    }
}
