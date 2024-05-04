using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public float speedIncreaseAmount = 2f;
    public float duration = 5f;
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the character
        if (other.CompareTag("Player"))
        {
            // Get the CharacterController script from the player GameObject
            DummyPlayerControl characterController = other.GetComponent<DummyPlayerControl>();

            // Check if the CharacterController script is found
            if (characterController != null)
            {
                // Increase the speed of the character
                characterController.speed += speedIncreaseAmount;

                // Start the coroutine to revert speed back after the duration
                StartCoroutine(RevertSpeed(characterController));
                
                // Disable the power-up object (optional)
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("CharacterController script not found on the player GameObject.");
            }
            gameObject.SetActive(false);
        }
    }

    IEnumerator RevertSpeed(DummyPlayerControl characterController)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert the speed back to its original value
        characterController.speed -= speedIncreaseAmount;
    }
}
