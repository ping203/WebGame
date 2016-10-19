using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PanelChat : MonoBehaviour {
    public static PanelChat instance;
    public Transform bkg_dialog;

    public GameObject tblSmile;
    public GameObject tblText;

    public InputField textChat;

    public static string[] textChats = { "Bạn ơi, đánh nhanh lên được không?", "Bắt đầu đi.",
        "Sẵn sàng đi", "Cho tớ chơi với, tớ hứa sẽ chơi ngoan!",
        "Thấy tớ đánh siêu chưa?", " Các cậu sợ tớ chưa? Heehe",
        "Tăng tiền cược lên bạn nhé?",
        "Thắng ván này tớ mời cậu đi xxx luôn.",
        "Cậu khóa bàn lại để chiến tay bo đi!", "Chết mày nè!", "Ảo vl...",
        "Huhu, sao đen đủi vậy...:(", "Chơi nhỏ chán quá!",
        "Mày hả bưởi...:D", "Tất tay đi nào!", "Đánh hay ghê!",
        "Mạng lag quá, bạn thông cảm nhé!", "Cho đánh với nào!"};
    Vector3 vtHide = new Vector3(0, -520, 0);
    // Use this for initialization
    void Awake() {
        instance = this;
        bkg_dialog.localPosition = vtHide;
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_ITEM_CHAT_SMILE, (objPre) => {
            GameObject obj = objPre;
            obj.transform.SetParent(tblSmile.transform);
            obj.transform.localScale = Vector3.one;
            LoadAssetBundle.LoadSprite(obj.GetComponent<Button>().image, Res.AS_UI_CHAT, "a" + 1);
            obj.name = "" + 0;
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                ClickSmile(obj);
            });
            for (int i = 1; i < Res.EMOTION_COUNT; i++) {
                GameObject btn = Instantiate(obj) as GameObject;
                btn.transform.SetParent(tblSmile.transform);
                btn.transform.localScale = Vector3.one;
                //btn.GetComponent<Button>().image.sprite = Res.getSmileByName("a" + (i + 1));
                LoadAssetBundle.LoadSprite(btn.GetComponent<Button>().image, Res.AS_UI_CHAT, "a" + (i + 1));
                btn.name = "" + i;
                btn.GetComponent<Button>().onClick.AddListener(delegate {
                    ClickSmile(btn);
                });
            }
        });
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_ITEM_CHAT_TEXT, (objPre) => {
            GameObject obj = objPre;
            obj.transform.SetParent(tblText.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.FindChild("Text").GetComponent<Text>().text = textChats[0];
            obj.name = "" + 0;
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                ClickText(obj);
            });
            for (int i = 1; i < textChats.Length; i++) {
                GameObject btnT = Instantiate(obj) as GameObject;
                btnT.transform.SetParent(tblText.transform);
                btnT.transform.localScale = Vector3.one;
                btnT.transform.FindChild("Text").GetComponent<Text>().text = textChats[i];
                btnT.name = "" + i;
                btnT.GetComponent<Button>().onClick.AddListener(delegate {
                    ClickText(btnT);
                });
            }
        });
    }

    //void OnEnable() {
    //    onShow();
    //}

    public void sendChatQuick() {
        GameControl.instance.sound.startClickButtonAudio();
        string text = textChat.text;
        if (!text.Equals("")) {
            SendData.onSendMsgChat(text);
            textChat.text = "";
            onHide();
        }
    }

    void ClickSmile(GameObject index) {
        GameControl.instance.sound.startClickButtonAudio();

        int i = int.Parse(index.name);

        string text = Chat.smileys[i];
        SendData.onSendMsgChat(text);
        onHide();
    }

    void ClickText(GameObject index) {
        GameControl.instance.sound.startClickButtonAudio();
        SendData.onSendMsgChat(textChats[int.Parse(index.name)]);
        onHide();
    }

    public void onShow() {
        gameObject.SetActive(true);
        bkg_dialog.transform.DOLocalMoveY(vtHide.y + 400, 0.2f);
    }

    public void onHide() {
        bkg_dialog.transform.DOLocalMoveY(vtHide.y, 0.2f).OnComplete(delegate {
            gameObject.SetActive(false);
        });
    }
}
