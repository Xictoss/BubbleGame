using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace BubbleGame.Core.PlayerMecanics
{
    public class AnimalController : MonoBehaviour
    {
        private SpawnSystem spawnSystem;
        private Player player;
        private RefreshTMP refreshTMP;

        private float currentTime;
        [SerializeField]
        public float timeBeforeAutoMerge = 2;

        [SerializeField] private VisualEffect visualDieEffect;
        [SerializeField] private VisualEffect visualMergeEffect;
        private void Awake()
        {
            spawnSystem = GetComponent<SpawnSystem>();
            player = FindFirstObjectByType<Player>();
            refreshTMP = GetComponent<RefreshTMP>();
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeBeforeAutoMerge)
            {
                currentTime = 0;
                var groups = Animal.Animals
                    .GroupBy(animal => animal.index);

                IGrouping<int, Animal> highest = null;
                foreach (var group in groups)
                {
                    if (group.Count() >= 2)
                    {
                        if(highest == null || highest.Key < group.Key)
                            highest = group;
                    }
                }

                if (highest != null)
                {
                    var animalsMerge = highest.ToList();
                    int first = Random.Range(0, animalsMerge.Count);
                    Animal a = animalsMerge[first];
                    animalsMerge.RemoveAt(first);
                    
                    int second = Random.Range(0, animalsMerge.Count);
                    Animal b = animalsMerge[second];
                    animalsMerge.RemoveAt(second);

                    Merge(a, b);
                }
            }
        }

        public void Sell(Animal ats)
        {
            var croquetteToAdd = ats.croquetteValue;
            var soapToAdd = ats.soapValue;
            player.Croquette += croquetteToAdd;
            player.Soap += soapToAdd;
            refreshTMP.RefreshTMPS();
            Die(ats.gameObject);
        }

        public void Die(GameObject atd)
        {
            PlayVFXAtAnimalPosition(atd, visualDieEffect);
            Destroy(atd.gameObject);
        }

        private void PlayVFXAtAnimalPosition(GameObject atd, VisualEffect visualEffect)
        {
            Vector3 position = atd.transform.position;
            
            VisualEffect vfxToSpawn = Instantiate(visualEffect, position, Quaternion.identity);

            if (vfxToSpawn != null)
            {
                vfxToSpawn.Play();
                StartCoroutine(DestroyVFXAfterDelay(vfxToSpawn, 1.5f));
            }
        }

        IEnumerator DestroyVFXAfterDelay(VisualEffect vfxToSpawn, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(vfxToSpawn.gameObject);
            Debug.Log("VFX détruit après 1 seconde");
        }

        public void Merge(Animal a, Animal b)
        {
            if(a.index != b.index && a != b)
                return;
            
            int newIndex = a.index + 1;

            Vector3 aPosition = a.transform.position;
            Vector3 bPosition = b.transform.position;
            
            Transform aTransform = a.transform;
            Transform bTransform = b.transform;
            
            Vector3 center = (aPosition + bPosition) * 0.5f;
            aTransform.DOMove(center, 0.5f);
            bTransform.DOMove(center, 0.5f).OnComplete(() =>
            {
                Vector3 PositionToSpawnVFX = a.transform.position;
                Destroy(a.gameObject);
                Destroy(b.gameObject);
                PlayVFXAtMerge(PositionToSpawnVFX, visualMergeEffect );

            
                if (spawnSystem.IsIndexValid(newIndex))
                { 
                    spawnSystem.SpawnAnimal(newIndex, a.transform.position, a.transform.rotation);
                }
                else
                {
                    //TODO game end
                    Debug.Log("GG");
                    
                }
            });
            void PlayVFXAtMerge(Vector3 atd, VisualEffect visualEffect)
            {
                Vector3 position = atd;
            
                VisualEffect vfxToSpawn = Instantiate(visualEffect, position, Quaternion.identity);

                if (vfxToSpawn != null)
                {
                    vfxToSpawn.Play();
                    StartCoroutine(DestroyVFXAfterDelay(vfxToSpawn, 1.5f));
                }
            }
        }
    }   
        
}    
