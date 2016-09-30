using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelChuyenXu : PanelGame {

	public InputField ip_userId, ip_xu;
	public Slider sliderSoXu;

	// Use this for initialization
	void Start () {
		//EventDelegate.Set (sliderSoXu.onChange, onChangeValue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onChangeValue(){
		ip_xu.text = (int)(BaseInfo.gI ().mainInfo.moneyVip * sliderSoXu.value) + "";
	}

    public void onClickChuyenXu () {
        GameControl.instance.sound.startClickButtonAudio ();
		if (!BaseInfo.gI().checkNumber(ip_userId.text.Trim()) || !BaseInfo.gI().checkNumber(ip_xu.text.Trim())) {
            GameControl.instance.panelMessageSytem.onShow ("Nhập sai!");
			return;

		}
		if (ip_userId.text.Trim ().Equals ("") || ip_xu.text.Trim ().Equals ("")) {
            GameControl.instance.panelMessageSytem.onShow ("Vui lòng nhập đầy đủ thông tin!");
			return;
		}

		long userid = long.Parse (ip_userId.text.Trim());
		long xu = long.Parse (ip_xu.text.Trim());

		SendData.onXuToNick (userid, xu);
	}

    public void TuChoi () {
        GameControl.instance.sound.startClickButtonAudio ();
		ip_userId.text = "";
		ip_xu.text = "";
		sliderSoXu.value = 0;
	}
}
