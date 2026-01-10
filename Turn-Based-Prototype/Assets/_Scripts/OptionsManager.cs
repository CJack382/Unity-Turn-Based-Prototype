using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class OptionsManager : MonoBehaviour
{
    private AudioManager audioManager;
    public bool muteAudio;

    public List<TMP_FontAsset> fontList;
    void Start()
    {
        audioManager = GameManager.Instance.AudioManager;
    }

    void Update()
    {
        
    }
}
