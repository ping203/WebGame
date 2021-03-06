﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
    public static void sendSMS(string port, string content) {
        //#if UNITY_WP8
        //        UnityPluginForWindowPhone.Class1.sendSMS(port, content);
        //#else
        //        string str = content;
        //        if (content.Contains("#")) {
        //            str = content.Replace("#", "%23");
        //        }
        //        Application.OpenURL("sms:" + port + @"?body=" + str);

        //#endif
    }
    private static GameControl _instance;
    public static GameControl instance {
        get {
            if (_instance == null) {
                _instance = new GameControl();
                //_instance = GameObject.Find("bkgChung").GetComponent<GameControl>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public LoginControl login;
    //public MenuControl menu;
    public RoomControl room;
    public TopControl top;
    public FacebookControl facebookControl;

    public TLMN tlmn;
    public TLMNSOLO tlmnsl;
    public PHOM phom;
    public Poker poker5;
    //public Poker9 poker9;
    public Xam xam;
    public Xito xito;
    public Lieng lieng5;
    //public Lieng9 lieng9;
    public Bacay bacay;
    //public Bacay9 bacay9;
    public MauBinh maubinh;
    public XocDia xocdia;
    public Xeng xeng;
    public TaiXiu taixiu;

    [SerializeField]
    Transform parentUI;
    //[HideInInspector]
    public PanelWaiting panelWaiting;
    //[HideInInspector]
    public PanelMessageSytem panelMessageSytem;
    //[HideInInspector]
    public Toast toast;
    //public PanelHelp panleHelp;
    //public PanelNapChuyenXu panelNapChuyenXu;
    //public PanelDoiThuong panelDoiThuong;
    //public PanelMail panelMail;
    //public PanelCreateRoom panelCreateRoom;
    //public PanelToiBan panelToiBan;
    //public PanelMoiChoi panelMoiChoi;
    //public PanelChat panelChat;
    //public PanelDatCuoc panelDatCuoc;
    //public PanelRutTien panelRutTien;
    //public PanelInput panelInput;
    //public PanelCuoc panelCuoc;
    //public DialogKetQua dialogKetQua;
    //public DialogRutTien dialogRutTien;
    //public DialogThongTin dialogThongTin;
    //public DialogQuenMatKhau dialogQuenMK;
    //public DialogMoiBan dialogMoiBan;
    //public DialogAllMess dialogAllMess;
    //public PanelNotiDoiThuong panelNotiDoiThuong;
    //public DialogLuatChoi dialogLuatChoi;
    //public DialogEvent dialogEvent;
    //public DialogDoiMatKhau dialogDoiMatKhau;
    //public PanelChangeAvata panelChangeAvata;
    //public PanelChangePassword panelChangePassword;
    //public PanelChangeName panelChangeName;
    //public PanelUpVip panelUpVip;

    public SoundManager sound;

    public BaseCasino currentCasino;
    public StageControl currenStage;
    public StageControl backState;

    //list de nhan danh sach tu sver trả về. roi xu li rieng
    [HideInInspector]
    public List<ItemMail> listMail = new List<ItemMail>();
    [HideInInspector]
    public List<ItemMail> listEvent = new List<ItemMail>();
    [HideInInspector]
    public List<ItemRanking> list_top = new List<ItemRanking>();
    [HideInInspector]
    public List<InfoGift> list_gift = new List<InfoGift>();
    [HideInInspector]
    public List<ItemInvite> list_invite = new List<ItemInvite>();

    [HideInInspector]
    public List<RoomInfo> phongFree = new List<RoomInfo>();
    [HideInInspector]
    public List<RoomInfo> phongVip = new List<RoomInfo>();
    [HideInInspector]
    public List<TableItem> listTable = new List<TableItem>();

    //public GameObject[] gameObj_Actions_InGame;
    //public Sprite[] chips_InGame;
    /// <summary>
    /// 0-vip, 1- free
    /// </summary>
    //public Sprite[] icon_moneys;
    //public Sprite getChipByName(string name) {
    //    for (int i = 0; i < chips_InGame.Length; i++) {
    //        if (name.Equals(chips_InGame[i].name))
    //            return chips_InGame[i];
    //    }
    //    return null;
    //}

    //public Sprite[] list_typecards;
    public int gameID;
    public bool cancelAllInvite = false;
    /// <summary>
    /// Là các action trong game như: theo, bỏ lượt, úp bỏ...
    /// </summary>
    //public Sprite[] list_actions_ingame;
    public Sprite[] gameNames;
    public static string IMEI = "";
    void Awake() {
        if (_instance == null) {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        } else {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }

        //Res.list_avata = Resources.LoadAll<Sprite>("Avata");
        //for (int i = 0; i < Res.list_avata.Length; i++) {

        //}
        // Res.list_avata = LoadAssetBundle.LoadSprite()
        // Res.list_emotions = Resources.LoadAll<Sprite>("Emotions");
        Res.list_cards = Resources.LoadAll<Sprite>("Cards/cardall");
        IMEI = "357238040933272";//SystemInfo.deviceUniqueIdentifier;
        if (panelWaiting == null) {
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_WAITING, (obj) => {
                panelWaiting = obj.GetComponent<PanelWaiting>();
                panelWaiting.transform.SetParent(parentUI);
                panelWaiting.transform.localScale = Vector3.one;
                panelWaiting.transform.localPosition = Vector3.zero;
                panelWaiting.gameObject.SetActive(false);
            });
        }
        if (panelMessageSytem == null) {
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_MESSAGE_SYSTEM, (obj) => {
                panelMessageSytem = obj.GetComponent<PanelMessageSytem>();
                panelMessageSytem.transform.SetParent(parentUI);
                panelMessageSytem.transform.localScale = Vector3.one;
                panelMessageSytem.transform.localPosition = Vector3.zero;
                panelMessageSytem.gameObject.SetActive(false);
            });
        }
        if (toast == null) {
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_TOAST, (obj) => {
                toast = obj.GetComponent<Toast>();
                toast.transform.SetParent(parentUI);
                toast.transform.localScale = Vector3.one;
                toast.transform.localPosition = Vector3.zero;
                toast.gameObject.SetActive(false);
            });
        }
    }

    // Use this for initialization
    void Start() {
#if !UNITY_WEBGL
        Application.targetFrameRate = 60;
#endif
        new ListernerServer(this);
        currenStage = login;
        login.Appear();
        //menu.DisAppear();
        //room.DisAppear();
        tlmn.DisAppear();
        tlmnsl.DisAppear();
        phom.DisAppear();
        poker5.DisAppear();
        //poker9.DisAppear();
        xam.DisAppear();
        xito.DisAppear();
        lieng5.DisAppear();
        //lieng9.DisAppear();
        bacay.DisAppear();
        maubinh.DisAppear();
        xocdia.DisAppear();
        xeng.DisAppear();
        taixiu.DisAppear();

        //disableAllDialog();
        if (NetworkUtil.GI().connected)
            NetworkUtil.GI().sendMessage(SendData.onGetPhoneCSKH());
		//NetworkUtil.GI().connect(SendData.onGetPhoneCSKH());
    }
    public void setStage(StageControl stage) {
        if (currenStage != stage) {
            backState = currenStage;
        }
        if (currenStage != room) {
            currenStage.DisAppear();
        }
        if (stage == room)
            isInfo = true;
        else
            isInfo = false;
        stage.Appear();
        currenStage = stage;
    }
    public bool isInfo = true;
    public void setCasino(int gameID, int type) {
        isInfo = false;
        switch (gameID) {
            case GameID.TLMN:
                setStage(tlmn);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.TLMNsolo:
                setStage(tlmnsl);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.XAM:
                setStage(xam);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.LIENG:
                // if (type == 0) { // 5
                setStage(lieng5);
                currentCasino = (BaseCasino)currenStage;
                // } else { // 9
                //setStage(lieng9);
                //currentCasino = (BaseCasino)currenStage;
                //}
                break;
            case GameID.BACAY:
                //if (type == 0) { // 5
                setStage(bacay);
                currentCasino = (BaseCasino)currenStage;
                //} else { // 9
                //setStage(bacay9);
                //currentCasino = (BaseCasino)currenStage;
                //}
                break;
            case GameID.PHOM:
                setStage(phom);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.POKER:
                //if (type == 0) { // 5
                setStage(poker5);
                currentCasino = (BaseCasino)currenStage;
                // }
                //    else { // 9
                //        setStage(poker9);
                //        currentCasino = (BaseCasino)currenStage;
                //    }
                break;
            case GameID.XITO:
                setStage(xito);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.MAUBINH:
                setStage(maubinh);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.XOCDIA:
                setStage(xocdia);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.XENG:
                setStage(xeng);
                currentCasino = (BaseCasino)currenStage;
                break;
            case GameID.TAIXIU:
                setStage(taixiu);
                currentCasino = (BaseCasino)currenStage;
                break;
            default:
                break;
        }
        initCardType(gameID);
    }

    private void initCardType(int gameID) {
        // TODO Auto-generated method stub
        switch (gameID) {
            case GameID.LIENG:
            case GameID.BACAY:
            case GameID.PHOM:
            case GameID.XITO:
                Card.setCardType(0);
                break;
            default:
                Card.setCardType(1);
                break;
        }
    }

    internal void disableAllDialog() {
        panelWaiting.onHide();
        panelMessageSytem.onHide();
        //panelSetting.onHide();
        //panelInfoPlayer.onHide();
        //panleHelp.onHide();
        //panelNapChuyenXu.onHide();
        //panelDoiThuong.onHide();
        //panelMail.onHide();
        //panelCreateRoom.onHide();
        //panelToiBan.onHide();
        //panelMoiChoi.onHide();
        //panelChat.onHide();
        //panelRutTien.onHide();
        //panelInput.onHide();
        //panelNotiDoiThuong.onHide();
        //panelCuoc.onHide();
        //panelDatCuoc.onHide();
        //panelChangeAvata.onHide();
        //panelChangePassword.onHide();
        //panelChangeName.onHide();
        //panelRank.onHide();
        //panelUpVip.onHide();
    }

    void disableDialog() {
    }

    public void UnloadAllSubScene() {
        unloadscene(Res.AS_SUBSCENES_SETTING);
        unloadscene(Res.AS_SUBSCENES_RANK);
        unloadscene(Res.AS_SUBSCENES_PLAYER_INFO);
        unloadscene(Res.AS_SUBSCENES_CHANGE_AVATA);
        unloadscene(Res.AS_SUBSCENES_CHANGE_NAME);
        unloadscene(Res.AS_SUBSCENES_CHANGE_PASS);
        unloadscene(Res.AS_SUBSCENES_HELP);
        unloadscene(Res.AS_SUBSCENES_ADD_COIN);
        unloadscene(Res.AS_SUBSCENES_MAIL);
        unloadscene(Res.AS_SUBSCENES_CREATE_ROOM);
        unloadscene(Res.AS_SUBSCENES_GOTO_ROOM);
        unloadscene(Res.AS_SUBSCENES_UP_VIP);
        unloadscene(Res.AS_SUBSCENES_CUOC);
        unloadscene(Res.AS_SUBSCENES_DAT_CUOC);
        unloadscene(Res.AS_SUBSCENES_INPUT);
        unloadscene(Res.AS_SUBSCENES_RUT_TIEN);
        unloadscene(Res.AS_SUBSCENES_EXCHANGE);
        unloadscene(Res.AS_SUBSCENES_INVITE_GAME);
        unloadscene(Res.AS_SUBSCENES_CHAT);
    }

    void unloadscene(string namescene) {
        if (SceneManager.GetSceneByName(namescene) != null) {
            SceneManager.UnloadScene(namescene);
        }
    }

    void OnApplicationQuit() {
        NetworkUtil.GI().cleanNetwork();
    }

    void OnApplicationPause(bool pauseStatus) {
        NetworkUtil.GI().resume(pauseStatus);
    }

    void resume() {
        //NetworkUtil.GI().resume();
    }

    public bool isTouchMB = true;
}
