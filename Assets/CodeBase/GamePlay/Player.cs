using UnityEngine;

namespace Space_lancer
{
    public class Player : SingletonBase<Player>
    {
        public static SpaceShip selectedSpaceShip;

        [SerializeField] private int _livesQuantity;
        private SpaceShip _ship;
        [SerializeField] private SpaceShip _playerShipPrefab;

        [SerializeField] private CameraController _cameraController;    
        [SerializeField] private MovementController _movementController;


        public SpaceShip shipPrefab
        {
            get
            {
                if (selectedSpaceShip == null)
                {
                    return _playerShipPrefab;
                }
                else
                {
                    return selectedSpaceShip;
                }
            }
        }
        public SpaceShip activeShip => _ship;

        private int _score;
        private int _numKills;

        public int score => _score;
        public int numKills => _numKills;
        public int numLives => _livesQuantity;
        // Start is called before the first frame update
        void Start()
        {
            Respawn();
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
            var newPlayerShip = Instantiate(shipPrefab);

            _ship = newPlayerShip.GetComponent<SpaceShip>();

            _cameraController.SetTarget(_ship.transform);
            _movementController.SetTargetShip(_ship);
            _ship.eventOnDeath.AddListener(OnShipDeath);
        }

        public void AddScore(int num)
        {
            _score += num;
        }

        public void AddKill()
        {
            _numKills++;
        }
    }
}
