using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelSetting : PanelGame {
    public Toggle nhacnen, autoready, nhanloimoichoi;

    void OnEnable() {
        nhanloimoichoi.isOn = BaseInfo.gI().isNhanLoiMoiChoi;
        nhacnen.isOn = BaseInfo.gI().isSound;
        autoready.isOn = BaseInfo.gI().isAutoReady;
       // base.OnEnable();
    }

    public void onChangeVL() {
        nhanloimoichoi.isOn = BaseInfo.gI().isNhanLoiMoiChoi;
    }

    public void clickNhacNen() {
        GameControl.instance.sound.startClickButtonAudio();
        PlayerPrefs.SetInt("sound", nhacnen.isOn ? 0 : 1);
        PlayerPrefs.Save();
        BaseInfo.gI().isSound = nhacnen.isOn;

    }
    public void clickRung() {

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

