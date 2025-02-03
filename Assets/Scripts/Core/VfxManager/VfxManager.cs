using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace BubbleGame.Core.VfxManager
{
    public class VfxManager : MonoBehaviour
    {
        public VisualEffect vfxPrefab; // Assigner le prefab du VFX ici dans l'inspecteur
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("Aucune caméra principale trouvée !");
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Détection du clic gauche de la souris
            {
                Debug.Log("Clic détecté");
                PlayVFXAtMousePosition();
            }
        }

        void PlayVFXAtMousePosition()
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Debug.Log($"Position de la souris : {mousePosition}");
            
            VisualEffect vfxInstance = Instantiate(vfxPrefab, mousePosition, Quaternion.identity);
            if (vfxInstance != null)
            {
                vfxInstance.Play(); 
                Debug.Log("VFX instancié et joué");

                
                StartCoroutine(DestroyVFXAfterDelay(vfxInstance, 1.5f));
            }
            else
            {
                Debug.LogError("Impossible d'instancier le VFX");
            }
        }

        IEnumerator DestroyVFXAfterDelay(VisualEffect vfx, float delay)
        {
            // Attendre 1 seconde (ou autre valeur définie)
            yield return new WaitForSeconds(delay);

            // Détruire le GameObject du VFX après le délai
            Destroy(vfx.gameObject);
            Debug.Log("VFX détruit après 1 seconde");
        }
    }
}