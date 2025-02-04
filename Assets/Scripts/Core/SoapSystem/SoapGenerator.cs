using System.Collections;
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

        private void Start()
        {
            //StartCoroutine(OnAutoGenerateRoutine());
            InvokeRepeating("OnAutoGenerate", 0f, 1f);

        }

        public void OnGenerate()
        {
            player.Croquette += generatePower;
            refreshTMP.RefreshTMPS();
        }

        private void OnAutoGenerate()
        {
            player.Croquette += generatePower;
            refreshTMP.RefreshTMPS();
        }

        IEnumerator OnAutoGenerateRoutine()
        {
            while (true)
            {
                OnAutoGenerate();
                yield return new WaitForSeconds(1);
            }
        }
    }
}
