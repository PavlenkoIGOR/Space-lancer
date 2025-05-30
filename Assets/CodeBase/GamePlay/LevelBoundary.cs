using UnityEngine;

namespace Space_lancer
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
     
        [SerializeField] private float _radius;
        public float Radius => _radius;

        public enum Mode
        {
            Limit,
            Teleport
        }
        [SerializeField] private Mode _limitMode;
        public Mode LimitMode => _limitMode;


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _radius);
        }
#endif
    }

}
