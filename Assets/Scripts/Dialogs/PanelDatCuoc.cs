using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDatCuoc : PanelGame {
    public Slider sliderMoney;
    public Text inputMoney;
    private long money;
    float rateVIP, rateFREE;
    // Use this for initialization
    void Start() {
        sliderMoney.onValueChanged.AddListener(onChangeMoney);
    }

    // Update is called once per frame
    void Update() {

    }
    public void onChangeMoney(float value) {
        //bool isOk = false;
        //if (RoomControl.roomType == 1) {
        //    for (int j = 0; j < BaseInfo.gI().listBetMoneysFREE.Count; j++) {
        //        BetMoney b = BaseInfo.gI().listBetMoneysFREE[j];
        //        if (BaseInfo.gI().mainInfo.moneyXu < b.maxMoney) {
        //            rateFREE = (float)1 / b.listBet.Count;
        //            for (int i = 0; i < b.listBet.Count; i++) {
        //                if (vl <= i * rateFREE) {
        //                    inputMoney.text = BaseInfo.formatMoneyDetailDot(b.listBet[i]); ;
        //                    money = b.listBet[i];
        //                    isOk = true;
        //                    break;
        //                }
        //            }
        //        }
        //        if (isOk) break;
        //    }
        //}
        //else {
        rateVIP = (float)1 / BaseInfo.gI().listBetMoneysVIP.Count;
        for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
            if (value <= j * rateVIP) {
                inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysVIP[j]);
                money = BaseInfo.gI().listBetMoneysVIP[j];
                break;
            }
            //BetMoney b = BaseInfo.gI().listBetMoneysVIP[j];
            //rateVIP = (float)1 / b.listBet.Count;
            //if (BaseInfo.gI().mainInfo.moneyXu < b.maxMoney) {
            //    for (int i = 0; i < b.listBet.Count; i++) {
            //        if (value <= i * rateVIP) {
            //            inputMoney.text = BaseInfo.formatMoneyDetailDot(b.listBet[i]);
            //            money = b.listBet[i];
            //            //isOk = true;
            //            break;
            //        }
            //    }
            //}
            //Debug.LogError(value);
            //if (isOk)
            //    break;
            // }
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
        base.onShow();
    }
}
