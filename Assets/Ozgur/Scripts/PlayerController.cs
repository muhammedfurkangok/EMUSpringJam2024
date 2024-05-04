using UnityEngine;

namespace Ozgur.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody rigidbody;

        [Header("Parameters")]
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            var moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) moveDirection -= Vector3.forward;
            if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;
            if (Input.GetKey(KeyCode.A)) moveDirection -= Vector3.right;

            rigidbody.velocity = moveDirection.normalized * speed;

            if (moveDirection != Vector3.zero)
            {
                var toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
