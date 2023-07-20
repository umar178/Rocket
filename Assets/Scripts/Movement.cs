using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip AC;

    [SerializeField] ParticleSystem mainEngineParticles;

    Rigidbody rb;
    AudioSource AS;

    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessUp();
        ProcessRotation();
    }

    void ProcessUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!AS.isPlaying)
        {
            AS.PlayOneShot(AC);
        }
        mainEngineParticles.Play();
        }

        else
        {
            AS.Stop();
            mainEngineParticles.Stop();
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(-Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            applyrotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            applyrotation(-rotationThrust);
        }
    }

    void applyrotation(float rotationthisframe)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationthisframe * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
