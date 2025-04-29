using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// синхронизирует положение фона и игрока по оси X и Y, а по оси Z, фон остаётся на первоначальной координате. 
/// </summary>
namespace Space_lancer
{
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
                transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }
    }
}