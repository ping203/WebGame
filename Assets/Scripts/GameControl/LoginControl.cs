using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class LoginControl : StageControl {
    //public Text lb_version;
    public InputField input_username;
    public InputField input_passsword;
    NetworkUtil net;

    public InputField input_username_reg;
    public InputField input_passsword_reg;
    public InputField input_try_passsword_reg;
    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
            onBack();
        }
    }

    void Awake() {
        net = GameObject.FindObjectOfType<NetworkUtil>();
        input_username.text = PlayerPrefs.GetString("username");
        input_passsword.text = PlayerPrefs.GetString("password");
        OnSubmit();
    }

    void OnEnable() {
        isLoginFB = false;
    }

    private bool checkNetWork() {
        return true;
    }
    /**
 * 
 * @param username
 * @param pass
 * @param type
 *            : 1-facebook 2-choingay 3-gmail 4-login normal
 * @param imei
 * @param link_avatar
 * @param tudangky
 *            : 1 la tu dang ky, 0
 * @param displayName
 * @param accessToken
 * @param regPhone
 */
    public void login(sbyte type, string username, string pass,
                               string imei, string link_avatar, sbyte tudangky, string displayName,
                               string accessToken, string regPhone) {
        BaseInfo.gI().isPurchase = false;
        if (checkNetWork()) {
            //if (NetworkUtil.GI().isConnected()) {
            //    NetworkUtil.GI().close();
            //}
            //if(net != null) {
            //    StartCoroutine(net.Start());
            //}
            Message msg = new Message(CMDClient.CMD_LOGIN_NEW);
            try {
                msg.writer().WriteByte(type);
                msg.writer().WriteUTF(username);
                msg.writer().WriteUTF(pass);
                msg.writer().WriteUTF(Res.version);
                msg.writer().WriteByte(CMDClient.PROVIDER_ID);
                msg.writer().WriteUTF(imei);
                msg.writer().WriteUTF(link_avatar);
                msg.writer().WriteByte(tudangky);
                msg.writer().WriteUTF(displayName);
                msg.writer().WriteUTF(accessToken);
                msg.writer().WriteUTF(regPhone);
            } catch (Exception ex) {
                Debug.LogException(ex);
            }
            //SendData.isLogin = true;
            NetworkUtil.GI().sendMessage(msg);
            BaseInfo.gI().username = username;
            BaseInfo.gI().pass = pass;
        } else {
            gameControl.panelMessageSytem.onShow("Vui lòng kiểm tra kết nối mạng!");
        }
    }

    public void onClick(string action) {
        switch (action) {
            case "login":
                doLogin();
                break;
            case "reg":
                onReg();
                break;
            case "playnow":
                gameControl.panelWaiting.onShow();
                clickLoginPlayNow();
                break;
            case "playfb":
                clickOnFacebook();
                break;
            case "getpass":
                clickQuenMK();
                break;
            case "setting":
                clickSetting();
                break;
            case "help":
                clickHelp();
                break;
        }
    }

    public void onReg() {
        GameControl.instance.sound.startClickButtonAudio();
        string info = "";
        string nick = input_username_reg.text;
        string mk = input_passsword_reg.text;
        string mkNhapLai = input_try_passsword_reg.text;
        string imei = GameControl.IMEI;
        bool kt = false;
        if (nick.Equals("")) {
            info = "Nhập vào tên đăng nhập!";
            kt = true;
        } else if (nick.Length <= 5) {
            info = "Tên đăng nhập phải lớn hơn 5 ký tự!";
            kt = true;
        } else if (mk.Equals("")) {
            info = "Hãy nhập vào mật khẩu!";
            kt = true;
        } else if (!mk.Equals(mkNhapLai)) {
            info = "Mật khẩu không trùng nhau!";
            kt = true;
        }

        if (kt) {
            gameControl.panelMessageSytem.onShow(info);
            return;
        } else {
            gameControl.panelWaiting.onShow();
            BaseInfo.gI().username = nick;
            BaseInfo.gI().pass = mk;
            StartCoroutine(delayReg(nick, mk, imei));
        }
    }

    public void loginWhenRegSucces() {
        gameControl.panelWaiting.onHide();
        input_username.text = BaseInfo.gI().username;
        input_passsword.text = BaseInfo.gI().pass;
        //doLogin();
        tg_dn.isOn = true;
        tg_dk.isOn = false;
    }

    [SerializeField]
    Toggle tg_dn, tg_dk;

    IEnumerator delayReg(string username, string pass, string imei) {
        if (net != null) {
            if (!net.connected) {
                StartCoroutine(net.Start());
                while (!net.connected) {
                    yield return new WaitForSeconds(0.1f);
                    if (net.connected) {
                        SendData.onRegister(username, pass, imei);
                    }
                }
            } else {
                SendData.onRegister(username, pass, imei);
            }
        }
    }
    IEnumerator delay(sbyte type, string username, string pass,
                             string imei, string link_avatar, sbyte tudangky, string displayName,
                             string accessToken, string regPhone) {
        if (net != null) {
            if (!net.connected) {
                StartCoroutine(net.Start());
                while (!net.connected) {
                    yield return new WaitForSeconds(0.1f);
                    if (net.connected) {
                        login(type, username, pass, imei, link_avatar, tudangky, displayName, accessToken, regPhone);
                    }
                }
            } else {
                login(type, username, pass, imei, link_avatar, tudangky, displayName, accessToken, regPhone);
            }
        }
    }
    void doLogin() {
        GameControl.instance.sound.startClickButtonAudio();
        string username = input_username.text;
        string password = input_passsword.text;
        if (username.Equals("") || password.Equals("")) {
            gameControl.panelMessageSytem.onShow("Bạn chưa nhập thông tin!");
            return;
        }

        gameControl.panelWaiting.onShow();
        string imei = GameControl.IMEI;
        StartCoroutine(delay(4, username, password, imei, "", 0, username, "", ""));
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.Save();
    }

    void clickLoginPlayNow() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelWaiting.onShow();
        string imei = GameControl.IMEI;
        login(2, imei, imei, imei, "", 1, "", "", "");
    }

    void clickOnFacebook() {
        GameControl.instance.sound.startClickButtonAudio();
#if UNITY_WEBGL
        Application.ExternalCall("myFacebookLogin");
#endif
        // Debug.Log("clickOnFacebook");
    }
    public bool isLoginFB { set; get; }
    public void sendloginFB(string accessToken) {
        // login(1, "sgc", "sgc", GameControl.IMEI, "", 1, "", accessToken, "");
        gameControl.panelWaiting.onShow();
        StartCoroutine(delay(1, "sgc", "sgc", GameControl.IMEI, "", 1, "", accessToken, ""));
        isLoginFB = true;
    }

    void clickSetting() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panelSetting.onShow();
    }

    void clickHelp() {
        GameControl.instance.sound.startClickButtonAudio();
        gameControl.panleHelp.onShow();
    }

    void clickCSKH() {
        gameControl.panelMessageSytem.onShow("Gọi điện đến tổng đài chăm sóc khách hàng "
            + Res.TXT_PhoneNumber + "?", delegate {
                Application.OpenURL("tel://" + Res.TXT_PhoneNumber);
            });
    }

    private int checkName(string username) {
        if (username.Length > 10 || username.Length < 4)
            return -3;

        for (int i = 0; i < username.Length; i++) {
            char c = username[i];
            if ((('0' > c) || (c > '9')) && (('A' > c) || (c > 'Z'))
                && (('a' > c) || (c > 'z'))) {
                return -1;
            }
        }
        bool isTrung = true;
        for (int i = 0; i < username.Length - 1; i++) {
            char c1 = username[i];
            char c2 = username[i + 1];
            if (c1 != c2) {
                isTrung = false;
                break;
            }
        }
        if (isTrung) {
            return -4;
        }
        bool isLT = false;
        if (username[0] == '0' || username[0] == '1') {
            isLT = true;
            for (int i = 0; i < username.Length - 1; i++) {
                char c1 = username[i];
                char c2 = username[i + 1];
                if (('0' <= c1) && (c1 <= '9')) {

                } else {
                    isLT = false;
                    break;
                }
                if (('0' <= c2) && (c2 <= '9')) {

                } else {
                    isLT = false;
                    break;
                }
                if (int.Parse(c1 + "") != int.Parse(c2 + "") - 1) {
                    isLT = false;
                    break;
                }
            }
        }
        if (isLT) {
            return -4;
        }
        return 1;
    }

    void clickCapNhat() {
        if (checkNetWork()) {
            gameControl.panelWaiting.onShow();
            NetworkUtil
                    .GI()
                    .sendMessage(
                            SendData.onGetMessageUpdateVersionNew(CMDClient.PROVIDER_ID));
        } else {
            gameControl.panelMessageSytem.onShow("Vui lòng kiểm tra kết nối mạng!");
        }

    }

    void clickGioiThieuBanChoi() {
        if (checkNetWork()) {
            gameControl.panelWaiting.onShow();
            NetworkUtil
                    .GI()
                    .sendMessage(
                            SendData.onGetMessageIntroduceFriend(CMDClient.PROVIDER_ID));
        } else {
            gameControl.panelMessageSytem.onShow("Vui lòng kiểm tra kết nối mạng!");
        }


    }

    void clickQuenMK() {
        //gameControl.dialogQuenMK.onShow(); 
        gameControl.panelInput.onShow_GetPass();
    }

    void OnSubmit() {
        //this.loginGroup.transform.localPosition = new Vector3(0, 0, 0);
    }

    void OnShowKeyBoard() {
        //TweenPosition.Begin(this.loginGroup, 0.25f, new Vector3(0, 160, 0));
    }

    public override void Appear() {
        base.Appear();
        OnSubmit();
    }

    public override void onBack() {
        base.onBack();
        Application.Quit();
    }
}
