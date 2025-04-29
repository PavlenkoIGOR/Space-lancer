using UnityEngine;

namespace Space_lancer
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoudary";
        [SerializeField] private float _velocityDamageModifier;
        [SerializeField] private float _damageConst;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var destructable = transform.root.GetComponent<Destructable>();

            if (destructable != null)
            {
                destructable.ApplyDmg((int)_damageConst + (int)(_velocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}
