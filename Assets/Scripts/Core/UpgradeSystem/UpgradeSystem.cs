using BubbleGame.Core.SoapSystem;
using DG.Tweening;
using UnityEngine;

namespace BubbleGame.Core
{
    public class UpgradeSystem : MonoBehaviour
    {
        [SerializeField] private int upgradeCost = 100;
        [SerializeField] private float upgradePower = 1.3f;
        [SerializeField] private GameObject upgradeButton;
        private SoapGenerator _soapGenerator;
        private Player _player;
        
        
        private void Awake()
        {
            _soapGenerator = FindFirstObjectByType<SoapGenerator>();
            _player = FindFirstObjectByType<Player>();
        }

        public void OnTryToUpgrade()
        {
            if (upgradeCost <= _player.Soap)
            {
                _player.Soap -= upgradeCost;
                _soapGenerator.generatePower = Mathf.CeilToInt(_soapGenerator.generatePower * upgradePower);
                upgradeCost = Mathf.CeilToInt(upgradeCost * 1.6f);
            }
            else
            {
                upgradeButton.transform.DOShakePosition(1, Vector3.left, 100);
            }
        }
    }
}