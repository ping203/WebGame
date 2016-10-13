using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuControl : StageControl {
    //public Text lb_textnoti;
    public Text lb_name;
    //public Text lb_id;
    //public Text lb_chip;
    public Text lb_xu;
    // public Text lb_num_mail;
    //public GameObject buttonDoiThuong;
    public Image imgAvata;
    public RawImage rawAvata;

    WWW www;
    bool isOne = false;
    // Use this for initialization
    void Start() {
        //updateAvataName ();
    }
    void OnEnable() {
        updateAvataName();
    }
    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
            gameControl.disableAllDialog();
            onBack();
        }

        //lb_chip.text = (BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyChip) + Res.MONEY_FREE + " Free");
        //lb_xu.text = (BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP + " Vip");
        //if (BaseInfo.gI().soTinNhan > 0) {
        //    lb_num_mail.text = BaseInfo.gI().soTinNhan + "";
        //} else {
        //    lb_num_mail.text = BaseInfo.gI().soTinNhan + "";
        //    if (lb_num_mail.transform.parent.gameObject.activeInHierarchy) {
        //        lb_num_mail.transform.parent.gameObject.SetActive(false);
        //    }
        //}

        if (www != null) {
            if (www.isDone && !isOne) {
                rawAvata.texture = www.texture;
                isOne = true;
            }
        }
    }

    void deActive() {
        gameObject.SetActive(false);
    }

    public void updateAvataName() {
        lb_name.text = (BaseInfo.gI().mainInfo.displayname);
        //lb_id.text = "ID:" + BaseInfo.gI().mainInfo.userid;
        int idAvata = BaseInfo.gI().mainInfo.idAvata;
        string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
        int num_star = BaseInfo.gI().mainInfo.level_vip;

        lb_xu.text = "" + BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyVip) + Res.MONEY_VIP;

        www = null;
        if (link_avata != "") {
            www = new WWW(link_avata);
            isOne = false;
            imgAvata.gameObject.SetActive(false);
            rawAvata.gameObject.SetActive(true);
        } else if (idAvata > 0) {
            imgAvata.gameObject.SetActive(true);
            rawAvata.gameObject.SetActive(false);
            // spriteAvata.spriteName = idAvata + "";
            //imgAvata.sprite = Res.getAvataByID(idAvata);//Res.list_avata[idAvata + 1];
            LoadAssetBundle.LoadSprite(imgAvata, Res.AS_AVATA, "" + idAvata);
        }
    }

    public override void onBack() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelMessageSytem.onShow("Bạn có muốn thoát?", delegate {
            NetworkUtil.GI().close();
            gameControl.setStage(gameControl.login);
        });
    }

    public void onClickGame(string obj) {
        gameControl.panelWaiting.onShow();
        GameControl.instance.sound.startClickButtonAudio();
        switch (obj) {
            case "tlmn":
                gameControl.gameID = GameID.TLMN;
                break;
            case "tlmnsl":
                gameControl.gameID = GameID.TLMNsolo;
                break;
            case "phom":
                gameControl.gameID = GameID.PHOM;
                break;
            case "xito":
                gameControl.gameID = GameID.XITO;
                break;
            case "poker":
                gameControl.gameID = GameID.POKER;
                break;
            case "bacay":
                gameControl.gameID = GameID.BACAY;
                break;
            case "lieng":
                gameControl.gameID = GameID.LIENG;
                break;
            case "maubinh":
                gameControl.gameID = GameID.MAUBINH;
                break;
            case "xam":
                gameControl.gameID = GameID.XAM;
                break;
            case "xocdia":
                gameControl.gameID = GameID.XOCDIA;
                break;
            case "xeng":
                gameControl.gameID = GameID.XENG;
                SendData.onJoinXengHoaQua();
                gameControl.top.setGameName();
                return;
            case "taixiu":
                gameControl.gameID = GameID.TAIXIU;
                gameControl.setCasino(GameID.TAIXIU, 0);
                SendData.onjoinTaiXiu((byte)BaseInfo.gI().typetableLogin);
                gameControl.top.setGameName();
                //gameControl.toast.showToast("GAME ĐANG PHÁT TRIỂN!");
                return;
        }

        gameControl.top.setGameName();
        SendData.onSendGameID((sbyte)gameControl.gameID);
    }

    public void clickSetting() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelSetting.onShow();

        //LoadAssetBundle.LoadScene("sub_setting", "sub_setting");
    }

    public void clickHelp() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panleHelp.onShow();
    }

    public void clickNapXu() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelNapChuyenXu.onShow();

    }

    public void clickDoiThuong() {
        GameControl.instance.sound.startClickButtonAudio();
        //		if (BaseInfo.gI()..Count > 0
        //		        && BaseInfo.gI().giftTheCao.Count > 0)
        //		{
        //		    gameControl.panelDoiThuong.onShow();
        //		    gameControl.panelWaiting.onHide();
        //		}
        //		else
        //		{
        //gameControl.panelDoiThuong.onShow();
        gameControl.panelWaiting.onShow();
        SendData.onGetInfoGift();
        //}
    }

    public void clickAvatar() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelInfoPlayer.infoMe();
        gameControl.panelInfoPlayer.onShow();
    }

    public void clickHomThu() {
        GameControl.instance.sound.startClickButtonAudio();
        SendData.onGetInboxMessage();
        gameControl.panelWaiting.onShow ();
        gameControl.panelMail.onShow();
    }

    public void clickDienDan() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.dialogNotification.onShow("Bạn có muốn chuyển đến diễn đàn?", delegate {
        //    Application.OpenURL(Res.linkForum);
        //});
    }

    public void clickLuatChoi() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.dialogLuatChoi.onShow();
    }

    public override void Appear() {
        base.Appear();
        if (BaseInfo.gI().isPurchase) {
            //   buttonDoiThuong.SetActive(false);
        } else {
            //   buttonDoiThuong.SetActive(true);
        }
    }

    public void clickNoti() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelNotiDoiThuong.onShow();
    }
}

