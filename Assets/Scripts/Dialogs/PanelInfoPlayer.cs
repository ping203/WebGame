using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInfoPlayer : PanelGame {
    public static PanelInfoPlayer instance;
    public Text txt_id;
    public Text txt_name;
    public Text txt_xu;
    public Text txt_chip;
    public Text txt_thang_thua;
    // public Text chip;
    public Image Img_Avata;
    public RawImage Raw_Avata;
    //public PanelChangePassword panelChangePassword;
    //public PanelChangeName panelChangeName;
    //public PanelChangeAvata panelChangeAvata;
    public GameObject changePass, changeName, changeAvata, updateInfo;
    public InputField ip_email, ip_phone;
    void Awake() {
        instance = this;
    }

    //public Text[] label;

    //public Image[] stars;

    /* WWW www;
     bool isOne = false;
     // Update is called once per frame
     void Update() {
         /*if (www != null) {
             if (www.isDone && !isOne) {
                 Raw_Avata.texture = www.texture;
                 isOne = true;
             }
         }
     }*/

    bool isLoginFB;

    void OnEnable() {
        isLoginFB = GameControl.instance.login.isLoginFB;
    }

    public void clickChangePass() {
        GameControl.instance.sound.startClickButtonAudio();
        if (isLoginFB) {
            GameControl.instance.panelMessageSytem.onShow("Bạn không thể đổi mật khẩu.");
        } else {
            //GameControl.instance.panelChangePassword.onShow();
            //onHide();
            LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_CHANGE_PASS, () => {
                GetComponent<UIPopUp>().HideDialog();
            });
        }
    }

    public void clickChangeName() {
        GameControl.instance.sound.startClickButtonAudio();
        if (isLoginFB) {
            GameControl.instance.panelMessageSytem.onShow("Bạn không thể đổi tên.");
        } else {
            //GameControl.instance.panelChangeName.onShow(BaseInfo.gI().mainInfo.displayname);
            //onHide();
            LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_CHANGE_NAME, () => {
                GetComponent<UIPopUp>().HideDialog();
            });
        }
    }

    public void clickChangeAvata() {
        GameControl.instance.sound.startClickButtonAudio();
        if (isLoginFB) {
            GameControl.instance.panelMessageSytem.onShow("Bạn không thể đổi avata.");
        } else {
            // GameControl.instance.panelChangeAvata.onShow();
            LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_CHANGE_AVATA, () => {
                GetComponent<UIPopUp>().HideDialog();
            });
        }
    }

    public void clickUpdateInfo() {
        string phone = ip_phone.text;
        string email = ip_email.text;
        string info = "";
        if (phone.Equals("") || email.Equals("")) {
            info = "Bạn chưa nhập email hoặc số điện thoại.";
        } else if (!BaseInfo.gI().checkNumber(phone) || phone.Length < 10 || phone.Length > 11) {
            info = "Định dạng số điện thoại không đúng.";
        } else if (!BaseInfo.gI().checkMail(email)) {
            info = "Định dạng email không đúng.";
        }
        if (!info.Equals("")) {
            GameControl.instance.panelMessageSytem.onShow(info);
        } else {
            SendData.onUpdateProfile(email, phone);
        }
    }

    //public void onClickEditMail() {
    //    string email = ip_email.text;
    //    if (email.Equals(""))
    //        return;

    //    SendData.onUpdateProfile(email, BaseInfo.gI().mainInfo.phoneNumber);
    //    BaseInfo.gI().mainInfo.email = email;
    //}

    //public void onClickEditPhone() {
    //    string phone = ip_phone.text;
    //    if (phone.Equals(""))
    //        return;
    //    if (phone.Length == 10 || phone.Length == 11
    //                        || phone.Length == 12) {
    //        SendData.onUpdateProfile(BaseInfo.gI().mainInfo.email, phone);
    //        BaseInfo.gI().mainInfo.phoneNumber = phone;
    //    } else {
    //        GameControl.instance.panelMessageSytem.onShow("Số điện thoại không đúng!", delegate {
    //        });
    //    }
    //}

    public void infoMe() {
        string n = BaseInfo.gI().mainInfo.displayname;
        long uid = BaseInfo.gI().mainInfo.userid;
        long xuMe = BaseInfo.gI().mainInfo.moneyVip;
        long chipMe = BaseInfo.gI().mainInfo.moneyFree;
        string slt = BaseInfo.gI().mainInfo.soLanThang;
        string slth = BaseInfo.gI().mainInfo.soLanThua;
        int idAva = BaseInfo.gI().mainInfo.idAvata;
        string link_ava = BaseInfo.gI().mainInfo.link_Avatar;
        string email = BaseInfo.gI().mainInfo.email;
        string phone = BaseInfo.gI().mainInfo.phoneNumber;
        int num_star = BaseInfo.gI().mainInfo.level_vip;

        infoProfile(n, uid, xuMe, chipMe, slt, slth, link_ava, idAva, email, phone, num_star);
    }

    public void infoProfile(string nameinfo, long userid, long xuinfo, long chipinfo,
        string slthang, string slthua, string link_avata, int idAvata,
        string email, string phone, int num_star) {
        // Ẩn các thông tin ko phải của mh
        changePass.SetActive(false);
        changeName.SetActive(false);
        changeAvata.SetActive(false);
        // updateInfo.SetActive(false);
        // ip_email.readOnly = true;
        // ip_phone.readOnly = true;

        if (GameControl.instance.isInfo) {
            // bool isMe = nameinfo.Equals(BaseInfo.gI().mainInfo.displayname);
            //Neu của mh thì hiện lên
            changePass.SetActive(true);
            changeName.SetActive(true);
            changeAvata.SetActive(true);
            // updateInfo.SetActive(isMe);
            // if (isMe) {
            //  ip_email.readOnly = false;
            //  ip_phone.readOnly = false;
            // }
        }

        txt_name.text = "Tên: " + nameinfo;
        txt_id.text = "ID: " + userid;
        txt_xu.text = BaseInfo.formatMoneyDetailDot(xuinfo) + " " + Res.MONEY_VIP_UPPERCASE;
        txt_chip.text = BaseInfo.formatMoneyDetailDot(chipinfo) + " " + Res.MONEY_FREE_UPPERCASE;
        //ip_email.text = email;
        //ip_phone.text = phone;

        //for (int i = 0; i < 5; i++) {
        //    //stars[i].spriteName = "Sao_toi_to";
        //    //if (i < num_star) {
        //    //    stars[i].spriteName = "Sao_sang_to";
        //    //}
        //}
        
        if (slthang.Length != 0 && slthua.Length != 0) {
            string[] st = slthang.Split(',');
            int slth = 0;
            int slthu = 0;
            for (int i = 0; i < st.Length; i++) {
                string[] kq = st[i].Split('-');
                //label[i].text = kq[1];
                slth += int.Parse(kq[1]);
            }

            string[] st1 = slthua.Split(',');
            for (int i = 0; i < st1.Length; i++) {
                string[] kq = st1[i].Split('-');
                //label[i].text += "/" + kq[1];
                slthu += int.Parse(kq[1]);
            }

            txt_thang_thua.text = "Thắng: " + slth + "\t\t\t\t\tThua: " + slthu;
        }
        //www = null;
        if (link_avata.Equals("")) {
            Img_Avata.gameObject.SetActive(true);
            Raw_Avata.gameObject.SetActive(false);
            //Img_Avata.sprite = Res.getAvataByID(idAvata);
            LoadAssetBundle.LoadSprite(Img_Avata, Res.AS_AVATA, "" + idAvata);
        } else {
            //Img_Avata.gameObject.SetActive(false);
            // Raw_Avata.gameObject.SetActive(true);
            // www = new WWW(link_avata);
            // isOne = false;
            StartCoroutine(getAvata(link_avata));
        }
    }
    IEnumerator getAvata(string link) {
        WWW www = new WWW(link);
        yield return www;
        Img_Avata.gameObject.SetActive(false);
        Raw_Avata.gameObject.SetActive(true);
        Raw_Avata.texture = www.texture;
        www.Dispose();
        www = null;
    }
}
