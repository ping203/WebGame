using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class NPCController : PanelGame {
    [HideInInspector]
    public int id = 0;
    [HideInInspector]
    public string name_player = "";

    public GameObject gourp_player;
    public GameObject btn_duoi;

    [HideInInspector]
    public Toggle tg;

    public void onClick(string action) {
        switch (action) {
            case "info":
                info();
                break;
            case "kick":
                kick();
                break;
            case "tomato":
                actionInGame(6);
                break;
            case "kiss":
                actionInGame(5);
                break;
            case "cake":
                actionInGame(2);
                break;
            case "hammer":
                actionInGame(4);
                break;
            case "beer":
                actionInGame(1);
                break;
            case "toy":
                actionInGame(6);
                break;
            case "flower":
                actionInGame(3);
                break;
        }
    }
    /*
    private void change() {
        int r = Random.Range(0, sprites_girl.Length);
        img_girl.sprite = sprites_girl[r];
        img_girl.SetNativeSize();
        r = Random.Range(0, str_names.Length);
        girl_name.text = str_names[r];
    }
    private void coin_girl() {

    }

    private void buy() {

    }*/

    public void setShowKick(bool isShow) {
        btn_duoi.SetActive(isShow);
    }

    private void info() {
        SendData.onViewInfoFriend(name_player);
        tg.isOn = false;
    }
    private void kick() {
        SendData.onKick(name_player);
        tg.isOn = false;
    }

    private void actionInGame(int index) {
        SendData.onBuyItem(index, BaseInfo.gI().mainInfo.nick, name_player);
        tg.isOn = false;
    }

    private void actionNem(int id) {
        ABSUser player1 = GameControl.instance.currentCasino.players[0];

        float distance = Vector2.Distance(player1.transform.position, tg.transform.position);
        float time = distance / 200;
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.action_name_ingame[id - 1], (objPre) => {
            GameObject obj = objPre;

            obj.transform.SetParent(player1.transform.parent);
            obj.transform.localPosition = player1.transform.localPosition;
            obj.transform.localScale = new Vector3(1, 1, 1);

            obj.transform.DOLocalMove(tg.transform.localPosition, time).OnComplete(delegate {
                finish(obj);
            });
        });
    }
    void finish(GameObject ob) {
        ob.GetComponent<Animator>().SetTrigger("action");
        Destroy(ob, 3f);
    }
}
