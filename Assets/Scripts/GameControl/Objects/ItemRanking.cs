using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemRanking : MonoBehaviour {
    public Image img_avata, icon_top;
    // public RawImage raw_avata;
    public Text lb_Name;
    public Text lb_money;
    public Text lb_stt;
    public Sprite[] sp_top;

    public int rank { set; get; }
    public int id_avata { set; get; }
    public string rank_name { set; get; }
    public long money { set; get; }

    public void SetData(int rank, int id_avata, string rank_name, long money) {
        this.rank = rank;
        this.id_avata = id_avata;
        this.rank_name = rank_name;
        this.money = money;
    }

    public void setUI() {
        icon_top.gameObject.SetActive(true);
        switch (rank) {
            case 1:
                icon_top.sprite = sp_top[0];
                break;
            case 2:
                icon_top.sprite = sp_top[1];
                break;
            case 3:
                icon_top.sprite = sp_top[2];
                break;
            default:
                icon_top.gameObject.SetActive(false);
                break;
        }
        icon_top.SetNativeSize();
        lb_stt.text = rank.ToString();
        //img_avata.sprite = Res.getAvataByID(idAvatar);
        LoadAssetBundle.LoadSprite(img_avata, Res.AS_AVATA, "" + id_avata);
        if (rank_name.Length > 10) {
            rank_name = rank_name.Substring(0, 10) + "...";
        }

        lb_Name.text = rank_name;
        lb_money.text = BaseInfo.formatMoney(money);
    }
}
