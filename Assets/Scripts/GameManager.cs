using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] AudioSource hurvestSound;
    [SerializeField] AudioSource raidSound;
    [SerializeField] AudioSource newCharacterSound;
    [SerializeField] AudioSource eatingSound;

    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button peasantButton;
    [SerializeField] Button warriorButton;
    [SerializeField] Sprite basePauseSprite;
    [SerializeField] Sprite newPauseSprite;

    [SerializeField] TextMeshProUGUI recourcesCounterTMP;
    [SerializeField] TextMeshProUGUI enemiesAmountTMP;
    [SerializeField] TextMeshProUGUI resultsWinTMP;
    [SerializeField] TextMeshProUGUI resultsLoseTMP;

    [SerializeField] Image harvestTimerImg;
    [SerializeField] Image eatingTimerImg;

    [SerializeField] Image peasantTimerImg;
    [SerializeField] Image warriorTimerImg;
    [SerializeField] Image raidTimerImg;

    [SerializeField] int warriorAmount;
    [SerializeField] int peasantAmount;
    [SerializeField] int enemyAmount;
    [SerializeField] int wheatAmount;

    [SerializeField] int warriorCost;
    [SerializeField] int peasantCost;
    [SerializeField] int wheatToWarriors;
    [SerializeField] int wheatPerPeasant;

    [SerializeField] private float peasantCreateTime;
    [SerializeField] private float warriorCreateTime;
    [SerializeField] private float raidMaxTime;
    [SerializeField] private float produceWheatTimerMaxTime;
    [SerializeField] private float eatWheatTimerMaxTime;



    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;
    private float produceWheatTimer;
    private float eatWheatTimer;

    private string results;
    private int raidsAmount;

    #endregion


    Warrior warriors = new Warrior();
    Peasant peasants = new Peasant();
    Enemy enemies = new Enemy();
    Recources wheat = new Recources();

    void Start()
    {
        peasants.CharacterAmount = peasantAmount;
        enemies.CharacterAmount = enemyAmount;
        warriors.CharacterAmount = warriorAmount;
        wheat.Wheat = wheatAmount;

        peasants.WheatPerPeasant = wheatPerPeasant;
        warriors.WheatToWarriors = wheatToWarriors;

        warriors.CharacterCost = warriorCost;
        peasants.CharacterCost = peasantCost;

        raidTimer = raidMaxTime;
        produceWheatTimer = produceWheatTimerMaxTime;
        eatWheatTimer = eatWheatTimerMaxTime;

        UpdateText();

        if ((wheat.Wheat - warriors.CharacterCost) < 0)
            warriorButton.interactable = false;
        if ((wheat.Wheat - peasants.CharacterCost) < 0)
            peasantButton.interactable = false;
    }


    void Update()
    {
        if (gamePanel.activeInHierarchy)
        {
            Time.timeScale = 1;
            AttackTimer();
            FoodTimer();
            UpdateText();
            WariorTimer();
            PeasantTimer();
            ShowEnemiesInNextRaid();
            CheckHiringButtons();
            CheckLoose();
            CheckWin();
            results = GetResults();
        }
    }

    #region Methods
    public void HireWarriorButtonClick()
    {
        wheat.Wheat -= warriors.CharacterCost;
        warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
    }

    public void HirePeasantButtonClick()
    {
        wheat.Wheat -= peasants.CharacterCost;
        peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
    }

    public void PauseButtonClick()
    {
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void NewGameButtonClick()
    {
        Start();
    }

    public void UpdateText()
    {
        recourcesCounterTMP.text = $"{peasants.CharacterAmount}\n\n{warriors.CharacterAmount}\n\n{wheat.Wheat}";
    }

    public void WariorTimer()
    {

        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            warriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {

            warriorTimerImg.fillAmount = 1;
            warriorButton.interactable = true;
            warriors.CharacterAmount += 1;
            newCharacterSound.Play();
            warriorTimer = -2;
        }

    }

    public void PeasantTimer()
    {
        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            peasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            peasantTimerImg.fillAmount = 1;
            peasantButton.interactable = true;
            peasants.CharacterAmount += 1;
            newCharacterSound.Play();
            peasantTimer = -2;
        }
    }

    public void AttackTimer()
    {
        raidTimer -= Time.deltaTime;
        raidTimerImg.fillAmount = raidTimer / raidMaxTime;
        if (raidTimer <= 0)
        {
            raidsAmount += 1;
            warriors.CharacterAmount = enemies.Attack(warriors.CharacterAmount);
            raidSound.Play();
            raidTimer = raidMaxTime;
        }
    }

    public void FoodTimer()
    {
        produceWheatTimer -= Time.deltaTime;
        harvestTimerImg.fillAmount = produceWheatTimer / produceWheatTimerMaxTime;
        if (produceWheatTimer <= 0)
        {
            wheat.Wheat = peasants.ProduceWheat(wheat.Wheat);
            hurvestSound.Play();
            produceWheatTimer = produceWheatTimerMaxTime;
        }
        eatWheatTimer -= Time.deltaTime;
        eatingTimerImg.fillAmount = eatWheatTimer / eatWheatTimerMaxTime;
        if (eatWheatTimer <= 0)
        {
            wheat.Wheat = warriors.Eat(wheat.Wheat);
            eatingSound.Play();
            eatWheatTimer = eatWheatTimerMaxTime;
        }
    }

    public void CheckLoose()
    {
        if (warriors.CharacterAmount < 0)
        {
            Time.timeScale = 0;
            resultsLoseTMP.text = results;
            gamePanel.SetActive(false);
            losePanel.SetActive(true);
        }
    }

    public void CheckWin()
    {
        if (peasants.CharacterAmount > 100 && wheat.Wheat > 500)
        {
            Time.timeScale = 0;
            resultsWinTMP.text = results;
            gamePanel.SetActive(false);
            winPanel.SetActive(true);

        }
    }

    private string GetResults()
    {
        this.results = $"{raidsAmount}\n\n{warriors.CharacterAmount}\n\n{peasants.CharacterAmount}\n\n{wheat.Wheat}";
        return results;
    }

    private void ShowEnemiesInNextRaid()
    {
        if (enemies.CharacterAmount > 0)
            enemiesAmountTMP.text = enemies.CharacterAmount.ToString();
    }

    private void CheckHiringButtons()
    {
        if ((wheat.Wheat - warriors.CharacterCost) > 0 && warriorTimer == -2)
            warriorButton.interactable = true;
        else
            warriorButton.interactable = false;
        if ((wheat.Wheat - peasants.CharacterCost) > 0 && peasantTimer == -2)
            peasantButton.interactable = true;
        else
            peasantButton.interactable = false;
    }

    #endregion
}
