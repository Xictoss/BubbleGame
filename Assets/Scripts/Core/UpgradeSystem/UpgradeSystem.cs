using BubbleGame.Core.SoapSystem;
using DG.Tweening;
using UnityEngine;

namespace BubbleGame.Core
{
    public class UpgradeSystem : MonoBehaviour
    {
        [SerializeField] private int upgradeCroquetteCost = 100;
        [SerializeField] private int upgradeLevelCost = 1000;
        [SerializeField] private float upgradePower = 1.3f;
        [SerializeField] private float LevelCostMultiplier = 1.6f;
        [SerializeField] private GameObject upgradeButton;
        [Header("Scripts refs")]
        [SerializeField] private SpawnSystem spawnSystem;
        
        private SoapGenerator _soapGenerator;
        private Player _player;
        
        
        private void Awake()
        {
            _soapGenerator = FindFirstObjectByType<SoapGenerator>();
            _player = FindFirstObjectByType<Player>();
        }

        public void OnTryToUpgradeCroquette()
        {
            if (upgradeCroquetteCost <= _player.Soap)
            {
                _player.Soap -= upgradeCroquetteCost;
                _soapGenerator.generatePower = Mathf.CeilToInt(_soapGenerator.generatePower * upgradePower);
                upgradeCroquetteCost = Mathf.CeilToInt(upgradeCroquetteCost * 1.6f);
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
                upgradeLevelCost = Mathf.CeilToInt(upgradeLevelCost * LevelCostMultiplier);
                spawnSystem.index += 1;

            }
        }
    }
}