using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

namespace Space_lancer
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            MobileAndKeyboard,
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip _ship;
        [SerializeField] private Joystick _joystick;

        [SerializeField] private ControlMode _controlMode;

        private void Start()
        {
            if (_controlMode == ControlMode.Keyboard)
            {
                _joystick.gameObject.SetActive(false);
                _mobileFirePrimary.gameObject.SetActive(false);
                _mobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                _joystick.gameObject.SetActive(true);
                _mobileFirePrimary.gameObject.SetActive(true);
                _mobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (_ship == null) return;

            if (_controlMode == ControlMode.Mobile)
            {
                ControlMobile();
            }
            if (_controlMode == ControlMode.Keyboard)
            {
                ControlKeyBoard();
            }
            if (_controlMode == ControlMode.MobileAndKeyboard)
            {
                ControlKeyboardAndMobile();
            }
        }

        private void ControlMobile()
        {
            if (SceneManager.GetActiveScene().name == nameof(Scenes.UniqueLevel))
            {
                var dir = _joystick.value;
                _ship.thrustControl = dir.y;
                _ship.thrustControlSided = dir.x;
            }
            else
            {
                var dir = _joystick.value;
                _ship.thrustControl = dir.y;
                _ship.torqueControl = -dir.x;

                if (_mobileFirePrimary.isHold)
                {
                    _ship.ShipFire(TurretMode.Primary);
                }
                if (_mobileFireSecondary.isHold)
                {
                    _ship.ShipFire(TurretMode.Secondary);
                }
            }
        }

        private void ControlKeyBoard()
        {
            if (SceneManager.GetActiveScene().name == nameof(Scenes.UniqueLevel))
            {
                float thrust = 0;

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    thrust = 1.0f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    thrust = -1.0f;
                }
                _ship.thrustControl = thrust;
            }
            else 
            {
                float thrust = 0;
                float torque = 0;

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    thrust = 1.0f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    thrust = -1.0f;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    torque = 1.0f;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    torque = -1.0f;
                }


                if (Input.GetKey(KeyCode.Space))
                {
                    _ship.ShipFire(TurretMode.Primary);
                }
                if (Input.GetKey(KeyCode.C))
                {
                    _ship.ShipFire(TurretMode.Secondary);
                }


                _ship.thrustControl = thrust;
                _ship.torqueControl = torque;

            }
        }
        private void ControlKeyboardAndMobile()
        {
            //rotate by joystick
            Vector3 direction = _joystick.value;
            var dot2 = Vector2.Dot(direction, _ship.transform.right);
            _ship.torqueControl = -dot2;

            //move by keyboard's arrows
            float thrust = 0;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                thrust = 1.0f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                thrust = -1.0f;
            }
            _ship.thrustControl = thrust;
        }

        public void SetTargetShip(SpaceShip spaceShip)
        {
            _ship = spaceShip;
        }

        [SerializeField] private PointerClickHold _mobileFirePrimary;
        [SerializeField] private PointerClickHold _mobileFireSecondary;
    }
}
