using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
public class GettingStartedTutorialHero : MonoBehaviour
{
    [Header("Hero Settings")]
    /// a key the Player has to press to make our Hero jump
    public KeyCode ActionKey = KeyCode.Space;
    /// the force to apply vertically to the Hero's rigidbody to make it jump up
    public float JumpForce = 8f;

    [Header("Feedbacks")]
    /// a MMF_Player to play when the Hero starts jumping
    public MMFeedbacks JumpFeedback;

    public MMFeedbacks LandingFeedback;

    private const float _lowVelocity = 0.1f;
    private Rigidbody _rigidbody;
    private float _velocityLastFrame;
    private bool _jumping = false;
    private void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        Physics.gravity = Vector3.down * 30;
    }
    private void Update()
    {
        if (Input.GetKeyDown(ActionKey) && !_jumping)
        {
            Jump();
            
            
        }
        if (_jumping && (_velocityLastFrame < 0) && (Mathf.Abs(_rigidbody.velocity.y) < _lowVelocity))
        {
   
            _jumping = false;
            LandingFeedback.PlayFeedbacks();
        }
        _velocityLastFrame = _rigidbody.velocity.y;
    }
    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        _jumping = true;
        
        JumpFeedback.PlayFeedbacks();
    }
}
