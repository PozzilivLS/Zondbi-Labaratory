using Additions;
using UnityEngine;

namespace Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _grabRadius;

        private PlayerInput _input;

        internal void Initialize(PlayerInput input)
        {
            _input = input;
            _input.GrabStateChanged += OnGrab;
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
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _grabRadius, Vector2.zero, 0f, _layerMask);

            if (hit)
            {
                Debug.Log(hit.collider);
            }
        }

        private void Realize()
        {

        }
    }
}
