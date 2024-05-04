using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPowerup : MonoBehaviour
{
    public int ammoAddup = 40;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DummyPlayerControl characterController = other.GetComponent<DummyPlayerControl>();
            characterController.ammo += ammoAddup;
            if (characterController.ammo > characterController.maxAmmo)//ammoCap e göre ammo eklemesi yapmak lazım
            {
                characterController.ammo = characterController.maxAmmo;
            }
            Debug.Log("ammo " + characterController.ammo);
            gameObject.SetActive(false);
        }
    }
}
