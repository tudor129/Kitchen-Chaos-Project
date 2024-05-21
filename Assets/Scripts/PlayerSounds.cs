using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    Player _player;
    float _footstepTimer;
    float _footstepTimerMax = .1f;
    

    void Awake()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        _footstepTimer -= Time.deltaTime;
        if (_footstepTimer < 0f)
        {
            _footstepTimer = _footstepTimerMax;

            if (_player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(_player.transform.position, volume);
            }
            
        }
    }
}
