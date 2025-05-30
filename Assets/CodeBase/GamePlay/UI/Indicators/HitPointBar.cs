using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Space_lancer
{
    public class HitPointBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private float _lastHitPoint;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float hp = (float)Player.instance.activeShip.currentHitPoints / (float)Player.instance.activeShip.maxHitPoints;
            Debug.Log($"(float)Player.instance.activeShip._hitPoints {(float)Player.instance.activeShip._hitPoints}");

            if (hp != _lastHitPoint) 
            {
                _image.fillAmount = hp;
                _lastHitPoint = hp;
            }
        }
    }
}
