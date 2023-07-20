using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    [SerializeField] float LDelay = 4f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    [SerializeField] GameObject[] RocketParts;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("start");
                break;
            case "Obstacle":
                Crash();
                break;
            case "Finish":
                Success();
                break;
        }
    }

    public void Crash()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LDelay);
        crashParticles.Play();

        foreach (var part in RocketParts)
        {
            part.transform.parent = null;
            //part.GetComponent<Rigidbody>().isKinematic = false;
            //part.GetComponent<Rigidbody>().AddExplosionForce(12, transform.position, 12);
        }
    }

    public void Success()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", LDelay);
        successParticles.Play();
    }

    public void ReloadLevel()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }

    public void NextLevel()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        int nextsceneindex = currentscene + 1;
        if (nextsceneindex == SceneManager.sceneCountInBuildSettings)
        {
            nextsceneindex = 0;
        }
        SceneManager.LoadScene(nextsceneindex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
