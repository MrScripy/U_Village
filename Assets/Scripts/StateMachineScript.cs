using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineScript : MonoBehaviour
{
    #region gameObjects   
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject mainMenuRulesPanel;

    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;



    #endregion

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        mainMenuPanel.SetActive(true);
        mainMenuRulesPanel.SetActive(false);
        gameCanvas.SetActive(false);
    }

    public void RulesButtonClick()
    {
        mainMenuPanel.SetActive(false);
        mainMenuRulesPanel.SetActive(true);
    }

    public void ComeBackToMainMenuButtonClick()
    {
        mainMenuCanvas.SetActive(true);
        mainMenuPanel.SetActive(true);
        mainMenuRulesPanel.SetActive(false);
        gameCanvas.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void StartGameButtonClick()
    {
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);        
    }

}
