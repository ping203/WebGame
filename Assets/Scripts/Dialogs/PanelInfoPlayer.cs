using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInfoPlayer : PanelGame {
    public Text txt_id;
    public Text txt_name;
    public Text txt_xu;
    public Text txt_thang_thua;
    // public Text chip;
    public Image Img_Avata;
    public RawImage Raw_Avata;
    //public PanelChangePassword panelChangePassword;
    //public PanelChangeName panelChangeName;
    //public PanelChangeAvata panelChangeAvata;
    public GameObject changePass, changeName, changeAvata, updateInfo;
    public InputField ip_email, ip_phone;

    //public Text[] label;

    //public Image[] stars;

    WWW www;
    bool isOne = false;
    // Update is called once per frame
    void Update() {
        if (www != null) {
            if (www.isDone && !isOne) {
                Raw_Avata.texture = www.texture;
                isOne = true;
            }
        }
    }

    public void clickChangePass() {
        GameControl.instance.sound.startClickButtonAudio();
        GameControl.instance.panelChangePassword.onShow();
        onHide();
    }

    public void clickChangeName() {
        GameControl.instance.sound.startClickButtonAudio();
        GameControl.instance.panelChangeName.onShow(BaseInfo.gI().mainInfo.displayname);
        onHide();
    }

    public void clickChangeAvata() {
        GameControl.instance.sound.startClickButtonAudio();
        GameControl.instance.panelChangeAvata.onShow();
        onHide();
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
        long xuMe = BaseInfo.gI().mainInfo.moneyXu;
        long chipMe = BaseInfo.gI().mainInfo.moneyChip;
        string slt = BaseInfo.gI().mainInfo.soLanThang;
        string slth = BaseInfo.gI().mainInfo.soLanThua;
        int idAva = BaseInfo.gI().mainInfo.idAvata;
        string link_ava = BaseInfo.gI().mainInfo.link_Avatar;
        string email = BaseInfo.gI().mainInfo.email;
        string phone = BaseInfo.gI().mainInfo.phoneNumber;
        int num_star = BaseInfo.gI().mainInfo.level_vip;

        infoProfile(n, uid, xuMe, chipMe, slt, slth, link_ava, idAva, email, phone, num_star);
    }

    public void updateAvata() {
        int id = BaseInfo.gI().mainInfo.idAvata;
        Img_Avata.sprite = Res.getAvataByID(id);
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

        if (GameControl.instance.room.isActive) {
            bool isMe = nameinfo.Equals(BaseInfo.gI().mainInfo.displayname);
            //Neu của mh thì hiện lên
            changePass.SetActive(isMe);
            changeName.SetActive(isMe);
            changeAvata.SetActive(isMe);
            // updateInfo.SetActive(isMe);
            // if (isMe) {
            //  ip_email.readOnly = false;
            //  ip_phone.readOnly = false;
            // }
        }

        txt_name.text = "Tên: " + nameinfo;
        txt_id.text = "ID: " + userid;
        txt_xu.text = BaseInfo.formatMoneyDetailDot(xuinfo) + " " + Res.MONEY_VIP_UPPERCASE;
        // chip.text = Res.MONEY_FREE + ": " + BaseInfo.formatMoneyDetailDot(chipinfo);
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
        www = null;
        if (link_avata.Equals("")) {
            Img_Avata.gameObject.SetActive(true);
            Raw_Avata.gameObject.SetActive(false);
            Img_Avata.sprite = Res.getAvataByID(idAvata);
        } else if (link_avata != "") {
            Img_Avata.gameObject.SetActive(false);
            Raw_Avata.gameObject.SetActive(true);
            www = new WWW(link_avata);
            isOne = false;
        }
    }
}
