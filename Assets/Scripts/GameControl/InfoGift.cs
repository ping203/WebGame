using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoGift : MonoBehaviour {
    public int id { get; set; }
    public int type { get; set; }
    public string nameGift { get; set; }
    public long cost { get; set; }
    public string telco { get; set; }
    public long price { get; set; }
    public long balance { get; set; }
    public string des { get; set; }
    public string link { get; set; }

    public RawImage img_Gift;
    public Text txt_Price, txt_Name;
    WWW www;
    bool isSet = false;

    // Update is called once per frame
    void Update() {
        if (www != null) {
            if (www.error != null) {
                isSet = false;
                www = null;
                www = new WWW(link);
                return;
            }
            if (www.isDone && !isSet) {
                img_Gift.texture = www.texture;
                isSet = true;
                www.Dispose();
                www = null;
            }
        }
    }

    internal void setInfo(int id, int type, string nameGift, long cost, string telco, long price, string des, long balance, string link) {
        this.id = id;
        this.type = type;
        this.nameGift = nameGift;
        this.cost = cost;
        this.telco = telco;
        this.price = price;
        this.des = des;
        this.balance = balance;
        this.link = link;
    }

    public void setUI() {
        txt_Price.text = BaseInfo.formatMoneyNormal(price) + Res.MONEY_VIP_UPPERCASE;
        txt_Name.text = nameGift;
        www = new WWW(link);
    }
}
