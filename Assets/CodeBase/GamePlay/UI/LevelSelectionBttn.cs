using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Space_lancer
{
    public class LevelSelectionBttn : MonoBehaviour
    {
        [SerializeField] private LevelProperties _levelProps;
        [SerializeField] private TMP_Text _levelTitle;
        [SerializeField] private Image _previewImage;

        private void Start()
        {
            if (_levelProps == null)
            {
                return;
            }

            _previewImage.sprite = _levelProps.previewIMG;
            _levelTitle.text = _levelProps.title;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(_levelProps.sceneName);
        }
    }
}
