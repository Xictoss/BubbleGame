using BubbleGame.Core.SFXManager;
using BubbleGame.Core.SoapSystem;
using DG.Tweening;
using UnityEngine;

namespace BubbleGame.Core
{
    public class UpgradeSystem : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeButton;
        [SerializeField] private GameObject levelUpButton;
        
        [Header("Upgrade Values")]
        
        [SerializeField] private float upgradePower = 1.5f;
        [SerializeField] private float levelCostMultiplier = 2.5f;
        [SerializeField] private float animalPriceMultiplier = 2f;
        public int upgradeLevelCost = 1000;
        public int upgradeCroquetteCost = 100;
        public int upgradeAutoClickerCost = 10;
        
        [Header("Scripts refs")]
        
        [SerializeField] private SpawnSystem spawnSystem;
        
        private SoapGenerator _soapGenerator;
        private Player _player;
        private RefreshTMP _refreshTmp;
        private SpawnSystem _spawnSystem;
        
        
        private void Awake()
        {
            _soapGenerator = FindFirstObjectByType<SoapGenerator>();
            _player = FindFirstObjectByType<Player>();
            _refreshTmp = FindFirstObjectByType<RefreshTMP>();
            _spawnSystem = FindFirstObjectByType<SpawnSystem>();
        }

        public void OnTryToUpgradeCroquette()
        {
            if (upgradeCroquetteCost <= _player.Soap)
            {
                _player.Soap -= upgradeCroquetteCost;
                _soapGenerator.generatePower = Mathf.CeilToInt(_soapGenerator.generatePower * upgradePower);
                upgradeCroquetteCost = Mathf.CeilToInt(upgradeCroquetteCost * 1.6f);
                _refreshTmp.RefreshTMPS();
                SoundManager.Instance.PlaySound2D("UpgradeCrunch");
            }
            else
            {
                upgradeButton.transform.DOShakePosition(1, Vector3.left, 100);
            }
        }

        public void OnTryToUpgradeLevel()
        {
            if (_player.Soap >= upgradeLevelCost)
            {
                _player.Soap -= upgradeLevelCost;
                upgradeLevelCost = Mathf.CeilToInt(upgradeLevelCost * levelCostMultiplier);
                spawnSystem.index += 1;
                _spawnSystem.croquetteCost = Mathf.CeilToInt(_spawnSystem.croquetteCost * animalPriceMultiplier);
                _refreshTmp.RefreshTMPS();
                SoundManager.Instance.PlaySound2D("UpgradeSoap");
            }
            else
            {
                levelUpButton.transform.DOShakePosition(1, Vector3.left, 100);
                //TODO JOUER SON DE FAIL D'ACHAT.
            }
        }

        public void OnTryToUpgradeAutoClicker()
        {
            if (_player.Soap >= upgradeAutoClickerCost)
            {
                _player.Soap -= upgradeAutoClickerCost;
                upgradeAutoClickerCost = Mathf.CeilToInt(upgradeAutoClickerCost * 1.6f);
            }
        }
    }
}