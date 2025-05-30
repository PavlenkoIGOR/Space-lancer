using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;

namespace Space_lancer
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _kills;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _result;
        [SerializeField] private TMP_Text _bttnNextText;

        private bool _levelPassed = false;

        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);

            LevelController.instance.levelLost += OnLvlLost;
            LevelController.instance.levelPassed += OnLvlPassed;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            LevelController.instance.levelLost -= OnLvlLost;
            LevelController.instance.levelLost -= OnLvlPassed;
        }

        private void OnLvlPassed()
        {
            gameObject.SetActive(true);
            FillLevelStatistics();

            _result.text = "Passed!";

            if (LevelController.instance.hasNextLVL)
            {
                _bttnNextText.text = "Next";
            }
            else
            {
                _bttnNextText.text = "Main menu";
            }
        }

        private void OnLvlLost()
        {
            gameObject.SetActive(true);
            FillLevelStatistics();

            _result.text = "Lose!";
            _bttnNextText.text = "Restart";
        }

        private void FillLevelStatistics()
        {
            _kills.text = "Kills: " + Player.instance.numKills.ToString();
            _score.text = "Scores: " + Player.instance.score.ToString();
            _time.text = "Time: " + LevelController.instance.levelTime.ToString("F0");
        }

        public void OnBttnNextAction()
        {
            gameObject.SetActive(false);

            if (_levelPassed)
            {
                LevelController.instance.LoadNextLVL();
            }
            else
            {
                LevelController.instance.RestartLVL();
            }
        }
    }
}
