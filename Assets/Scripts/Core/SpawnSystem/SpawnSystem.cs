using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleGame.Core
{
    public class SpawnSystem : MonoBehaviour
    {

        [SerializeField] private Player player;
        [Header("Spawn Settings")]
       // public int croquettes = 50; //remove pour prendre la valeur présente dans le script du joueur 
        //système de croquettes prcq jsuis une fraude j'ai pas compris qui fait quoi et où
       
        public int coutEnCroquettes = 10; 
        [Header("Assignations")]
        [SerializeField] private List<GameObject> animals; //les trucs à spawn
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button button;


        public void SpawnObject()
        {
            if (player.Croquette >= coutEnCroquettes)
            {
                player.Croquette -= coutEnCroquettes;//enlève les croquettes

                Instantiate(animals[0], spawnPoint.position, spawnPoint.rotation);
/* la poule c num 0 et ainsi de suite jusquà la chimere mais dcp jsp comment ça va s'organiser
 après quand yaura les bonus et tt bref */

            }

            else
            {
                button.transform.DOShakePosition(3, Vector3.right, 1000);
            }
        }

    }

}
