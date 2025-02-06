using System.Collections;
using System.Linq;
using BubbleGame.Core.SFXManager;
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

        [SerializeField] 
        private ParticleSystem mergeParticles;
        [SerializeField]
        private ParticleSystem sellParticles;
        
        
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
            Vector3 pos = ats.transform.position;
            VfxManager.VFX(sellParticles, pos, 1.5f);
            SoundManager.Instance.PlaySound2D("Bass");
            ats.Die();
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
            aTransform.DOMove(center, 0.5f).OnComplete(() => a.Die());
            bTransform.DOMove(center, 0.5f).OnComplete(() =>
            {
                Vector3 positionToSpawnVFX = b.transform.position;
                b.Die();
                
                VfxManager.VFX(mergeParticles, positionToSpawnVFX, 1.5f);
                SoundManager.Instance.PlaySound2D("PopMerge");
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
        }
    }   
        
}    
