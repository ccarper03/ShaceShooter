using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Explosion is Null");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
            
        }
        _audioSource.Play();
        Destroy(gameObject, 3);
    }
}
