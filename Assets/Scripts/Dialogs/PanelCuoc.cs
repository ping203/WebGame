﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelCuoc : PanelGame {
    public Slider slider;
    public Text currentMoney;
    long tienmin = 0, tienmax, tienchon;

    void Awake() {
        slider.onValueChanged.AddListener(onValueChange);
    }
    public void onValueChange(float value) {
        tienchon = (int)(value * tienmax);
        if (tienchon < tienmin) {
            tienchon = tienmin;
        }
        currentMoney.text = BaseInfo.formatMoneyDetailDot(tienchon);
    }

    public void clickBtnOk() {
        GameControl.instance.sound.startClickButtonAudio();
        SendData.onSendCuocBC(tienchon);
        onHide();
    }

    public void onShow(long min, long max) {
        //long temp = 0;
        //if (RoomControl.roomType == 1) {
        //    temp = BaseInfo.gI().mainInfo.moneyChip;
        //}
        //else {
        long temp = BaseInfo.gI().mainInfo.moneyXu;
        //}
        tienmin = min;
        tienmax = max;
        if (temp < min) {
            min = temp;
            max = temp;
        }

        if (tienmax > temp) {
            tienmax = temp;
        }
        
        slider.value = 0;
        currentMoney.text = BaseInfo.formatMoneyDetailDot(tienmin);
        base.onShow();
    }
}
