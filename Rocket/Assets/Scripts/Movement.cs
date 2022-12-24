using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 500f;
    [SerializeField] float direction = 180f;
    Rigidbody rb;
    AudioSource Sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessBoost();
        ProcessRotation();
    }

    private void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * speed);
            if (!Sound.isPlaying)
            {
                Sound.Play();
            }
        }

        else
        {
            Sound.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(-direction);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
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
