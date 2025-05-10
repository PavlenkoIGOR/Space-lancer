using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_lancer
{
    public class AI_PointPatrol : MonoBehaviour
    {
        [SerializeField] private float _radius;
        public float radius => _radius;

        private static readonly Color _gizmoColor = new Color(1,0,0,0.3f);
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
