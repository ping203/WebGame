using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;

public class PanelDoiThuong : PanelGame {
    public GameObject tblContaintGiftTheCao;
    public GameObject tblContaintGiftVatPham;
    public GameObject btnGift;
    public bool isLoaded = false;
    public void addGiftInfo(int id, int type, string name, long price, long balance, string link) {
        GameObject btnT = Instantiate(btnGift) as GameObject;
        if (type == 1) {
            btnT.transform.parent = tblContaintGiftTheCao.transform;
        } else {
            btnT.transform.parent = tblContaintGiftVatPham.transform;
        }

        btnT.transform.localScale = Vector3.one;
        btnT.GetComponent<InfoGift>().setInfoGift(id, name, link, price, balance);

        btnT.GetComponent<Button>().onClick.AddListener(delegate {
            sendGift(btnT);
        });
    }

    public void sendGift(GameObject gift) {
        GameControl.instance.sound.startClickButtonAudio();
        int id = gift.GetComponent<InfoGift>().idGift;
        long priceGift = gift.GetComponent<InfoGift>().priceGift;
        string name = gift.GetComponent<InfoGift>().nameGift;
        long balance = gift.GetComponent<InfoGift>().balance;
        if (BaseInfo.gI().mainInfo.moneyVip <= balance) {
            long money = balance + priceGift;
            GameControl.instance.panelMessageSytem.onShow("Bạn cần phải có ít nhất "
            + BaseInfo.formatMoneyDetailDot(money) + " " + Res.MONEY_VIP + " để đổi lấy phần quà này!");
            return;
        }
        GameControl.instance.panelMessageSytem.onShow("Bạn muốn đổi " + BaseInfo.formatMoneyNormal(priceGift) + " lấy " + name, delegate {
            SendData.onSendGift(id, priceGift);
        });
    }
}
