using Additions;
using UnityEngine;

namespace Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private Transform _grabParent;
        [SerializeField] private Rigidbody2D _grabRb;
        [SerializeField] private Rigidbody2D _grabLine;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _grabRadius;

        private PlayerInput _input;

        private Transform _grabObject;
        bool _isGrabbing = false;

        internal void Initialize(PlayerInput input)
        {
            _input = input;
            _input.GrabStateChanged += OnGrab;
        }

        private void Update()
        {
            if (_isGrabbing)
            {

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
            print(hits.Length);

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
            _grabObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        private void Realize()
        {
            _isGrabbing = false;

            _grabObject.parent = null;
            Rigidbody2D rb = _grabObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.angularVelocity = _grabRb.angularVelocity;
            rb.linearVelocity = _grabRb.linearVelocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(transform.position, _grabRadius);
        }
    }
}
