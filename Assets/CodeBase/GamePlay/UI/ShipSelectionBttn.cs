using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Space_lancer
{
    public class ShipSelectionBttn : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenuPanel;

        [SerializeField] private SpaceShip _prefabSpaceShip;
        [SerializeField] private TMP_Text _shipName;
        [SerializeField] private TMP_Text _hitPoints;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _agility;
        [SerializeField] private Image _preview;

        // Start is called before the first frame update
        void Start()
        {
            if (_prefabSpaceShip == null) return;

            _shipName.text = _prefabSpaceShip.nickName;
            _hitPoints.text = "HP: " + _prefabSpaceShip._hitPoints.ToString();
            _speed.text = "Speed: " + _prefabSpaceShip.maxLinearVelocity.ToString();
            _agility.text = "Agility: " + _prefabSpaceShip.maxAngularVelocity.ToString();
            _preview.sprite = _prefabSpaceShip.previewImage;
        }

        public void SelectShip()
        {
            Player.selectedSpaceShip = _prefabSpaceShip;
            _mainMenuPanel.ShowMainPanel();
        }
    }
}
