using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    public float healthAmount = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DummyPlayerControl characterController = other.GetComponent<DummyPlayerControl>();
            characterController.health += healthAmount;
            if (characterController.health > characterController.maxHealth)//max healthi var olarak tutuyorsan max healthi aşmayı engelliyor
            {
                characterController.health = characterController.maxHealth;
            }
            Debug.Log("health " + characterController.health);
            gameObject.SetActive(false);
        }
    }
}
