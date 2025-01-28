using System;
using UnityEngine;
using BubbleGame.Core;

namespace BubbleGame.Core.SoapSystem
{
    
    
    public class SoapGenerator : MonoBehaviour
    {
        [SerializeField] private Player player; 
        private int generatePower = 1;
        
        public void OnGenerate()
        {
            player.Croquette += generatePower;
        }
    }
}
