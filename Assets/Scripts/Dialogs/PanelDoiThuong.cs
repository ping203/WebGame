using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelDoiThuong : PanelGame {
    public Transform parentTheCao;
    public Transform parentVatPham;
    List<InfoGift> list_gift = new List<InfoGift>();

    void Start() {
        list_gift = GameControl.instance.list_gift;
        addGiftInfo();
    }

    //public void addGiftInfo(int id, int type, string name, long price, long balance, string link) {
    void addGiftInfo() {
        if (list_gift.Count <= 0)
            return;
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_BUTTON_GIFT, (prefabAB) => {
            GameObject obj = prefabAB;
            if (list_gift[0].type == 1) {
                obj.transform.SetParent(parentTheCao);
            } else {
                obj.transform.SetParent(parentVatPham);
            }
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<InfoGift>().setInfo(
                list_gift[0].id,
                list_gift[0].type,
                list_gift[0].nameGift,
                list_gift[0].cost,
                list_gift[0].telco,
                list_gift[0].price,
                list_gift[0].des,
                list_gift[0].balance,
                list_gift[0].link);
            obj.GetComponent<InfoGift>().setUI();
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                sendGift(obj);
            });

            for (int i = 1; i < list_gift.Count; i++) {
                GameObject btnT = Instantiate(obj) as GameObject;
                if (list_gift[i].type == 1) {
                    btnT.transform.SetParent(parentTheCao);
                } else {
                    btnT.transform.SetParent(parentVatPham);
                }

                btnT.transform.localScale = Vector3.one;

                btnT.GetComponent<InfoGift>().setInfo(
                    list_gift[i].id,
                    list_gift[i].type,
                    list_gift[i].nameGift,
                    list_gift[i].cost,
                    list_gift[i].telco,
                    list_gift[i].price,
                    list_gift[i].des,
                    list_gift[i].balance,
                    list_gift[i].link);
                btnT.GetComponent<InfoGift>().setUI();
                btnT.GetComponent<Button>().onClick.AddListener(delegate {
                    sendGift(btnT);
                });
            }
        });
    }
    public void sendGift(GameObject gift) {
        GameControl.instance.sound.startClickButtonAudio();
        int id = gift.GetComponent<InfoGift>().id;
        long priceGift = gift.GetComponent<InfoGift>().price;
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
