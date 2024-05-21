using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] AudioClipRefsSO _audioClipRefsSO;


    float _volume = 1f;
    
    void Awake()
    {
        Instance = this;
        
        _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerInstance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerInstance_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }
    void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipRefsSO._trash, trashCounter.transform.position);
    }
    void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(_audioClipRefsSO._objDrop, baseCounter.transform.position);
    }
    void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO._objPickup, Player.Instance.transform.position);
    }
    void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipRefsSO._chop, cuttingCounter.transform.position);
    }
    void DeliveryManagerInstance_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        
        PlaySound(_audioClipRefsSO._deliveryFailed, deliveryCounter.transform.position);
    }
    void DeliveryManagerInstance_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRefsSO._deliverySuccess, deliveryCounter.transform.position);
    }

    void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * _volume);
    }
    void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipRefsSO._footstep, position, volume);
    }
    
    public void PlayCountdownSound()
    {
        PlaySound(_audioClipRefsSO._warning, Vector3.zero);
    }
    
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(_audioClipRefsSO._warning, position);
    }

    public void ChangeVolume()
    {
        _volume += 0.1f;
        if (_volume > 1f)
        {
            _volume = 0f;
        }
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, _volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return _volume;
    }
}
