using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerControl : MonoBehaviour
{
     public float speed = 5f; // Movement speed
     public float maxHealth = 100f;
     public float health = 15f;
     public int ammo = 10;
     public int maxAmmo = 30;

     private void Start()
     {
         Debug.Log("start ammo " + ammo);
         Debug.Log("start health " + health);
     }

     void Update()
    {
        // Input for movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // If movement vector has length (input is not zero)
        if (movement.magnitude >= 0.1f)
        {
            // Calculate rotation to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            
            // Move the character
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }
}
