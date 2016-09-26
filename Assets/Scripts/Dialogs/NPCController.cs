using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class NPCController : PanelGame {
    [HideInInspector]
    public int id = 0;
    [HideInInspector]
    public string name_player = "";

    //public Image img_girl;
    //public Sprite[] sprites_girl;
    //public string[] str_names = new string[] { "Tâm Tít", "Ngọc Trinh", "Maria Ozawa", "Mai Thỏ", "Elly Trần" };

    //public Text girl_name;
    //public Text num_tool;

    //public Button btn_change;
    public GameObject gourp_player;

    //public bool isPlayer;// { set; get; }
    [HideInInspector]
    public Toggle tg;
    //void OnEnable() {
    //    if (!isPlayer) {
    //       // btn_change.gameObject.SetActive(true);
    //        gourp_player.SetActive(false);
    //    } else {
    //        //btn_change.gameObject.SetActive(false);
    //        gourp_player.SetActive(true);
    //    }
    //}


    public void onClick(string action) {
        switch (action) {
            //case "change":
            //    change();
            //    break;
            //case "coin":
            //    coin_girl();
            //    break;
            //case "buy":
            //    buy();
            //    break;
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

    private void info() {
        SendData.onViewInfoFriend(name_player);
        tg.isOn = false;
    }
    private void kick() {
        SendData.onKick(name_player);
        tg.isOn = false;
    }

    private void actionInGame(int index) {
        //if (isPlayer) {
        SendData.onBuyItem(index, BaseInfo.gI().mainInfo.nick, name_player);
        //} else {
        //    actionNem(index);

        //}
        tg.isOn = false;
    }

    private void actionNem(int id) {
        ABSUser player1 = GameControl.instance.currentCasino.players[0];// = players[getPlayer(nem)];

        float distance = Vector2.Distance(player1.transform.position, tg.transform.position);
        float time = distance / 200;
        GameObject obj = Instantiate(GameControl.instance.gameObj_Actions_InGame[id - 1]) as GameObject;

        obj.transform.parent = player1.transform.parent;
        obj.transform.localPosition = player1.transform.localPosition;
        obj.transform.localScale = new Vector3(1, 1, 1);

        obj.transform.DOLocalMove(tg.transform.localPosition, time).OnComplete(delegate {
            finish(obj);
        });
    }
    void finish(GameObject ob) {
        ob.GetComponent<Animator>().SetTrigger("action");
        Destroy(ob, 3f);
    }
}
