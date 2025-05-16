using UnityEngine;

namespace Space_lancer
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        // Start is called before the first frame update
        void Start()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void ShowPause()
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void HidePause()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void LoadMainMenu()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
            SceneController.instance.LoadMainMenu();
        }
    }
}
