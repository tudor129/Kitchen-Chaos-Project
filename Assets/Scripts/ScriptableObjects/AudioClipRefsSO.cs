using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
        public AudioClip[] _chop;
        public AudioClip[] _deliveryFailed;
        public AudioClip[] _deliverySuccess;
        public AudioClip[] _footstep;
        public AudioClip[] _objDrop;
        public AudioClip[] _objPickup;
        public AudioClip[] _stoveSizzle;
        public AudioClip[] _trash;
        public AudioClip[] _warning;
}
