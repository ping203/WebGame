using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelChangeName : PanelGame {
	public Text oldName;
    public InputField ip_newname;

	public void onShow(string name){
		oldName.text = name;
		base.onShow ();
	}

    public void changeName () {
        GameControl.instance.sound.startClickButtonAudio();
        string tenmoi = ip_newname.text;
		if (tenmoi != "") {
			if(tenmoi.Length >= 4 && tenmoi.Length <= 20){
				SendData.onChangeName (tenmoi);
				onHide();
			}else{
                GameControl.instance.panelMessageSytem.onShow ("Tên phải nhiều hơn 4 và ít hơn 20 kí tự.");
			}
		} else {
            GameControl.instance.panelMessageSytem.onShow ("Nhập với tên mới.");
		}
	}
}
