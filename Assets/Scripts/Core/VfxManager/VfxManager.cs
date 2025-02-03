using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace BubbleGame.Core.VfxManager
{
    public class VfxManager : MonoBehaviour
    {
        public VisualEffect vfxPrefab;
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
                PlayVFXAtMousePosition();
            }
        }

        public void PlayVFXAtMousePosition()
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            
            VisualEffect vfxInstance = Instantiate(vfxPrefab, mousePosition, Quaternion.identity);
            if (vfxInstance != null)
            {
                vfxInstance.Play(); 
                StartCoroutine(DestroyVFXAfterDelay(vfxInstance, 1.5f));
            }
            else
            {
                Debug.LogError("Impossible d'instancier le VFX");
            }
        }

        IEnumerator DestroyVFXAfterDelay(VisualEffect vfx, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(vfx.gameObject);
        }

        public void VFX(VisualEffect vfx, Vector3 position, float delayAfterDestroyVfx)
        {
            VisualEffect vfxToSpawn = Instantiate(vfx, position, Quaternion.identity);

            if (vfx != null)
            {
                vfx.Play();
                Destroy(vfx.gameObject, delayAfterDestroyVfx);
                
            }
        }
    }
}