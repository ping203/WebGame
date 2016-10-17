﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemInfoLichSuTaiXiu : MonoBehaviour {
    public Image img_yellow;
    public Image img_bkg;
    public Sprite[] sp_color;
    [HideInInspector]
    public int isTai;
    [HideInInspector]
    public int id_phien;

    public void setInfo(int isTai, int phien) {
        this.isTai = isTai;
        this.id_phien = phien;
        img_bkg.sprite = isTai == 1 ? sp_color[0] : sp_color[1];
    }

    public void onClick() {
        Debug.Log("Id phien: " + id_phien);
        SendData.onXemLSTaiXiu(id_phien);
    }
}
