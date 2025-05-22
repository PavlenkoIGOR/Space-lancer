using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace Space_lancer
{
    public class LevelController : SingletonBase<LevelController>
    {
        public event UnityAction levelPassed;
        public event UnityAction levelLost;

        [SerializeField] private LevelProperties _levelProperties;
        [SerializeField] private LevelCondition[] _conditions;
        private bool _isLevelCompleted;
        public bool hasNextLVL => _levelProperties.nextLVL != null;

        private float _levelTime;       
        public float levelTime=>_levelTime;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1.0f;
            _levelTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isLevelCompleted) 
            {
                _levelTime += Time.deltaTime;
                CheckLvlConditions();
            }




            if (Player.instance.numLives == 0)
            {
                Lose();
            }
        }

        private void CheckLvlConditions()
        {
            if (_isLevelCompleted) return;

            int numCompleted = 0;

            for (int i = 0; i < _conditions.Length; i++)
            {
                if (_conditions[i].isCompleted)
                {
                    numCompleted++;
                }
            }

            if (numCompleted == _conditions.Length)
            {
                _isLevelCompleted = true;
                Pass();
            }
        }

        private void Lose()
        {
            levelLost?.Invoke();
            Time.timeScale = 0.0f;
        }

        private void Pass()
        {
            levelPassed?.Invoke();
            Time.timeScale = 0.0f;
        }

        public void LoadNextLVL()
        {
            if (hasNextLVL == true)
            {
                SceneManager.LoadScene(_levelProperties.nextLVL.sceneName);
            }
            else
            {
                SceneManager.LoadScene(nameof(Scenes.MainScene));
            }
        }

        public void RestartLVL()
        {
            SceneManager.LoadScene(_levelProperties.sceneName);
        }
    }
}