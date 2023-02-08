using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource music;

    [SerializeField] AudioSource buttonSound;


    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (winPanel.activeInHierarchy)
        {
            music.Pause();
        }
        else if (losePanel.activeInHierarchy)
        {
            music.Pause();
        }
    }


    public void MusicButtonClick()
    {
        if (music.isPlaying)
            music.Pause();
        else
            music.Play();
    }

    public void PlayButtonClickSound()
    {
        buttonSound.Play();
    }
}
