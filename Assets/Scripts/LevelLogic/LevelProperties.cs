using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Space_lancer
{
    [CreateAssetMenu(fileName ="LevelProp", menuName ="LevelProp")]
    public class LevelProperties : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private string _sceneName;
        [SerializeField] private Sprite _previewIMG;
        [SerializeField] private LevelProperties _nextLVL;

        public string title => _title;
        public string sceneName => _sceneName;
        public LevelProperties nextLVL => _nextLVL;
        public Sprite previewIMG => _previewIMG;
    }
}