using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PanelTheCao : PanelGame {
    public InputField ip_masothe, ip_serithe;
    //public Text lb_menh_gia_the;
    int typeCard = -1;

    void Start () {
        infoTygia ();
        clickToggle(2);
    }

   public void clickToggle(int type) {
        typeCard = type;
    }

    public void clickNapTheCao () {
        GameControl.instance.sound.startClickButtonAudio ();
        //	OnSubmit ();
        
        //switch(tenMang) {
        //    case "Mobiphone":
        //        typeCard = 0;
        //        break;
        //    case "Vinaphone":
        //        typeCard = 1;
        //        break;
        //    case "Viettel":
        //        typeCard = 2;
        //        break;
        //}
        if(ip_masothe.text == null
            || ip_masothe.text.Trim ().Equals ("")
            || ip_masothe.text.Length > 15) {
            GameControl.instance.panelMessageSytem
                    .onShow ("Mã số thẻ không hợp lệ!");
            return;
        }

        if(/*typeCard != 4 &&*/ (ip_serithe.text.Trim ().Equals (""))) {
            GameControl.instance.panelMessageSytem
                    .onShow ("Bạn hãy nhập vào số Serial");
            return;
        }
        doRequestChargeMoneySimCard (BaseInfo.gI ().mainInfo.nick, typeCard, ip_masothe.text, ip_serithe.text);
        GameControl.instance.panelMessageSytem
                .onShow ("Hệ thống đang xử lý!");
    }

    public void TuChoi () {
        GameControl.instance.sound.startClickButtonAudio ();
        ip_masothe.text = "";
        ip_serithe.text = "";
    }

    private void doRequestChargeMoneySimCard (string userName, int type,
            string cardCode, string series) {
        Message m;
        m = new Message (CMDClient.PAYCARD);
        try {
            m.writer ().WriteUTF (userName);
            m.writer ().WriteShort ((short) type);
            m.writer ().WriteUTF (cardCode);
            m.writer ().WriteUTF (series);
            NetworkUtil.GI ().sendMessage (m);
        } catch(Exception e) {
            Debug.LogException (e);
        }
    }

    public void infoTygia () {
        //lb_menh_gia_the.text = "";
        //for(int i = 0; i < BaseInfo.gI ().list_tygia.Count; i++) {
        //    TyGia tg = (TyGia) BaseInfo.gI ().list_tygia[i];
        //    lb_menh_gia_the.text += BaseInfo.formatMoneyDetailDot (tg.menhgia) + " vnđ = " + BaseInfo.formatMoneyDetailDot (tg.xu) + " " + Res.MONEY_VIP + "\n\n";
        //}
    }
}
