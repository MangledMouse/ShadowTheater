using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;
    public AudioSource persistentSource;
    // Start is called before the first frame update
    void Start()
    {
        persistentSource = GameObject.FindGameObjectWithTag("PersistentAudioManager").GetComponent<AudioSource>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying && !persistentSource.isPlaying)
            source.Play();
    }
}
