using UnityEngine;

namespace Space_lancer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _livesQuantity;
        [SerializeField] private SpaceShip _ship;
        [SerializeField] private GameObject _playerShipPrefab;

        [SerializeField] private CameraController _cameraController;    
        [SerializeField] private MovementController _movementController;

        
        // Start is called before the first frame update
        void Start()
        {
            _ship.eventOnDeath.AddListener(OnShipDeath);
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnShipDeath()
        {
            _livesQuantity--;

            if (_livesQuantity > 0)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(_playerShipPrefab);

            _ship = newPlayerShip.GetComponent<SpaceShip>();

            _cameraController.SetTarget(_ship.transform);
            _movementController.SetTargetShip(_ship);
        }
    }
}
