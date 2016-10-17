using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PanelCreateRoom : PanelGame {
    public Slider sliderMoney;
    public Text inputMoney;
    public InputField inputPlayer;

    float rateVIP, rateFREE;

    long money = 0;

    // Use this for initialization
    void Start() {
        sliderMoney.onValueChanged.AddListener(onChangeMoney);
    }
    public void onChangeMoney(float value) {
        if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
            rateVIP = (float)1 / BaseInfo.gI().listBetMoneysVIP.Count;
            for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
                if (value <= j * rateVIP) {
                    money = BaseInfo.gI().listBetMoneysVIP[j];
                    inputMoney.text = BaseInfo.formatMoneyDetailDot(money);
                    break;
                }
            }
        } else {
            rateFREE = (float)1 / BaseInfo.gI().listBetMoneysFREE.Count;
            for (int j = 0; j < BaseInfo.gI().listBetMoneysFREE.Count; j++) {
                if (value <= j * rateFREE) {
                    money = BaseInfo.gI().listBetMoneysFREE[j];
                    inputMoney.text = BaseInfo.formatMoneyDetailDot(money);
                    break;
                }
            }
        }
    }

    public void createTableGame() {
        try {
            GameControl.instance.sound.startClickButtonAudio();
            int gameid = GameControl.instance.gameID;
            string strMoney = inputMoney.text;
            string strMaxPlayer = inputPlayer.text;
            if (strMoney == "" || strMaxPlayer == "") {
                GameControl.instance.panelMessageSytem.onShow("Bạn chưa điền đủ thông tin!");
                return;
            }
            if (!BaseInfo.gI().checkNumber(strMoney) || !BaseInfo.gI().checkNumber(strMaxPlayer)) {
                GameControl.instance.panelMessageSytem.onShow("Nhập sai!");
                return;
            }
            int maxplayer = int.Parse(strMaxPlayer);

            bool check = false;
            string info = "";
            switch (gameid) {
                case GameID.TLMN:
                case GameID.PHOM:
                case GameID.XAM:
                case GameID.MAUBINH: {
                        if (maxplayer > 4 || maxplayer < 2) {
                            check = false;
                            info = "Số người phải lớn hơn 2 và nhỏ hơn 4";
                        } else {
                            check = true;
                        }
                        break;
                    }
                case GameID.POKER:
                case GameID.XITO:
                case GameID.LIENG:
                case GameID.BACAY: {
                        if (maxplayer > 5 || maxplayer < 2) {
                            check = false;
                            info = "Số người phải lớn hơn 2 và nhỏ hơn 5";
                        } else {
                            check = true;
                        }
                        break;
                    }
                case GameID.XOCDIA: {
                        if (maxplayer != 9) {
                            check = false;
                            info = "Số người phải bằng 9!";
                        } else
                            check = true;
                    }
                    break;
                case GameID.TLMNsolo: {
                        if (maxplayer != 2) {
                            check = false;
                            info = "Số người phải bằng 2!";
                        } else
                            check = true;
                    }
                    break;
            }

            if (check) {
                if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
                    if (10 * money > BaseInfo.gI().mainInfo.moneyVip) {
                        GameControl.instance.panelMessageSytem.onShow("Không đủ tiền để tạo bàn!");
                    } else {
                        SendData.onCreateTable(gameid, 2, money, maxplayer, 0, "");
                    }
                } else {
                    if (10 * money > BaseInfo.gI().mainInfo.moneyFree) {
                        GameControl.instance.panelMessageSytem.onShow("Không đủ tiền để tạo bàn!");
                    } else {
                        SendData.onCreateTable(gameid, 1, money, maxplayer, 0, "");
                    }
                }
            } else {
                GameControl.instance.panelMessageSytem.onShow(info);
            }
        } catch (Exception e) {
            GameControl.instance.toast.showToast("Định dạng bàn không đúng!");
        }
    }

    public override void onShow() {
        sliderMoney.value = 0;
        onChangeMoney(0);
        base.onShow();
    }
}
