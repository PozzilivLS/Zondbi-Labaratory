using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData _boxData;
        [SerializeField] private ItemData _data;
        [Header("Physic")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private ItemPhysicsData _boxPhData;
        [SerializeField] private ItemPhysicsData _phData;
        [Header("Image")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void InitializeAsBox()
        {
            Initialize(_boxData, _boxPhData);
        }

        public void InitializeAsItem()
        {
            Initialize(_data, _phData);
        }

        private void Initialize(ItemData data, ItemPhysicsData phData)
        {
            _rigidbody.mass = phData.Mass;
            _rigidbody.linearDamping = phData.LinearDamping;
            _rigidbody.angularDamping = phData.AngularDamping;

            _spriteRenderer.sprite = data.Sprite;
        }
    }
}
