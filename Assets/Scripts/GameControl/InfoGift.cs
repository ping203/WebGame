using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoGift : MonoBehaviour {
    public int idGift { get; set; }
    public long priceGift { get; set; }
    public long balance { get; set; }
    public string nameGift { get; set; }

    public RawImage Gift;
    public Text Price, Name;
    WWW www;
    bool isSet = false;
    string linkGift;

    // Update is called once per frame
    void Update() {
        if (www != null) {
            if (www.error != null) {
                isSet = false;
                www = null;
                www = new WWW(linkGift);
                return;
            }
            if (www.isDone && !isSet) {
                Gift.texture = www.texture;
                isSet = true;
                www.Dispose();
                www = null;
            }
        }
    }

    internal void setInfoGift(int id, string name, string linkGift, long longPrice, long longBalance) {
        idGift = id;
        priceGift = longPrice;
        balance = longBalance;
        nameGift = name;
        this.linkGift = linkGift;
        Price.text = BaseInfo.formatMoneyNormal(longPrice) + Res.MONEY_VIP_UPPERCASE;
        Name.text = name;
        www = new WWW(linkGift);
        //StartCoroutine(coDownload(linkGift));
    }
    /*
    IEnumerator coDownload(string link) {
        WWW www = new WWW(link);
        yield return www;

        if (www.error != null) {
            isSet = false;
        } else {
            Gift.texture = www.texture;
        }
        www.Dispose();
        www = null;
    }
    */
}
