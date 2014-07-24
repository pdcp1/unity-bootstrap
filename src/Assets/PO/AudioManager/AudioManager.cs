using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public List<AudioClip> musicTraks;
	public List<AudioClip> sfx;
	
	// Use this for initialization
	List<AudioSource> sources = new List<AudioSource>();
	
	AudioSource masterSource;
	AudioSource musicSource;
	
	
	void Start () 
	{
		masterSource = newAudioSource();	
		musicSource = newAudioSource();
	}	
	
	
	public bool MusicMute
	{
		set { musicSource.mute = value; }
		get { return musicSource.mute; }		
	}
	
	
	public bool FXMute
	{
		set { masterSource.mute = value; }
		get { return masterSource.mute; }		
	}
	
	public float MusicVolume
	{
		set 
		{ musicSource.volume = value; }
		
		get 
		{ 
			return musicSource.volume; 
		}
	}
		
	public float FXVolume
	{
		set 
		{ masterSource.volume = value; }
		get 
		{ 
			return masterSource.volume; 
		}
	}
	
	
	public void PlayFX(int track)
	{
		Play(sfx[track]);
	}
	
	public void loop(AudioClip clip)
	{
		var source = getOrCreateOwner(clip);
		
		source.clip = clip;
		source.loop = true;
		source.Play();
	}
	
	public void PlayMusicIfNotPlaying(int track)
	{ 
		var clip = musicTraks[track];
		
		if(musicSource.clip != clip)
		{
			PlayMusic(track);
		}
	}
	public void PlayMusic(int track)
	{	
		var clip = musicTraks[track];

		musicSource.clip = clip;
		musicSource.loop = true;
		musicSource.Play();
	}
	
	
	public void Play(AudioClip clip)
	{ 
		masterSource.PlayOneShot(clip);
	}
	
	public void playIfNotPlaying(AudioClip clip)
	{
		var source = getOrCreateOwner(clip);
		
		if(!source.isPlaying)
		{
			source.Play();
			
			if(!sources.Contains(source))
			{
				sources.Add(source);	
			}
		}
	}
	
	
	AudioSource newAudioSource()
	{
		var audioSource = gameObject.AddComponent<AudioSource>();
		
		audioSource.dopplerLevel = 0;
		audioSource.bypassEffects = true;
		audioSource.playOnAwake = false;
		
		return audioSource;
	}
	
	public AudioSource getOwner(AudioClip clip)
	{
		foreach (AudioSource item in sources) 
		{
			if(item.clip == clip)
				return item;
		}
		
		return null;
	}
	
	public AudioSource getOrCreateOwner(AudioClip clip)
	{
		var source = getOwner(clip);
		
		if(source == null)
		{
			source = newAudioSource();
			source.clip = clip;
		}
		
		return source;
	}
}
