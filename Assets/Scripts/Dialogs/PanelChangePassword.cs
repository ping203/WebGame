using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelChangePassword : PanelGame {
    public InputField ip_pass_current, ip_pass_new, ip_pass_again;
    public void changePass() {
        GameControl.instance.sound.startClickButtonAudio();
        string oldPass = ip_pass_current.text;
        string newPass1 = ip_pass_new.text;
        string newPass2 = ip_pass_again.text;
        if (oldPass == "" || newPass1 == "" || newPass2 == "") {
            GameControl.instance.panelMessageSytem.onShow("Bạn hãy nhập đủ thông tin.");
            return;
        }

        if (oldPass != BaseInfo.gI().pass) {
            GameControl.instance.panelMessageSytem.onShow("Mật khẩu cũ không đúng.");
            return;
        }

        if (newPass1 != newPass2) {
            GameControl.instance.panelMessageSytem.onShow("Mật khẩu không giống nhau.");
            return;
        }
        GameControl.instance.panelMessageSytem.onShow("Bạn muốn gửi tin nhắn để đổi mật khẩu.", delegate {
            SendData.onGetPass(BaseInfo.gI().mainInfo.nick);
        });
        GetComponent<UIPopUp>().HideDialog();
    }
}
