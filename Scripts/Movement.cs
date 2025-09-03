using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength;
    [SerializeField] float rotateStrength;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoost;
    [SerializeField] ParticleSystem rightBoost;
    [SerializeField] ParticleSystem leftBoost;

    Rigidbody rb;
    AudioSource audioSourse;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

            if (!audioSourse.isPlaying)
            {
                audioSourse.PlayOneShot(mainEngine);
            }
            if (!mainBoost.isPlaying)
            {
                mainBoost.Play();
            }
        }
        else
        {
            audioSourse.Stop();
            mainBoost.Stop();
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotateStrength);
            if (!rightBoost.isPlaying)
            {
                leftBoost.Stop();
                rightBoost.Play();
            }
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotateStrength);
            if (!leftBoost.isPlaying)
            {
                rightBoost.Stop();
                leftBoost.Play();
            }
        }
        else
        {
            rightBoost.Stop();
            leftBoost.Stop();
        }
    }

    void ApplyRotation(float rotationyhis)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationyhis * Vector3.forward * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void OnDisable()
    {
        thrust.Disable();
    }
}
