using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDoiXuChip : PanelGame {
	public InputField ip_doi;
	public Slider slider;
	public Text lbNhan, tyle_xu_chip, tyle_chip_xu;

    int chip = 0, xu = 0;

	// Use this for initialization
	void Start () {
		if(tyle_xu_chip!=null)
		tyle_xu_chip.text = "(Tỷ lệ 1 " + Res.MONEY_VIP_UPPERCASE +" = " + BaseInfo.gI().tyle_xu_sang_chip + " " + Res.MONEY_FREE_UPPERCASE +")";
		if(tyle_chip_xu!=null)
            tyle_chip_xu.text = "(Tỷ lệ " + BaseInfo.gI ().tyle_chip_sang_xu + " " + Res.MONEY_FREE_UPPERCASE + " = 1 " + Res.MONEY_VIP_UPPERCASE + ")";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//panel doi chip
	public void onChangeValueSliderXuToChip(){
		xu = (int)(BaseInfo.gI ().mainInfo.moneyVip * slider.value);
        ip_doi.text = xu + "";

        long c = xu * BaseInfo.gI ().tyle_xu_sang_chip;
        lbNhan.text = Res.MONEY_FREE_UPPERCASE + " = " + BaseInfo.formatMoneyDetailDot (c);
		//onChangeValueInputDoiChip ();
	}

	public void onChangeValueInputDoiChip(){
		if (!ip_doi.text.Trim ().Equals ("")) {
            int xuu = int.Parse (ip_doi.text);
            long c = xuu * BaseInfo.gI ().tyle_xu_sang_chip;
            lbNhan.text = Res.MONEY_FREE_UPPERCASE + " = " + BaseInfo.formatMoneyDetailDot (c);

		} else {
            lbNhan.text = Res.MONEY_FREE_UPPERCASE + " = 0";
		}
	}

	//panel doi xu
	public void onChangeValueSliderChipToXu(){
        chip = (int) (BaseInfo.gI ().mainInfo.moneyFree * slider.value);
        ip_doi.text = chip + "";

        int x = (int) (chip / BaseInfo.gI ().tyle_chip_sang_xu);
        lbNhan.text = Res.MONEY_VIP_UPPERCASE + " = " + BaseInfo.formatMoneyDetailDot (x);
		//onChangeValueInputDoiXu ();
	}
	
	public void onChangeValueInputDoiXu(){
		if (!ip_doi.text.Trim ().Equals ("")) {
            int chipp = int.Parse (ip_doi.text);
            int x = (int)(chipp / BaseInfo.gI ().tyle_chip_sang_xu);
            lbNhan.text = Res.MONEY_VIP_UPPERCASE + " = " + BaseInfo.formatMoneyDetailDot (x);
		} else {
            lbNhan.text = Res.MONEY_VIP_UPPERCASE + " = 0";
		}
	}

    public void xuToChip () {
		if (ip_doi.text.Trim ().Equals ("")) {
            GameControl.instance.panelMessageSytem.onShow ("Bạn hãy nhập đủ thông tin.");
			return;
		}

		if (!BaseInfo.gI ().checkNumber (ip_doi.text.Trim ())) {
            GameControl.instance.panelMessageSytem.onShow ("Nhập sai!");
			return;
		}

		if (long.Parse(ip_doi.text.Trim ()) > BaseInfo
		    .gI().mainInfo.moneyVip) {
                GameControl.instance.panelMessageSytem.onShow ("Số xu chuyển phải <= số " + Res.MONEY_VIP +" hiện tại!");
			return;
		}

		SendData.onXuToChip (long.Parse (ip_doi.text.Trim ()));
        TuChoi ();
	}

    public void chipToXu () {
		if (ip_doi.text.Trim ().Equals ("")) {
            GameControl.instance.panelMessageSytem.onShow ("Bạn hãy nhập đủ thông tin.");
			return;
		}
		
		if (!BaseInfo.gI ().checkNumber (ip_doi.text.Trim ())) {
            GameControl.instance.panelMessageSytem.onShow ("Nhập sai!");
			return;
		}
		
		if (long.Parse(ip_doi.text.Trim ()) > BaseInfo
		    .gI().mainInfo.moneyFree) {
                GameControl.instance.panelMessageSytem.onShow ("Số chip chuyển phải <= số " + Res.MONEY_FREE +" hiện tại!");
			return;
		}
		
		SendData.onChipToXu (long.Parse (ip_doi.text.Trim ()));
        TuChoi ();
	}

    public void TuChoi () {
        GameControl.instance.sound.startClickButtonAudio ();
		ip_doi.text = "";
		slider.value = 0;
		lbNhan.text = "";
	}
}
