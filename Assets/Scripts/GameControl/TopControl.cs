using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopControl : PanelGame {
    public GameControl gameControl;
    public Text displayName;
    public Text lb_id;
    public Text displayXu;
    public Text displayFree;
    public Image imgAvata;
    public RawImage rawAvata;
    
    void OnEnable() {
        updateProfileUser();
    }

    // Update is called once per frame
    void Update() {
        //if (www != null) {
        //    if (www.isDone && !isOne) {
        //        rawAvata.texture = www.texture;
        //        isOne = true;
        //    }
        //}
        displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyVip) + Res.MONEY_VIP_UPPERCASE;
        displayFree.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyFree) + Res.MONEY_FREE_UPPERCASE;
    }
    public Image game_name;
    public void setGameName() {
        //string name = "CHỌN GAME";
        int nameIndex = -1;
        switch (gameControl.gameID) {
            case GameID.PHOM:
                // name = "PHỎM";
                nameIndex = 0;
                break;
            case GameID.TLMN:
                //  name = "TIẾN LÊN MN";
                nameIndex = 1;
                break;
            case GameID.XITO:
                // name = "XÌ TỐ";
                nameIndex = 2;
                break;
            case GameID.MAUBINH:
                // name = "MẬU BINH";
                nameIndex = 3;
                break;
            case GameID.BACAY:
                // name = "BA CÂY";
                nameIndex = 4;
                break;
            case GameID.LIENG:
                // name = "LIÊNG";
                nameIndex = 5;
                break;
            case GameID.XAM:
                // name = "SÂM";
                nameIndex = 6;
                break;
            case GameID.XOCDIA:
                // name = "XÓC ĐĨA";
                nameIndex = 7;
                break;
            case GameID.POKER:
                // name = "POKER";
                nameIndex = 8;
                break;
            case GameID.TAIXIU:
                //name = "TÀI XỈU";
                nameIndex = 9;
                break;
            case GameID.TLMNsolo:
                // name = "TIẾN LÊN MN Solo";
                nameIndex = 10;
                break;
            case GameID.XENG:
                // name = "XÈNG";
                nameIndex = 11;
                break;
        }
        game_name.sprite = gameControl.gameNames[nameIndex];
    }

    //WWW www;
    //bool isOne = false;
    public void updateProfileUser() {
        string dis = BaseInfo.gI().mainInfo.displayname;
        if (dis.Length > 15) {
            dis = dis.Substring(0, 15) + "...";
        }
        displayName.text = dis;
        int idAvata = BaseInfo.gI().mainInfo.idAvata;
        string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
        int num_star = BaseInfo.gI().mainInfo.level_vip;

        displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyVip) + Res.MONEY_VIP_UPPERCASE;
        lb_id.text = "ID: " + BaseInfo.gI().mainInfo.userid;
       // www = null;
        if (link_avata != "") {
            //www = new WWW(link_avata);
            //isOne = false;
            //imgAvata.gameObject.SetActive(false);
            //rawAvata.gameObject.SetActive(true);
            StartCoroutine(getAvata(link_avata));
        } else if (idAvata > 0) {
            imgAvata.gameObject.SetActive(true);
            rawAvata.gameObject.SetActive(false);
            // imgAvata.sprite = Res.getAvataByID(idAvata);//Res.list_avata[idAvata + 1];
            LoadAssetBundle.LoadSprite(imgAvata, Res.AS_AVATA, "" + idAvata);
        }
    }

    IEnumerator getAvata(string link) {
        WWW www = new WWW(link);
        yield return www;
        imgAvata.gameObject.SetActive(false);
        rawAvata.gameObject.SetActive(true);
        rawAvata.texture = www.texture;
        www.Dispose();
        www = null;
    }

    public void clickAvatar() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelInfoPlayer.infoMe();
        gameControl.panelInfoPlayer.onShow();
    }
    
    public void clickSetting() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.panelSetting.onShow();
        LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_SETTING);
    }

    public void clickHomThu() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelMail.onShow();
    }

    public void clickNapXu() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelNapChuyenXu.onShow();

    }

    public void clickDoiThuong() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelWaiting.onShow();
        SendData.onGetInfoGift();
        //}
    }

    public void clickHelp() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panleHelp.onShow();
    }

    public void clickCreateRoom() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelCreateRoom.onShow();
    }

    public void clickToiBan() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelToiBan.onShow();
    }

    public void clickNoti() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelNotiDoiThuong.onShow();
    }

    public void clickPlayNow() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelWaiting.onShow();
        SendData.onAutoJoinTable();
    }

    public void clickRanking() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.panelRank.onShow();

        LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_RANK);
    }

    public void clickInviteFacebook() {
        GameControl.instance.sound.startClickButtonAudio();
#if UNITY_WEBGL
        Application.ExternalCall("myFacebookInvite");
#endif
    }
}
