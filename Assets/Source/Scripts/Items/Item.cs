//TODO: Убрать зависимость от Input.
using Additions;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData _boxData;
        [SerializeField] private ItemData _data;
        [Header("Physic")]
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private ItemPhysicsData _boxPhData;
        [SerializeField] private ItemPhysicsData _phData;
        [Header("Image")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Event<bool> GrabItemStatus;

        private bool _isGrabbed;

        public bool IsGrabbed => _isGrabbed;

        public void InitializeAsBox()
        {
            Initialize(_boxData, _boxPhData);
        }

        public void InitializeAsItem()
        {
            Initialize(_data, _phData);
        }

        public void HasGrabbed()
        {
            _isGrabbed = true;
        }

        public void DropItem()
        {
            _isGrabbed = false;
            GrabItemStatus?.Invoke(_isGrabbed);
        }

        private void Initialize(ItemData data, ItemPhysicsData phData)
        {
            _rigidBody.mass = phData.Mass;
            _rigidBody.linearDamping = phData.LinearDamping;
            _rigidBody.angularDamping = phData.AngularDamping;

            _spriteRenderer.sprite = data.Sprite;
        }
    }
}
