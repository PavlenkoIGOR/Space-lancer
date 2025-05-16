using TMPro;
using UnityEngine;

namespace Space_lancer
{
    public class LivesIndicator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _lastLives;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int lives = Player.instance.numLives;

            if (_lastLives != lives) 
            {
                _text.text = lives.ToString();
                _lastLives = lives;
            }
        }
    }
}
