using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityEngine.Networking;
//using UnityEngine.Experimental.Networking;
//using System.Runtime.InteropServices;

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

        // if (/*gameObject.activeInHierarchy &&*/ !isSet) {
        //StartCoroutine(coDownload(linkGift));
        // StartCoroutine(GetTexture(linkGift));
        // isSet = true;
        //}
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
    
    IEnumerator GetTexture(string link) {
        using (UnityWebRequest www = UnityWebRequest.GetTexture(link)) {
            yield return www.Send();

            if (www.isError) {
                Debug.Log(www.error);
            } else {
                Texture myTexture = DownloadHandlerTexture.GetContent(www);
                Gift.texture = myTexture;
            }
        }
    }
*/
}
