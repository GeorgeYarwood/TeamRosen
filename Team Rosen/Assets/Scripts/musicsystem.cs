using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicsystem : MonoBehaviour
{ 
    static AudioSource musicPlayer;
    static AudioClip [] clips = new AudioClip[3];
    Object [] tracks;   
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = gameObject.AddComponent<AudioSource>();

        LoadTracks();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadTracks()
    {
        //find all audio clips in the projject

        tracks = Resources.LoadAll("music",typeof(AudioClip));
        FillArray(tracks);
    } 

    void FillArray(Object[] tracklist)
    {
       for(int i = 0 ; i <= clips.Length; i++)
       { 
          try
          {
              clips[i] = (AudioClip)tracklist[i];
          }
          catch
          {
        
    
          } 

       }

        Play();

    }

    void Play() 
    {
        musicPlayer.clip = clips[0];
    }
}
