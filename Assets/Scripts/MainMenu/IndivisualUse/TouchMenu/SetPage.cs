using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPage : MonoBehaviour
{
    [SerializeField] private GameObject _SettingPanel, _settingwhiteshadow, _CreditPanel, _creditWhiteShadow;
    private List<GameObject> whiteShadows = new List<GameObject>();
    private List<GameObject> panels = new List<GameObject>();

    private void Start() {
        whiteShadows.Add(_settingwhiteshadow);
        whiteShadows.Add(_creditWhiteShadow);
        panels.Add(_SettingPanel);
        panels.Add(_CreditPanel);
        SetPreference();
        OnSettingPanel();
    }

    #region SettingPanel

    [SerializeField] private Slider _MusicSlider, _SoundFxSlider;

    private void SetPreference() {
        _MusicSlider.value = User.I.GetMusicVolume();
        _SoundFxSlider.value = User.I.GetSoundEffectVolume();
    }

    public void OnSettingPanel() {
        ChangePanel(_SettingPanel, _settingwhiteshadow);
    }

    public void MusicVolumeChange() {
        User.I.UpdateMusicVolume(_MusicSlider.value);
        AudioManager.AudioManagerInstance.UpdateMusicVolume();
    }

    public void SoundFxVolumeChange() {
        User.I.UpdateSoundEffectVolume(_SoundFxSlider.value);
        AudioManager.AudioManagerInstance.UpdateSoundEffect();
        AudioManager.AudioManagerInstance.PlayAudio("happy-4-note");
    }

    #endregion
    public void OnCreditPanel() {
        ChangePanel(_CreditPanel, _creditWhiteShadow);
    }

    private void ChangePanel(GameObject panel, GameObject whiteShadow) {
        foreach(GameObject ws in whiteShadows) {
            ws.SetActive(true);
        }
        foreach (GameObject p in panels) {
            p.SetActive(false);
        }
        whiteShadow.SetActive(false);
        panel.SetActive(true);
        
    }
}
