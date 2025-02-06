using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace BubbleGame.Core
{
    public class VfxManager : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem vfxPrefab;
        
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
                VFX(vfxPrefab, mousePosition, 1.5f);
                SoundManager.Instance.PlaySound2D("ClickBubble");
            }
        }

        public static void VFX(ParticleSystem vfx, Vector3 position, float delayAfterDestroyVfx)
        {
            ParticleSystem vfxToSpawn = Instantiate(vfx, position, Quaternion.identity);

            if (vfxToSpawn != null)
            {
                vfxToSpawn.Play();
                Destroy(vfxToSpawn.gameObject, delayAfterDestroyVfx);
                
            }
        }
        
        
    }
}