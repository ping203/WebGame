using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelUpVip : MonoBehaviour {
    public static PanelUpVip instance;
    public Text txt_info;
    public int vip;
    void Awake() {
        instance = this;
    }
    void Start() {
        txt_info.text = "VIP " + vip;
    }
    public void onClickShare() {
#if UNITY_WEBGL
        Application.ExternalCall("ShareFB", vip);
#endif
    }

    //public void onShow(int lv_vip) {
    //    vip = lv_vip;
    //    txt_info.text = "VIP " + lv_vip;
    //    base.onShow();
    //}
}
