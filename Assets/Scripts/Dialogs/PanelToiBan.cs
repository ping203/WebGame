using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class PanelToiBan : PanelGame {
    public InputField ip_soban;

    public void toiBan() {
        try {
            GameControl.instance.sound.startClickButtonAudio();
            string str = ip_soban.text;
            if (str == "") {
                GameControl.instance.panelMessageSytem.onShow("Bạn chưa nhập tên bàn.");
                return;
            }
            int tbid = int.Parse(str);
            SendData.onJoinTableForView(tbid, "");
            onHide();
        } catch (Exception e) {
            GameControl.instance.toast.showToast("Định dạng bàn không đúng!");
            //Debug.LogException(e);
        }
    }
}
