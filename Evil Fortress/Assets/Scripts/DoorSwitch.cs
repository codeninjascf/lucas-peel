using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject enabledObject;
    public GameObject disabledObject;
    public AudioClip enabledSound;
    public AudioClip disabledSound;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        enabledObject.SetActive(false);
        disabledObject.SetActive(true);
        SwitchEnabled = false;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool SwitchEnabled
    {
        get;
        private set;
    }
    private void OnTriggerExit(Collider other)
    {
        enabledObject.SetActive(true);
        disabledObject.SetActive(false);
        _audioSource.clip = disabledSound;
        _audioSource.Play();
        SwitchEnabled = true;
    }
    private void OnTriggerStay(Collider other)
    {
        enabledObject.SetActive(true);
        disabledObject.SetActive(false);
        SwitchEnabled = true;
        _audioSource.clip = disabledSound;
        _audioSource.Play();
    }
}
