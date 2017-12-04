using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip Bel;
    public AudioClip Bite;
    public AudioClip Death;
    public AudioClip Shot;
    public AudioClip Slash;

    AudioSource Bels;
    AudioSource Bites;
    AudioSource Deaths;
    AudioSource Shots;
    AudioSource Slashs;

    static MusicPlayer instance;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        Bels = gameObject.AddComponent<AudioSource>();
        Bels.clip = Bel;
        Bels.playOnAwake = false;
        Bites = gameObject.AddComponent<AudioSource>();
        Bites.clip = Bite;
        Bites.playOnAwake = false;
        Deaths = gameObject.AddComponent<AudioSource>();
        Deaths.clip = Death;
        Deaths.playOnAwake = false;
        Shots = gameObject.AddComponent<AudioSource>();
        Shots.clip = Shot;
        Shots.playOnAwake = false;
        Slashs = gameObject.AddComponent<AudioSource>();
        Slashs.clip = Slash;
        Slashs.playOnAwake = false;
    }

    public static void PlayBel()
    {
        instance.Bels.Play();
    }

    public static void PlayBite()
    {
        instance.Bites.Play();
    }

    public static void PlayDeath()
    {
        instance.Deaths.Play();
    }

    public static void PlayShot()
    {
        instance.Shots.Play();
    }

    public static void PlaySlash()
    {
        instance.Slashs.Play();
    }
}
