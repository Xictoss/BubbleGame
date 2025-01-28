using System.Collections;
using UnityEngine;

namespace BubbleGame.Core
{
    public class Animal : MonoBehaviour
    {
        public float movementSpeed = 20f; // Vitesse de base
        public float rotationSpeed = 100f; // Vitesse de rotation de base

        private bool isWandering = false;
        private bool isRotatingLeft = false;
        private bool isRotatingRight = false;
        private bool isWalking = false;

        private Rigidbody rb;

        [HideInInspector]
        public int index;
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (!isWandering)
            {
                StartCoroutine(Wander());
            }
        }

        void FixedUpdate()
        {
            if (isWalking)
            {
                // Ajoute une légère déviation pendant que l'objet avance
                float randomDeviation = Random.Range(-0.5f, 0.5f); // Déviation aléatoire
                Quaternion deviation = Quaternion.Euler(0, randomDeviation, 0);
                rb.MoveRotation(rb.rotation * deviation);

                // Déplacement
                rb.AddForce(transform.forward * movementSpeed, ForceMode.Force);
            }

            if (isRotatingRight)
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime, 0));
            }

            if (isRotatingLeft)
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -rotationSpeed * Time.fixedDeltaTime, 0));
            }
        }

        IEnumerator Wander()
        {
            int rotationTime = Random.Range(1, 4); // Durée de rotation
            int rotateWait = Random.Range(1, 3); // Attente avant de tourner
            int rotateDirection = Random.Range(1, 3); // 1 pour gauche, 2 pour droite
            int walkWait = Random.Range(1, 3); // Attente avant de marcher
            int walkTime = Random.Range(1, 5); // Durée de marche

            float randomSpeedFactor = Random.Range(0.8f, 1.2f); // Facteur de vitesse aléatoire
            movementSpeed *= randomSpeedFactor; // Modifie temporairement la vitesse

            isWandering = true;

            // 1. Attente avant de marcher
            yield return new WaitForSeconds(walkWait);
            isWalking = true;

            // 2. Marche avec durée aléatoire
            yield return new WaitForSeconds(walkTime);
            isWalking = false;

            // 3. Attente avant de tourner
            yield return new WaitForSeconds(rotateWait);

            // 4. Tourner dans une direction aléatoire
            if (rotateDirection == 1)
            {
                isRotatingLeft = true;
                yield return new WaitForSeconds(rotationTime);
                isRotatingLeft = false;
            }
            else if (rotateDirection == 2)
            {
                isRotatingRight = true;
                yield return new WaitForSeconds(rotationTime);
                isRotatingRight = false;
            }

            // 5. Rotation globale aléatoire après chaque cycle
            float randomAngle = Random.Range(0, 360); // Angle de rotation global
            Quaternion newDirection = Quaternion.Euler(0, randomAngle, 0);
            rb.MoveRotation(newDirection);

            // Réinitialise la vitesse après avoir appliqué le facteur aléatoire
            movementSpeed /= randomSpeedFactor;

            isWandering = false; // Réinitialise l'état
        }
    }
}
