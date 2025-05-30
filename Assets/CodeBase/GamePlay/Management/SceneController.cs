using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Space_lancer
{
    public enum Scenes
    {
        MainScene,
        MainMenu,
        UniqueLevel
    }

    public class SceneController : SingletonBase<SceneController>
    {
        public void LoadSinglePlayerScene()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            SceneManager.LoadSceneAsync(nameof(Scenes.MainScene));
        }

        public void LoadUniqueLevelScene()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadSceneAsync(nameof(Scenes.UniqueLevel));
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(nameof(Scenes.MainMenu));
        }
        //public void LoadSinglePlayerScene()
        //{
        //    foreach (var scene in scenes)
        //    {
        //        if (System.IO.Path.GetFileNameWithoutExtension(scene.path) == nameof(Scenes.MainScene))
        //        {
        //            // ������������� ��������� ����������
        //            Screen.orientation = ScreenOrientation.LandscapeLeft;
        //            SceneManager.LoadSceneAsync(nameof(Scenes.MainScene));
        //            break;
        //        }

        //        //����� ��������� ����������
        //    }
        //}

        //public void LoadUniqueLevelScene()
        //{
        //    foreach (var scene in scenes)
        //    {
        //        if (System.IO.Path.GetFileNameWithoutExtension(scene.path) == nameof(Scenes.UniqueLevel))
        //        {
        //            // ������������� ������� ����������
        //            Screen.orientation = ScreenOrientation.Portrait;
        //            SceneManager.LoadSceneAsync(nameof(Scenes.UniqueLevel));
        //            break;
        //        }

        //        //����� ������� ����������
        //    }
        //}

        public void ExitApp()
        {
            Application.Quit();
        }
    }
}
