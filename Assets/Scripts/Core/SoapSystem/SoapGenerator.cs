using System;
using UnityEngine;
using BubbleGame.Core;

namespace BubbleGame.Core.SoapSystem
{
    
    
    public class SoapGenerator : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private RefreshTMP refreshTMP;
        public int generatePower = 1;

        private void Awake()
        {
            refreshTMP = FindFirstObjectByType<RefreshTMP>();
        }
        
        public void OnGenerate()
        {
            player.Croquette += generatePower;
            refreshTMP.RefreshTMPS();
        }
    }
}
