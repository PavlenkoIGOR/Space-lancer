using UnityEngine;

namespace Space_lancer
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]
        [SerializeField] private float _parallaxPower;

        [SerializeField] private float _texruteScale;

        private Material _quad_mtl;
        private Vector2 _initialOffset;

        // Start is called before the first frame update
        void Start()
        {
            _quad_mtl = GetComponent<MeshRenderer>().material;
            _initialOffset = UnityEngine.Random.insideUnitCircle;

            _quad_mtl.mainTextureScale = Vector2.one * _texruteScale;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 offset = _initialOffset;

            offset.x += transform.position.x / transform.localScale.x / _parallaxPower;
            offset.y += transform.position.y / transform.localScale.y / _parallaxPower;

            _quad_mtl.mainTextureOffset = offset;
        }
    }
}