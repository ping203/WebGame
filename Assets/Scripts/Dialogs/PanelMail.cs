using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelMail : PanelGame {
    public Transform tblContaintSuKien;
    public Transform tblContaintTinNhan;
    public GameObject itemMail;

    public PanelMessage panelMessage;

    List<GameObject> listMail = new List<GameObject>();
    List<GameObject> listEvent = new List<GameObject>();

    public Toggle tg_mail, tg_event;
    void OnEnable() {
        if (tg_mail.isOn && listMail.Count <= 0) {
            panelMessage.setTextMail("", "", "Không có thư nào!");
        }
        if (tg_event.isOn && listEvent.Count <= 0) {
            panelMessage.setTextSK("Không có sự kiện nào!");
        }
    }

    public void addIconTinNhan(int id, string guiTu, string guiLuc, string noiDung, sbyte isRead) {
        GameObject btnT = Instantiate(itemMail) as GameObject;
        itemMail.transform.SetParent(tblContaintTinNhan);
        btnT.transform.localScale = Vector3.one;
        btnT.GetComponent<ItemMail>().setIconItemTN(id, guiTu, guiLuc, noiDung, isRead);
        btnT.GetComponent<Button>().onClick.AddListener(delegate {
            ClickDocTN(btnT);
        });
        listMail.Add(btnT);
    }

    public void addIconSuKien(int id, string title, string content) {
        GameObject btnT = Instantiate(itemMail) as GameObject;
        itemMail.transform.SetParent(tblContaintSuKien);
        btnT.transform.localScale = Vector3.one;
        btnT.GetComponent<ItemMail>().setIconItemSK(id, title, content);
        btnT.GetComponent<Button>().onClick.AddListener(delegate {
            ClickDocSK(btnT);
        });

        listEvent.Add(btnT);
    }

    public void ClearListMail() {
        for (int i = 0; i < listMail.Count; i++) {
            Destroy(listMail[i]);
        }
        listMail.Clear();
    }
    public void ClearListEvent() {
        for (int i = 0; i < listEvent.Count; i++) {
            Destroy(listEvent[i]);
        }
        listEvent.Clear();
    }

    public void ClickDocTN(GameObject obj) {
        for (int i = 0; i < listMail.Count; i++) {
            listMail[i].GetComponent<ItemMail>().setCheck(false);
        }
        ItemMail it = obj.GetComponent<ItemMail>();
        it.setCheck(true);
        int id = it.id;
        string guiluc = it.guiLuc;
        string guitu = it.guiTu;
        string nd = it.content;
        it.setRead();
        panelMessage.setTextMail(guitu, guiluc, nd);
        SendData.onReadMessage(id);
    }

    public void ClickDocSK(GameObject obj) {
        for (int i = 0; i < listMail.Count; i++) {
            listEvent[i].GetComponent<ItemMail>().setCheck(false);
        }
        ItemMail it = obj.GetComponent<ItemMail>();
        it.setCheck(true);
        int id = it.id;
        string nd = it.content;

        panelMessage.setTextSK(nd);
        SendData.onReadMessage(id);
    }

    public void onValuaChange(int i) {
        if (i == 1) {
            if (listMail.Count <= 0) {
                panelMessage.setTextMail("", "", "Không có thư nào!");
            }
        } else {
            if (listEvent.Count <= 0) {
                panelMessage.setTextSK("Không có sự kiện nào!");
            }
        }
    }

    public bool isShowNew() {
        if (listMail.Count > 0 || listEvent.Count > 0)
            return true;
        return false;
    }
}
