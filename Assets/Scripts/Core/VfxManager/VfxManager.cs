using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace BubbleGame.Core
{
    public class VfxManager : MonoBehaviour
    {
        public ParticleSystem vfxPrefab;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("Aucune caméra principale trouvée !");
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                //PlayVFXAtMousePosition();
                VFX(vfxPrefab, mousePosition, 1.5f);
            }
        }

        public void VFX(ParticleSystem vfxPrefab, Vector3 position, float delayAfterDestroyVfx)
        {
            ParticleSystem vfxToSpawn = Instantiate(vfxPrefab, position, Quaternion.identity);

            if (vfxToSpawn != null)
            {
                vfxToSpawn.Play();
                Destroy(vfxToSpawn.gameObject, delayAfterDestroyVfx);
                
            }
        }
        
        
    }
}