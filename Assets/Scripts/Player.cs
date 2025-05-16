using UnityEngine;

namespace Space_lancer
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int _livesQuantity;
        [SerializeField] private SpaceShip _ship;
        [SerializeField] private GameObject _playerShipPrefab;

        [SerializeField] private CameraController _cameraController;    
        [SerializeField] private MovementController _movementController;

        private int _score;
        private int _numKills;
        public int score => _score;
        public int numKills => _numKills;

        public SpaceShip activeShip => _ship;
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
            //Debug.Log("OnshipDeathMethod");
            _livesQuantity--;
            //Debug.Log($"_livesQuantity {_livesQuantity}");
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
            _ship.eventOnDeath.AddListener(OnShipDeath);
        }

        public void AddKill()
        {
            _numKills++;
        }
        public void AddScore(int num)
        {
            _score += num;
        }
    }
}
