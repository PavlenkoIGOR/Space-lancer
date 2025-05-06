using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
namespace Space_lancer
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructable
    {
        [Header("SpaceShip")]
        [SerializeField] private float _mass;

        /// <summary>
        /// a force that moves forward
        /// </summary>
        [SerializeField] float _thrust;

        /// <summary>
        /// rotation force
        /// </summary>
        [SerializeField] private float _mobility;

        /// <summary>
        /// max linear velocity
        /// </summary>
        [SerializeField] private float _maxLinearVelocity;

        /// <summary>
        /// max rotation force (rad/sec)
        /// </summary>
        [SerializeField] private float m_maxAngularVelocity;

        private Rigidbody2D _rb;

        private float _timer = 0;
        private float _durationOfSpeedIncrease = 2.0f;
        private bool _isSpeedIncreased = false;
        float originalThrust;
        float origMaxVelocity;

        #region Public API

        /// <summary>
        /// linear force control between -1.0 and 1.0
        /// </summary>
        public float thrustControl { get; set; }
        public float thrustControlSided { get; set; }
        public float torqueControl { get; set; }

        #endregion

        protected override void Start()
        {
            base.Start();

            _rb = GetComponent<Rigidbody2D>();
            _rb.mass = _mass;
            _rb.inertia = 1;

            InitOffensive();

             originalThrust = _thrust;
             origMaxVelocity = _maxLinearVelocity;
        }

        private void Update()
        {

        }
        void FixedUpdate() //because RB
        {
            UpdateRB();

            UpdateNRGRegen();
        }

        private void UpdateRB()
        {
            if (SceneManager.GetActiveScene().name == nameof(Scenes.UniqueLevel))
            {
                ////move side
                _rb.AddForce(transform.right * _thrust * thrustControlSided * Time.fixedDeltaTime, ForceMode2D.Force);
                _rb.AddForce(-_rb.velocity * (_thrust / _maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
            }
            else
            {
                //rotation
                _rb.AddTorque(torqueControl * _mobility * Time.fixedDeltaTime, ForceMode2D.Force);
                _rb.AddTorque(-_rb.angularVelocity * (_mobility / m_maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
            }
            //move
                _rb.AddForce(transform.up * _thrust * thrustControl * Time.fixedDeltaTime, ForceMode2D.Force);

                //This line adds a force against the ship's current velocity (- _rb.velocity), which helps slow it down.
                //The force is proportional to the current velocity and divided by _maxLinearVelocity to limit the amount of force that is applied to slow it down.
                //This allows to avoid excessive acceleration of the ship and to keep its speed within reasonable values.If the speed exceeds the maximum linear
                //velocity(_maxLinearVelocity), then this force will decrease the ship's speed.
                _rb.AddForce(-_rb.velocity * (_thrust / _maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] _turrets;
        public void ShipFire(TurretMode turretMode)
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                if (_turrets[i].turretMode == turretMode)
                {
                    _turrets[i].Fire();
                    
                }
            }
        }

        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private int _energyRegenPerSec;

        private float _primaryEnergy;
        private int _secondaryAmmo;

        public void AddEnergy(int nrg)
        {
            _primaryEnergy = Mathf.Clamp(_primaryEnergy + nrg, 0, _maxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            _secondaryAmmo = Mathf.Clamp(_secondaryAmmo + ammo, 0, _maxAmmo);
        }

        private void InitOffensive()
        {
            _primaryEnergy = _maxEnergy;
            _secondaryAmmo = _maxAmmo;
        }

        private void UpdateNRGRegen()
        {
            _primaryEnergy += (float)_energyRegenPerSec * Time.fixedDeltaTime;
            _primaryEnergy = Mathf.Clamp(_primaryEnergy, 0, _maxEnergy);
        }

        public bool DrawAmmo(int amount)
        {
            if (amount == 0) return true;

            if (_secondaryAmmo >= amount)
            {
                _secondaryAmmo -= amount;
                return true;
            }

            return false;
        }

        public bool DrawNRG(int amount)
        {
            if (amount == 0) return true;

            if (_primaryEnergy >= amount)
            {
                _primaryEnergy -= amount;
                return true;
            }

            return false;
        }

        /// <summary>
        /// метод, назначающий какое-то свойство турелям
        /// </summary>
        /// <param name="props"></param>
        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                _turrets[i].AssignLoadOut(props);
            }
        }



        public void SpeedUp(float speedAmount)
        {
            if (!_isSpeedIncreased)
            {
                StartCoroutine(SpeedUpCoroutine(speedAmount));
            }
        }

        private IEnumerator SpeedUpCoroutine(float speedAmount)
        {
            _isSpeedIncreased = true;

            _thrust *= speedAmount;
            _maxLinearVelocity += _thrust;

            yield return new WaitForSeconds(_durationOfSpeedIncrease);

            _thrust = originalThrust;
            _maxLinearVelocity = origMaxVelocity;

            _isSpeedIncreased = false;
        }
    }
}