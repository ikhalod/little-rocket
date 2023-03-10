using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 500f;
    [SerializeField] float direction = 180f;
    [SerializeField] AudioClip boost;
    [SerializeField] ParticleSystem boostEffect;

    Rigidbody rb;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessBoost();
        ProcessRotation();
    }

    private void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * speed);
            if (!sound.isPlaying)
            {
                sound.PlayOneShot(boost);
            }
            if (!boostEffect.isPlaying)
            {
                boostEffect.Play();
            }
        }

        else
        {
            boostEffect.Stop();
            sound.Stop();
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.down* Time.deltaTime * speed);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-direction);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(direction);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; // Unfreezing rotation so we can play normally
    }
}
