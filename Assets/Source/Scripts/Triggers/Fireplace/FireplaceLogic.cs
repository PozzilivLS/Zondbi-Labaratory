using UnityEngine;
using Items;

namespace Triggers
{
    public class FireplaceLogic : MonoBehaviour
    {
        private bool _potionInTrigger;
        private Item _activePotion;

        private void OnPlayerDropPotion(bool IsDrag)
        {
            if (_potionInTrigger && IsDrag is false)
            {
                print("Dropped");
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