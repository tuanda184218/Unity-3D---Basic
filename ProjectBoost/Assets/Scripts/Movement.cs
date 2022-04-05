using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1600f;
    [SerializeField] float RotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    AudioSource audioSource;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }

    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop(); // done
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play(); // chay effect
        }
    }

    bool isAlive;

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-RotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play(); // chay effect
        }
    }

    void RotateLeft()
    {
        ApplyRotation(RotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play(); // chay effect
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
