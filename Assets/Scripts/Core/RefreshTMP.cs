using System;
using TMPro;
using UnityEngine;

namespace BubbleGame.Core
{
    public class RefreshTMP : MonoBehaviour
    {
        [Header("Scripts References:")]
        [SerializeField]
        private SpawnSystem spawnSystem;
        [SerializeField]
        private UpgradeSystem upgradeSystem;
        [SerializeField]
        private Player player;
        
        [Header("TMPs References :")] 
        
        [SerializeField]
        private TextMeshProUGUI TmpUpgradeLevelPrice;
        [SerializeField]
        private TextMeshProUGUI TmpUpgradeCroquettePrice;
        [SerializeField] 
        private TextMeshProUGUI TmpAnimalPrice;
        [SerializeField] 
        private TextMeshProUGUI TmpPlayerCroquetteAmount;
        [SerializeField]
        private TextMeshProUGUI TmpPlayerSoapAmount;

        private void Awake()
        {
            spawnSystem = FindFirstObjectByType<SpawnSystem>();
            upgradeSystem = FindFirstObjectByType<UpgradeSystem>();
            player = FindFirstObjectByType<Player>();
        }

        private void Start()
        {
            RefreshTMPS();
        }

        public void RefreshTMPS()
        {
            TmpPlayerCroquetteAmount.text = player.Croquette.ToString();
            TmpPlayerSoapAmount.text = player.Soap.ToString();
            TmpAnimalPrice.text = spawnSystem.croquetteCost.ToString();
            TmpUpgradeCroquettePrice.text = upgradeSystem.upgradeCroquetteCost.ToString();
            TmpUpgradeLevelPrice.text = upgradeSystem.upgradeLevelCost.ToString();
        }
    }
}
