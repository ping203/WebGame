using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PanelCreateRoom : PanelGame {
    public Slider sliderMoney;
    public Text inputMoney;
    public Text inputPlayer;

    float rateVIP, rateFREE;

    // Use this for initialization
    void Start() {
        sliderMoney.onValueChanged.AddListener(onChangeMoney);
    }

    // Update is called once per frame
    void Update() {
        //#if UNITY_WP8
        //        if (Input.GetButtonDown("Fire1") && (this.transform.localPosition.y == 160)) {
        //            OnHideKeyBoard();
        //        }

        //        if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
        //            OnHideKeyBoard();
        //        }
        //#endif
    }

    public void onChangeMoney(float value) {
        //bool isOk = false;
        //float vl = sliderMoney.value;
        //if (RoomControl.roomType == 1) {
        //    for (int j = 0; j < BaseInfo.gI().listBetMoneysFREE.Count; j++) {
        //        BetMoney b = BaseInfo.gI().listBetMoneysFREE[j];
        //        if (BaseInfo.gI().mainInfo.moneyXu < b.maxMoney) {
        //            rateFREE = (float)1 / b.listBet.Count;
        //            for (int i = 0; i < b.listBet.Count; i++) {
        //                if (vl <= i * rateFREE) {
        //                    inputMoney.text = b.listBet[i] + "";
        //                    isOk = true;
        //                    break;
        //                }
        //            }
        //        }
        //        if (isOk)
        //            break;
        //    }
        //} else {
        //    for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
        //        BetMoney b = BaseInfo.gI().listBetMoneysVIP[j];
        //        if (BaseInfo.gI().mainInfo.moneyXu < b.maxMoney) {
        //            rateVIP = (float)1 / b.listBet.Count;
        //            for (int i = 0; i < b.listBet.Count; i++) {
        //                if (vl <= i * rateVIP) {
        //                    inputMoney.text = b.listBet[i] + "";
        //                    isOk = true;
        //                    break;
        //                }
        //            }
        //        }
        //        if (isOk)
        //            break;
        //    }
        //}
        rateVIP = (float)1 / BaseInfo.gI().listBetMoneysVIP.Count;
        for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
            if (value <= j * rateVIP) {
                inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysVIP[j]);
                // money = BaseInfo.gI().listBetMoneysVIP[j];
                break;
            }
        }
    }

    public void createTableGame() {
        try {
            GameControl.instance.sound.startClickButtonAudio();
            int gameid = GameControl.instance.gameID;
            //int roomid = 0;
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
            long money = long.Parse(strMoney);
            int maxplayer = int.Parse(strMaxPlayer);

            bool check = false;
            string info = "";
            switch (GameControl.instance.gameID) {
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
                //if (RoomControl.roomType == 1) {//free
                //    if (10 * money > BaseInfo.gI().mainInfo.moneyChip) {
                //        GameControl.instance.panelMessageSytem.onShow("Không đủ tiền để tạo bàn!");
                //    } else {
                //        SendData.onCreateTable(gameid, 1, money, maxplayer, 0, "");
                //    }
                //} else {
                    if (10 * money > BaseInfo.gI().mainInfo.moneyXu) {
                        GameControl.instance.panelMessageSytem.onShow("Không đủ tiền để tạo bàn!");
                    } else {
                        SendData.onCreateTable(gameid, 2, money, maxplayer, 0, "");
                    }
                //}
            } else {
                GameControl.instance.panelMessageSytem.onShow(info);
            }
        } catch (Exception e) {
            GameControl.instance.toast.showToast("Định dạng bàn không đúng!");
            //Debug.LogException(e);
        }
    }

    public void onShow() {
        sliderMoney.value = 0;
        base.onShow();
    }
}
