using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Space_lancer
{
    public class KillText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _lastNumKills;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int nk = Player.instance.numKills;

            if (_lastNumKills != nk) 
            {
                _text.text = "Kills: "+nk.ToString();
                _lastNumKills = nk;
            }
        }
    }
}
