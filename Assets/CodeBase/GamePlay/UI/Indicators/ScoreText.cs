using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Space_lancer
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _lastScoreText;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int score = Player.instance.score;

            if (_lastScoreText != score) 
            {
                _text.text = "Score: " + score.ToString();
                _lastScoreText = score;
            }
        }
    }
}
