using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleGame.Core
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox1;
        [SerializeField] private GameObject dialogueBox2;
        [SerializeField] private GameObject dialogueBox3;
        [SerializeField] private GameObject dialogueBox4;
        [SerializeField] private GameObject dialogueBox5;
        [SerializeField] private GameObject dialogueBox6;
        [SerializeField] private GameObject dialogueBox7;
        [SerializeField] private GameObject dialogueBox8;
        [SerializeField] private GameObject dialogueBox9;
        [SerializeField] private GameObject dialogueBox10;
        [SerializeField] private GameObject dialogueBox11;
        
        public void ButtonNext1()
        {
            dialogueBox1.SetActive(false);
            dialogueBox2.SetActive(true);
        }
        
        public void ButtonNext2()
        {
            dialogueBox2.SetActive(false);
            dialogueBox3.SetActive(true);
        }
        
        public void ButtonNext3()
        {
            dialogueBox3.SetActive(false);
            dialogueBox4.SetActive(true);
        }
        
        public void ButtonNext4()
        {
            dialogueBox4.SetActive(false);
            dialogueBox5.SetActive(true);
        }
        
        public void ButtonNext5()
        {
            dialogueBox5.SetActive(false);
            dialogueBox6.SetActive(true);
        }
        
        public void ButtonNext6()
        {
            dialogueBox6.SetActive(false);
            dialogueBox7.SetActive(true);
        }
        
        public void ButtonNext7()
        {
            dialogueBox7.SetActive(false);
            dialogueBox8.SetActive(true);
        }
        
        public void ButtonNext8()
        {
            dialogueBox8.SetActive(false);
            dialogueBox9.SetActive(true);
        }
        
        public void ButtonNext9()
        {
            dialogueBox9.SetActive(false);
            dialogueBox10.SetActive(true);
        }
        public void ButtonNext10()
        {
            dialogueBox10.SetActive(false);
            dialogueBox11.SetActive(true);
        }
        
        public void ButtonPlayInDialogue()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
