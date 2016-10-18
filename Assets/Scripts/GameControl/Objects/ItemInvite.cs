using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInvite : MonoBehaviour {
    public Text txt_name, txt_money;
    public string _name { set; get; }
    public string full_name { set; get; }
    public long money { set; get; }

    public void setText(string name, long money) {
        if (name.Length > 17) {
            name = name.Substring(0, 14) + "...";
        }

        txt_name.text = name;
        txt_money.text = BaseInfo.formatMoney(money);
    }
}
