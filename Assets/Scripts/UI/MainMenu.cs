using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelectionPanel;
    [SerializeField] private GameObject _shipSelectionPanel;
    [SerializeField] private GameObject _mainPanel;

    public void Start()
    {
        _mainPanel.SetActive(true);
    }
    public void ShowMainPanel()
    {
        _mainPanel.SetActive(true);
        _levelSelectionPanel.SetActive(false);
        _shipSelectionPanel.SetActive(false);
    }
    public void ShowShipSelectionPanel()
    {
        _mainPanel.SetActive(false);
        _levelSelectionPanel.SetActive(false);
        _shipSelectionPanel.SetActive(true);
    }

    public void ShowLevelSelectionPanel()
    {
        _mainPanel.SetActive(false);
        _levelSelectionPanel.SetActive(true);
        _shipSelectionPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
