using UnityEngine;

public class PlayerManager : MonoBehaviour,IDamageable
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform playerTransform;
    #region Private Variables
    
    private int currentHealth = 100;
    private int maxHealth = 100;

    #endregion

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            playerTransform.rotation = Quaternion.Euler(0, -45, 0);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            playerTransform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            playerTransform.rotation = Quaternion.Euler(0, -135, 0);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            playerTransform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerTransform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerTransform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }


    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector3(horizontal * speed, playerRb.velocity.y, vertical * speed);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        //haelthbar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        //animaton
        //position reset
    }
}