using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
public class RoomControl : StageControl {
    //public static int roomType = 2; //1:Thuong, 2:VIP
    //  public GameObject prefabsTable;
    public Transform parentIconGame;

    public Transform tf_effect;

    //public Text displayName;
    //public Text lb_id;
    //public Text displayXu;
    //public Image imgAvata;
    //public RawImage rawAvata;

    //public Image game_name;
    //public Sprite[] gameNames;

    //public List<GameObject> listRoom = new List<GameObject>();
    [SerializeField]
    LoopVerticalScrollRect loopVerticalScrollRect;//toi uu scroll

    public Text text_noti;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < parentIconGame.childCount; i++) {
            GameObject obj = parentIconGame.GetChild(i).gameObject;
            parentIconGame.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate {
                onClickGame(obj);
            });
        }
    }
    const float posXDefault = 200.0f;
    public void setNoti(string str) {
        if (!str.Equals("")) {
            text_noti.text = str;
            
            float w = LayoutUtility.GetPreferredWidth(text_noti.rectTransform);
            text_noti.transform.localPosition = new Vector3(posXDefault, 0, 0);
            float posEnd = -posXDefault - w - 50;

            float time = (posXDefault - posEnd) / 50;
            text_noti.transform.DOKill();
            text_noti.transform.DOLocalMoveX(posEnd, time).SetLoops(-1).SetEase(Ease.Linear);
        }
    }

    void OnEnable() {
        gameControl.top.setGameName();
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
            gameControl.disableAllDialog();
            onBack();
        }
        //if (www != null) {
        //    if (www.isDone && !isOne) {
        //        rawAvata.texture = www.texture;
        //        isOne = true;
        //    }
        //}
    }
    public override void onBack() {
        GameControl.instance.sound.startClickButtonAudio();
        //  gameControl.setStage(gameControl.menu);
        gameControl.panelMessageSytem.onShow("Bạn có muốn thoát?", delegate {
            NetworkUtil.GI().close();
            gameControl.setStage(gameControl.login);
        });
    }

    //public Image game_name;
    //public void setGameName() {
    //    //string name = "CHỌN GAME";
    //    int nameIndex = -1;
    //    switch (gameControl.gameID) {
    //        case GameID.PHOM:
    //            // name = "PHỎM";
    //            nameIndex = 0;
    //            break;
    //        case GameID.TLMN:
    //            //  name = "TIẾN LÊN MN";
    //            nameIndex = 1;
    //            break;
    //        case GameID.XITO:
    //            // name = "XÌ TỐ";
    //            nameIndex = 2;
    //            break;
    //        case GameID.MAUBINH:
    //            // name = "MẬU BINH";
    //            nameIndex = 3;
    //            break;
    //        case GameID.BACAY:
    //            // name = "BA CÂY";
    //            nameIndex = 4;
    //            break;
    //        case GameID.LIENG:
    //            // name = "LIÊNG";
    //            nameIndex = 5;
    //            break;
    //        case GameID.XAM:
    //            // name = "SÂM";
    //            nameIndex = 6;
    //            break;
    //        case GameID.XOCDIA:
    //            // name = "XÓC ĐĨA";
    //            nameIndex = 7;
    //            break;
    //        case GameID.POKER:
    //            // name = "POKER";
    //            nameIndex = 8;
    //            break;
    //        case GameID.TAIXIU:
    //            //name = "TÀI XỈU";
    //            nameIndex = 9;
    //            break;
    //        case GameID.TLMNsolo:
    //            // name = "TIẾN LÊN MN Solo";
    //            nameIndex = 10;
    //            break;
    //        case GameID.XENG:
    //            // name = "XÈNG";
    //            nameIndex = 11;
    //            break;
    //    }
    //    game_name.sprite = gameControl.gameNames[nameIndex];
    //}
    public void createScollPane() {
        // List<TableItem> listTable = gameControl.listTable;
        gameControl.panelWaiting.onShow();
        //if (parent.childCount > 0) {
        //loopVerticalScrollRect.RefillCells();
        loopVerticalScrollRect.ClearCells();
        loopVerticalScrollRect.RefillCells();
        // }
        loopVerticalScrollRect.totalCount = gameControl.listTable.Count;
        //loopVerticalScrollRect.enabled = true;
        //for (int i = 0; i < listRoom.Count; i++) {
        //    Destroy(listRoom[i]);
        //}
        //listRoom.Clear();
        //for (int i = 0; i < listTable.Count; i++) {
        //    GameObject obj = Instantiate(prefabsTable) as GameObject;
        //    obj.transform.SetParent(parent);
        //    obj.transform.localScale = Vector3.one;
        //    obj.GetComponent<TableBehavior>().index = i;
        //    obj.GetComponent<TableBehavior>().ScrollCellIndex(listTable[i]);
        //    listRoom.Add(obj);
        //}
        gameControl.panelWaiting.onHide();
    }

    //WWW www;
    //bool isOne = false;
    //public void updateProfileUser() {
    //    string dis = BaseInfo.gI().mainInfo.displayname;
    //    if (dis.Length > 7) {
    //        dis = dis.Substring(0, 6) + "...";
    //    }
    //    displayName.text = dis;
    //    int idAvata = BaseInfo.gI().mainInfo.idAvata;
    //    string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
    //    int num_star = BaseInfo.gI().mainInfo.level_vip;

    //    displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP;

    //    www = null;
    //    if (link_avata != "") {
    //        www = new WWW(link_avata);

    //        isOne = false;
    //        imgAvata.gameObject.SetActive(false);
    //        rawAvata.gameObject.SetActive(true);
    //    } else if (idAvata > 0) {
    //        imgAvata.gameObject.SetActive(true);
    //        rawAvata.gameObject.SetActive(false);
    //        imgAvata.sprite = Res.getAvataByID(idAvata);//Res.list_avata[idAvata + 1];
    //    }
    //}

    //public void clickAvatar() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelInfoPlayer.infoMe();
    //    gameControl.panelInfoPlayer.onShow();
    //}

    public void clickButtonLamMoi() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelWaiting.onShow();
        SendData.onUpdateRoom();
    }

    public void sortBanCuoc() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().sort_giam_dan_bancuoc = !BaseInfo.gI().sort_giam_dan_bancuoc;
        BaseInfo.gI().type_sort = 1;
        SendData.onUpdateRoom();
    }

    public void sortMucCuoc() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().sort_giam_dan_muccuoc = !BaseInfo.gI().sort_giam_dan_muccuoc;
        BaseInfo.gI().type_sort = 2;
        SendData.onUpdateRoom();
    }

    public void sortTrangThai() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().sort_giam_dan_nguoichoi = !BaseInfo.gI().sort_giam_dan_nguoichoi;
        BaseInfo.gI().type_sort = 3;
        SendData.onUpdateRoom();
    }

    public void clickButtonChoiNgay() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelWaiting.onShow();
        SendData.onAutoJoinTable();
    }
    public void clickAnBanFull(bool isChecked) {
        gameControl.panelWaiting.onShow();
        BaseInfo.gI().isHideTabeFull = isChecked;
        SendData.onUpdateRoom();
    }
    [SerializeField]
    Button btn_vip, btn_free;
    [SerializeField]
    Sprite[] sp_btn;
    public void clickRoomVip() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().typetableLogin = Res.ROOMVIP;
        SendData.onJoinRoom(Res.ROOMVIP);
        gameControl.panelWaiting.onShow();
        setStateButton();
    }

    public void clickRoopFree() {
        GameControl.instance.sound.startClickButtonAudio();
        BaseInfo.gI().typetableLogin = Res.ROOMFREE;
        SendData.onJoinRoom(Res.ROOMFREE);
        gameControl.panelWaiting.onShow();
        setStateButton();
    }

    void setStateButton() {
        if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
            btn_vip.image.sprite = sp_btn[0];
            btn_free.image.sprite = sp_btn[1];
        } else {
            btn_vip.image.sprite = sp_btn[1];
            btn_free.image.sprite = sp_btn[0];
        }
    }
    //public void clickSetting() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelSetting.onShow();
    //}

    //public void clickHomThu() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelMail.onShow();
    //}

    //public void clickNapXu() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelNapChuyenXu.onShow();

    //}

    //public void clickDoiThuong() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelWaiting.onShow();
    //    SendData.onGetInfoGift();
    //    //}
    //}

    public void clickHelp() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.panleHelp.onShow();
        LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_HELP);
    }

    public void clickCreateRoom() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.panelCreateRoom.onShow();
        LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_CREATE_ROOM);
    }

    public void clickToiBan() {
        GameControl.instance.sound.startClickButtonAudio();
        //gameControl.panelToiBan.onShow();
        LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_GOTO_ROOM);
    }

    //public void clickNoti() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelNotiDoiThuong.onShow();
    //}
    public void clickPlayNow() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelWaiting.onShow();
        SendData.onAutoJoinTable();
    }

    //public void clickRanking() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    // gameControl.panelRanking.onShow();
    //}
    public GameObject objGame;
    public void onClickGame(GameObject game) {
        objGame = game;
        for (int i = 0; i < parentIconGame.childCount; i++) {
            parentIconGame.GetChild(i).transform.DOKill();
        }
        onClickGame(game.name);
        //tf_effect.localPosition = game.transform.localPosition;

        //vtPosCenter = mainTransform.InverseTransformPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //tf_effect.position = game.transform.position;
        tf_effect.transform.SetParent(game.transform);
        tf_effect.localPosition = Vector3.zero;
        game.transform.DOScale(1.05f, 0.6f).SetLoops(-1);
    }
    public void onClickGame(string obj) {
        gameControl.panelWaiting.onShow();
        GameControl.instance.sound.startClickButtonAudio();
        switch (obj) {
            case "Button_Phom":
                gameControl.gameID = GameID.PHOM;
                break;
            case "Button_TLMN":
                gameControl.gameID = GameID.TLMN;
                break;
            case "Button_XiTo":
                gameControl.gameID = GameID.XITO;
                break;
            case "Button_MB":
                gameControl.gameID = GameID.MAUBINH;
                break;
            case "Button_Poker":
                gameControl.gameID = GameID.POKER;
                break;
            case "Button_Sam":
                gameControl.gameID = GameID.XAM;
                break;
            case "Button_3Cay":
                gameControl.gameID = GameID.BACAY;
                break;
            case "Button_Lieng":
                gameControl.gameID = GameID.LIENG;
                break;
            case "Button_XocDia":
                gameControl.gameID = GameID.XOCDIA;
                break;
            case "Button_TaiXiu":
                gameControl.gameID = GameID.TAIXIU;
                gameControl.setCasino(GameID.TAIXIU, BaseInfo.gI().typetableLogin);
                SendData.onjoinTaiXiu((byte)BaseInfo.gI().typetableLogin);
                gameControl.top.setGameName();
                return;
            case "Button_TLMNSl":
                gameControl.gameID = GameID.TLMNsolo;
                break;
            case "Button_Xeng":
                gameControl.gameID = GameID.XENG;
                SendData.onJoinXengHoaQua();
                gameControl.top.setGameName();
                return;
        }

        gameControl.top.setGameName();
        SendData.onSendGameID((sbyte)gameControl.gameID);
    }
}
