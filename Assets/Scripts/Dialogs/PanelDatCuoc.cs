using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDatCuoc : MonoBehaviour {
    public static PanelDatCuoc instance;
    public Slider sliderMoney;
    public Text inputMoney;
    private long money;
    float rateVIP, rateFREE;

    void Awake() {
        instance = this;
    }
    // Use this for initialization
    void Start() {
        sliderMoney.onValueChanged.AddListener(onChangeMoney);
    }
    
    public void onHide() {
        GetComponent<UIPopUp>().HideDialog();
    }

    public void onChangeMoney(float value) {
        if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
            rateVIP = (float)1 / BaseInfo.gI().listBetMoneysVIP.Count;
            for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
                if (value <= j * rateVIP) {
                    inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysVIP[j]);
                    money = BaseInfo.gI().listBetMoneysVIP[j];
                    break;
                }
            }
        } else {
            rateFREE = (float)1 / BaseInfo.gI().listBetMoneysFREE.Count;
            for (int j = 0; j < BaseInfo.gI().listBetMoneysFREE.Count; j++) {
                if (value <= j * rateFREE) {
                    inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysFREE[j]);
                    money = BaseInfo.gI().listBetMoneysFREE[j];
                    break;
                }
            }
        }
    }

    public void clickOK() {
        GameControl.instance.sound.startClickButtonAudio();
        SendData.onChangeBetMoney(money);
        onHide();
    }
    public void onShow() {
        sliderMoney.value = 0;
        onChangeMoney(0);
    }
}
