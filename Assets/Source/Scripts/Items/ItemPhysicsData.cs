using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemPhysicsData", menuName = "Scriptable Objects/ItemPhysicsData")]
    public class ItemPhysicsData : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public float Mass { get; private set; }
        [field: SerializeField] public float LinearDamping { get; private set; }
        [field: SerializeField] public float AngularDamping { get; private set; }
    }
}
