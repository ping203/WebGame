using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelSetting : PanelGame {
    public Toggle nhacnen, autoready, nhanloimoichoi;
    void OnEnable() {
        nhanloimoichoi.isOn = BaseInfo.gI().isNhanLoiMoiChoi;
        nhacnen.isOn = BaseInfo.gI().isSound;
        autoready.isOn = BaseInfo.gI().isAutoReady;

    }

    public void onChangeVL() {
        nhanloimoichoi.isOn = BaseInfo.gI().isNhanLoiMoiChoi;
    }

    public void clickNhacNen() {
        GameControl.instance.sound.startClickButtonAudio();
        PlayerPrefs.SetInt("sound", nhacnen.isOn ? 0 : 1);
        PlayerPrefs.Save();
        BaseInfo.gI().isSound = nhacnen.isOn;
        //if (BaseInfo.gI().isSound) {
        //    //SoundManager.Get().startAudio(SoundManager.AUDIO_TYPE.BKG_MUSIC);
        //    GameControl.instance.sound.pauseSound();
        //} else {
        //    //SoundManager.Get().pauseAudio(SoundManager.AUDIO_TYPE.BKG_MUSIC);
        //    GameControl.instance.sound.pla();
        //}
    }
    public void clickRung() {
        //PlayerPrefs.SetInt ("rung", rung.isOn ? 0 : 1);
        //PlayerPrefs.Save ();

        //BaseInfo.gI ().isVibrate = rung.isOn;
        //GameControl.instance.sound.startClickButtonAudio ();
    }

    public void clickNhanLoiMoiChoi() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().isNhanLoiMoiChoi = nhanloimoichoi.isOn;
    }
    public void clickAutoReady() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().isAutoReady = autoready.isOn;
    }
}
