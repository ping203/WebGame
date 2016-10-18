using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInput : MonoBehaviour {
   public static PanelInput instance;
    public Text lb_title, lb_display_1, lb_display_2, lb_text_ishide;
    public InputField ip_enter;
    public delegate void CallBack();
    public CallBack onClickOK;

    void Awake() {
        instance = this;
    }

    public void onShow(string title, string display1, string display2, CallBack clickOK) {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            lb_title.text = title;
            lb_display_1.text = display1;
            lb_display_2.text = display2;
            onClickOK = clickOK;
        });
    }

    public void onShow_GetPass() {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            lb_text_ishide.gameObject.SetActive(false);
            lb_title.text = "LẤY LẠI MẬT KHẨu";
            lb_display_2.text = "Tài khoản:";
            onClickOK = delegate {
                string nick = ip_enter.text;
                if (!nick.Equals("")) {
                    SendData.onGetPass(nick);
                    //Debug.Log(nick);
                    onHide();
                } else {
                    GameControl.instance.panelMessageSytem.onShow("Tài khoản không đúng!");
                }
            };
        });
    }

    public void onClickButtonOK() {
        GameControl.instance.sound.startClickButtonAudio();
        onClickOK.Invoke();
    }

    public int checkSDT(string sdt) {
        if (sdt.Length > 11 || sdt.Length < 10)
            return -3;

        for (int i = 0; i < sdt.Length; i++) {
            char c = sdt[i];
            if (('0' > c) || (c > '9')) {
                return -1;
            }
        }

        return 1;
    }

    public void onHide() {
        GetComponent<UIPopUp>().HideDialog();
    }
}
