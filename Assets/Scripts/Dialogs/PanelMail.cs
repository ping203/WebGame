﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelMail : PanelGame {
    public static PanelMail instance;
    public Transform parentSuKien;
    public Transform parentTinNhan;

    public PanelMessage panelMessage;

    public Toggle tg_mail, tg_event;
    List<ItemMail> listMail = new List<ItemMail>();
    List<ItemMail> listEvent = new List<ItemMail>();
    void Awake() {
        instance = this;
    }

    void Start() {
        tg_mail.onValueChanged.AddListener(onValuaChangeMail);
        tg_event.onValueChanged.AddListener(onValuaChangeEvent);
    }

    public void load() {
        listMail = GameControl.instance.listMail;
        listEvent = GameControl.instance.listEvent;
        Debug.Log(listMail.Count + " =========== " + listEvent.Count);
        addIconTinNhan();
        addIconSuKien();
    }

    void OnEnable() {
        if (tg_mail.isOn) {
            if (listMail.Count <= 0)
                panelMessage.setTextMail("", "", "Không có thư nào!");
            else
                ClickDocTN(parentTinNhan.GetChild(0).gameObject);
        }
        if (tg_event.isOn) {
            if (listEvent.Count <= 0)
                panelMessage.setTextSK("Không có sự kiện nào!");
            else
                ClickDocTN(parentSuKien.GetChild(0).gameObject);
        }
    }

    //public void addIconTinNhan(int id, string guiTu, string guiLuc, string noiDung, sbyte isRead) {
    public void addIconTinNhan() {
        if (listMail.Count <= 0) { return; }
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_ITEM_MAIL, (objPre) => {
            GameObject obj = objPre;
            obj.transform.SetParent(parentTinNhan);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<ItemMail>().setIconItemTN(listMail[0].id, listMail[0].guiTu, listMail[0].guiLuc, listMail[0].content, listMail[0].isRead);
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                ClickDocSK(obj);
            });
            for (int i = 1; i < listMail.Count; i++) {
                GameObject btnT = Instantiate(obj) as GameObject;
                btnT.transform.SetParent(parentTinNhan);
                btnT.transform.localScale = Vector3.one;
                btnT.GetComponent<ItemMail>().setIconItemTN(listMail[i].id, listMail[i].guiTu, listMail[i].guiLuc, listMail[i].content, listMail[i].isRead);
                btnT.GetComponent<Button>().onClick.AddListener(delegate {
                    ClickDocTN(btnT);
                });
            }
        });
        //listMail.Add(btnT.GetComponent<ItemMail>());
    }

    //public void addIconSuKien(int id, string title, string content) {
    public void addIconSuKien() {
        if (listEvent.Count <= 0) { return; }
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_ITEM_MAIL, (objPre) => {
            GameObject obj = objPre;
            obj.transform.SetParent(parentSuKien);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<ItemMail>().setIconItemSK(listEvent[0].id, "", listEvent[0].content);
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                ClickDocSK(obj);
            });
            for (int i = 1; i < listEvent.Count; i++) {
                GameObject btnT = Instantiate(obj) as GameObject;
                btnT.transform.SetParent(parentSuKien);
                btnT.transform.localScale = Vector3.one;
                btnT.GetComponent<ItemMail>().setIconItemSK(listEvent[i].id, "", listEvent[i].content);
                btnT.GetComponent<Button>().onClick.AddListener(delegate {
                    ClickDocSK(btnT);
                });
            }
        });
        //listEvent.Add(btnT.GetComponent<ItemMail>());
    }
    /*
    public void ClearListMail() {
        //for (int i = 0; i < tblContaintTinNhan.childCount; i++) {
        //    Destroy(tblContaintTinNhan.GetChild(i).gameObject);
        //}
        foreach (Transform it in tblContaintTinNhan) {
            Destroy(it.gameObject);
        }
        listMail.Clear();
    }
    public void ClearListEvent() {
        //for (int i = 0; i < tblContaintSuKien.childCount; i++) {
        //    Destroy(tblContaintSuKien.GetChild(i).gameObject);
        //}
        foreach (Transform it in tblContaintSuKien) {
            Destroy(it.gameObject);
        }
        listEvent.Clear();
    }
    */
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

    public void onValuaChangeMail(bool isactive) {
        Debug.Log("Mail:   " + isactive);
        if (!isactive)
            return;
        if (isactive) {
            if (listMail.Count <= 0)
                panelMessage.setTextMail("", "", "Không có thư nào!");
            else
                ClickDocTN(parentSuKien.GetChild(0).gameObject);
        }
    }

    public void onValuaChangeEvent(bool isactive) {
        Debug.Log("Event:   " + isactive);
        if (!isactive)
            return;
        if (isactive) {
            if (listEvent.Count <= 0)
                panelMessage.setTextSK("Không có sự kiện nào!");
            else
                ClickDocSK(parentSuKien.GetChild(0).gameObject);
        }
    }

    public bool isShowNew() {
        if (listMail.Count > 0 || listEvent.Count > 0)
            return true;
        return false;
    }
}
