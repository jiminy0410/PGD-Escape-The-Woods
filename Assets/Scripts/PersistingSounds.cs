using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistingSounds : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();

    bool backgroundSFXOn;

    private static PersistingSounds instance;
    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlaySound("background");
            PlaySound("ambient");
        }
    }

    public void Update()
    {
        if (!backgroundSFXOn && SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlaySound("background");
            PlaySound("ambient");

            backgroundSFXOn = true;
        }
    }

    public void PlaySound(string targetTrack)
    {
        switch(targetTrack)
        {
            case ("background"):
                audioSources[0].Play();
                break;

            case ("ambient"):
                audioSources[1].Play();
                break;

            case("chopTree"):
                audioSources[2].Play();
                break;

            case ("levelComplete"):
                audioSources[3].Play();
                break;
        }
    }
}
