using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesController : Singleton<UpgradesController>
{
    protected UpgradesController() { }
    [SerializeField] private List<int> upgradesCosts;
    [SerializeField] private List<float> frequencyUpgrades;
    [SerializeField] private List<int> slowDownUpgrades;
    [SerializeField] private List<int> pointsUpgrades;
    [SerializeField] private List<int> crystalFlowUpgrades;

    [SerializeField] private Text frequncyCostText;
    [SerializeField] private Text slowDownCostText;
    [SerializeField] private Text pointsCostText;
    [SerializeField] private Text crystalFlowCostText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text coinsTextUpgrades;

    [SerializeField] private Image frequncyProgress;
    [SerializeField] private Image slowDownProgress;
    [SerializeField] private Image pointsProgress;
    [SerializeField] private Image crystalFlowProgress;

    public float Frequency()
    {
        return frequencyUpgrades.ElementAt(PlayerPrefs.GetInt("Frequency"));
}
   
    private void Start () {
	    if (!PlayerPrefs.HasKey("Frequency"))
	    {
	        PlayerPrefs.SetInt("Frequency",0);
        }
	    if (!PlayerPrefs.HasKey("SlowDown"))
	    {
	        PlayerPrefs.SetInt("SlowDown", 0);
	    }
	    if (!PlayerPrefs.HasKey("2xPoints"))
	    {
	        PlayerPrefs.SetInt("2xPoints", 0);
	    }
	    if (!PlayerPrefs.HasKey("CrystalFlow"))
	    {
	        PlayerPrefs.SetInt("CrystalFlow", 0);
	    }
        UpdateUpgradesCost();
    }

    private void UpdateUpgradesCost()
    {
        String upgradeName = "Frequency";
        var upgradeText = frequncyCostText;
        UpdateUpgradeButton("Frequency", frequncyCostText, frequncyProgress);
        UpdateUpgradeButton("SlowDown", slowDownCostText, slowDownProgress);
        UpdateUpgradeButton("2xPoints", pointsCostText, pointsProgress);
        UpdateUpgradeButton("CrystalFlow", crystalFlowCostText, crystalFlowProgress);
       
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        coinsTextUpgrades.text = PlayerPrefs.GetInt("Coins").ToString();

        PlayerPrefs.SetFloat("FrequencyFactor", frequencyUpgrades.ElementAt(PlayerPrefs.GetInt("Frequency")));
        PlayerPrefs.SetFloat("2xPointsFactor", pointsUpgrades.ElementAt(PlayerPrefs.GetInt("2xPoints")));
        PlayerPrefs.SetFloat("SlowDownFactor", pointsUpgrades.ElementAt(PlayerPrefs.GetInt("SlowDown")));
        PlayerPrefs.SetFloat("CrystalFlowFactor", pointsUpgrades.ElementAt(PlayerPrefs.GetInt("CrystalFlow")));
    }

    private void UpdateUpgradeButton(string upgradeName, Text upgradeText, Image progressBar )
    {
        if (PlayerPrefs.GetInt(upgradeName) < upgradesCosts.Count)
        {
            upgradeText.text = upgradesCosts.ElementAt(PlayerPrefs.GetInt(upgradeName)).ToString();
        }
        else
        {
            upgradeText.text = "FULL";
        }
        float frequency = PlayerPrefs.GetInt(upgradeName);
        progressBar.fillAmount = frequency / 5;
    }


    public void UpgradeFrequency()
    {
        if (PlayerPrefs.GetInt("Frequency") < frequencyUpgrades.Count - 1)
        {
            int upgradeCost = upgradesCosts.ElementAt(PlayerPrefs.GetInt("Frequency"));
            if (PlayerPrefs.GetInt("Coins") >= upgradeCost)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - upgradeCost);
                PlayerPrefs.SetInt("Frequency", PlayerPrefs.GetInt("Frequency") + 1);
                UpdateUpgradesCost();
            }
            float frequency = PlayerPrefs.GetInt("Frequency");
            frequncyProgress.fillAmount = frequency / (frequencyUpgrades.Count - 1);
        }
    }

    public void UpgradeCrystalFlow()
    {
        Upgrade("CrystalFlow", crystalFlowUpgrades, crystalFlowProgress);
    }

    public void UpgradeSlowDown()
    {
        Upgrade("SlowDown", slowDownUpgrades, slowDownProgress);
    }

    public void Upgrade2xPoints()
    {
        Upgrade("2xPoints", pointsUpgrades, pointsProgress);
    }

    private void Upgrade(string upgradeName, List<int> upgrades, Image progressBar)
    {
        if (PlayerPrefs.GetInt(upgradeName) < upgrades.Count - 1)
        {
            int upgradeCost = upgradesCosts.ElementAt(PlayerPrefs.GetInt(upgradeName));
            if (PlayerPrefs.GetInt("Coins") >= upgradeCost)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - upgradeCost);
                PlayerPrefs.SetInt(upgradeName, PlayerPrefs.GetInt(upgradeName) + 1);
                UpdateUpgradesCost();
            }
            float frequency = PlayerPrefs.GetInt(upgradeName);
            progressBar.fillAmount = frequency / (upgrades.Count - 1);
        }
    }
}
