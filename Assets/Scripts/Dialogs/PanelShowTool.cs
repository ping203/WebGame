﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelShowTool : PanelGame {
    public Image img_new;
    public GameObject sp_new;

    public void OnEnable() {
        //sp_new.SetActive(PanelMail.instance.isShowNew());
    }
    public void onClick(string action) {
        switch (action) {
            case "doithuong":
                GameControl.instance.panelWaiting.onShow();
                SendData.onGetInfoGift();
                break;
            case "napthe":
                //GameControl.instance.panelNapChuyenXu.onShow();
                LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_ADD_COIN);
                break;
            case "mail":
                //GameControl.instance.panelMail.onShow();
                LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_MAIL, ()=>{
                    PanelMail.instance.load();
                });
                break;
            case "help":
                // GameControl.instance.panleHelp.onShow();
                LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_HELP);
                break;
        }
        onHide();
    }
}
