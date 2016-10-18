﻿using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Bacay : BaseCasino {
    public TimerLieng timerWaiting;
    int turntimeBC;
    long timeReceiveTurnBC;

    public Button btn_cuoc, btn_bocuoc;
    //[HideInInspector]
    //public long int_cuoc1, int_cuoc2, int_cuoc3, int_cuoc4, int_cuoc5,
    //     int_cuoc6;
    sbyte time;// tim con lai de bat dau choi
    private long maxCuoc = 0;
    public new void Awake() {
        nUsers = 5;
        chip_tong.gameObject.SetActive(false);
        base.Awake();
    }

    public void clickBtnCuoc() {
        if (maxCuoc > BaseInfo.gI().betMoney * 10) {
            showDatcuoc(BaseInfo.gI().betMoney * 2,
                    BaseInfo.gI().betMoney * 10);
        } else {
            showDatcuoc(BaseInfo.gI().betMoney * 2, maxCuoc);
        }
    }

    private void showDatcuoc(long min, long max) {
        //gameControl.panelCuoc.onShow(min, max);
        LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_CUOC, ()=> {
            PanelCuoc.instance.onShow(min, max);
        });
        //btn_bocuoc.gameObject.SetActive(false);
        //btn_cuoc.gameObject.SetActive(false);
        //showButtonCuoc(false);
    }
    public void clickBtnBoCuoc() {
        btn_bocuoc.gameObject.SetActive(false);
        btn_cuoc.gameObject.SetActive(false);
        SendData.onSendCuocBC(0);
    }

    private void showButtonCuoc(bool isShow) {
        btn_cuoc.gameObject.SetActive(isShow);
        btn_bocuoc.gameObject.SetActive(isShow);
        long maxCuoc = players[0].getFolowMoney();
        if (maxCuoc > players[getPlayer(masterID)].getFolowMoney()) {
            maxCuoc = players[getPlayer(masterID)].getFolowMoney();
        }
        this.maxCuoc = maxCuoc;
    }
    public override void onJoinTableSuccess(string master) {
        for (int i = 0; i < nUsers; i++) {
            if (!players[i].isSit()) {
                players[i].setInvite(true);
            } else {
                players[i].setInvite(false);
            }
            ((BacayPlayer)players[i]).setVisibleDiemPlayer();
        }
        //masterID = "";
        //groupKhoa.gameObject.SetActive(false);
        if (toggleLock != null)
            toggleLock.gameObject.SetActive(false);
    }

    public override void setMasterSecond(string master) {
        for (int i = 0; i < nUsers; i++) {
            if (!players[i].isSit()) {
                players[i].setInvite(true);
            } else {
                players[i].setInvite(false);
            }
        }
        //groupKhoa.gameObject.SetActive(false);
        //toggleLock.gameObject.SetActive(false);
    }
    public override void resetData() {
        base.resetData();

        //lbl_timeBC.gameObject.SetActive(false);
    }
    public override void calculDiem() {
        if (players[0].cardHand3Cay.getArrCardObj()[0].getId() != 52) {
            ((BacayPlayer)players[0]).setDiem(diem3Cay());
        }
    }
    private string diem3Cay() {
        int a = BaseInfo.getScoreFinal(players[0].cardHand3Cay.getArrCardAll());
        if (a < 0) {
            return "";
        }
        if (a == 100) {
            return "Sáp";
        }
        int finalDiem = a % 10;
        String diem = "";
        if (finalDiem == 0) {
            diem = "10 điểm";
        } else {
            diem = finalDiem + " điểm";
        }
        return diem;
    }
    public override void onFinishGame(Message message) {
        try {
            isPlaying = false;
            prevPlayer = -1;
            preCard = -1;
            isStart = false;
            int total = message.reader().ReadByte();
            BaseInfo.gI().infoWin.Clear();
            for (int i = 0; i < total; i++) {
                string nick = message.reader().ReadUTF();
                int rank = message.reader().ReadByte();
                if (rank != 1 && rank != 5) {
                    // int pl = getPlayer(nick);
                    // players[pl].cardHand.setAllMo(true);
                }
                long money = message.reader().ReadLong();
                int score = message.reader().ReadInt();
                string nicks = "";
                string info = "";
                if (score == 99) {
                    nicks = nick + ": " + "MƯỜI ÁT RÔ" + " ";
                    info = "MƯỜI ÁT RÔ";
                } else if (score == 100) {
                    nicks = nick + ": " + "SÁP" + " ";
                    info = "SÁP";
                } else {
                    nicks = nick + ": " + score + " điểm";
                    info = score + " điểm";
                }
                players[getPlayer(nick)].setReady(false);
                ((BacayPlayer)players[getPlayer(nick)]).setDiem(info);
                if (getPlayer(nick) == 0) {
                    BaseInfo.gI().infoWin.Add(new InfoWin(i + 1 + ". ", nicks,
                            money, true));
                } else {
                    BaseInfo.gI().infoWin.Add(new InfoWin(i + 1 + ". ", nicks,
                            money, false));
                }
                nickFire = "";
                for (int j = 0; j < nUsers; j++) {
                    if (players[j].isPlaying()
                            && players[j].getName().Equals(nick)) {
                        ((BacayPlayer)players[j]).setRank(rank, money);

                        break;
                    }
                }
            }
            disableAllBtnTable();
            for (int j = 0; j < nUsers; j++) {
                if (players[j].isSit()) {
                    players[j].setPlaying(false);
                    players[j].setMoneyChip(0);
                }
                players[j].setTurn(false);
            }
            tongMoney = 0;
            chip_tong.setMoneyChipChu(tongMoney);
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }
    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay) {
        base.startTableOk(cardHand, msg, nickPlay);

        players[0].setCardHand(new int[] { 52, 52, 52 }, true, false, false);
        players[0].cardHand3Cay.setArrCard(cardHand, true, false, false);
        for (int i = 1; i < players.Length; i++) {
            if (players[i].isPlaying() && !players[i].st_name.Equals("")) {
                players[i].setCardHand(new int[] { 52, 52, 52 }, true, false,
                        false);
            }
        }

        turntime = 0;
        turntimeBC = 10;
        timerWaiting.setActiveNan(turntimeBC);
        timeReceiveTurnBC = GetCurrentMilli();
        gameControl.sound.startTineCountAudio();
        chip_tong.setMoneyChipChu(tongMoney);
        if (SceneManager.GetSceneByName(Res.AS_SUBSCENES_CUOC) != null) {
            // gameControl.panelCuoc.onHide();
            PanelCuoc.instance.onHide();
        }
    }

    public override void onTimeAuToStart(sbyte time) {
        base.onTimeAuToStart(time);
        if (timerWaiting != null) {
            timerWaiting.setActiveXinCho(time);
        }
        disableAllBtnTable();
    }

    public override void onStartForView(string[] playingName) {
        base.onStartForView(playingName);
        if (BaseInfo.gI().isView) {
            disableAllBtnTable();
        }
        //lbl_timeBC.gameObject.SetActive(true);
        for (int i = 0; i < players.Length; i++) {
            if (players[i].isPlaying() && !players[i].st_name.Equals("")) {
                players[i].setCardHand(new int[] { 52, 52, 52 }, true, false,
                        false);
            }

        }
        //lbl_timeBC.text = ("8");

        turntime = 0;
        turntimeBC = 10;
        timeReceiveTurnBC = GetCurrentMilli();
    }
    public override void setTurn(string nick, Message message) {
        base.setTurn(nick, message);

    }
    public override void InfoCardPlayerInTbl(Message message, string turnName, int time, sbyte numP) {
        base.InfoCardPlayerInTbl(message, turnName, time, numP);
        try {
            for (int i = 0; i < numP; i++) {
                string name = message.reader().ReadUTF();
                long chip = message.reader().ReadLong();
                players[getPlayer(name)].setPlaying(true);
                players[getPlayer(name)].cardHand.setArrCard(new int[] { 52,
                        52, 52 }, false, false, false);
                if (chip >= 0) {
                    players[getPlayer(name)].setMoneyChip(chip);
                    tongMoney += players[getPlayer(name)].getMoneyChip();
                    players[getPlayer(name)].chipBay.onMoveto(players[getPlayer(name)].getMoneyChip(), 1);
                }
            }

            chip_tong.setMoneyChipChu(tongMoney);
            setTurn(turnName, time);
        } catch (Exception e) {
            // TODO: handle exception
        }
    }
    public override void onBeginRiseBacay(Message message) {
        base.onBeginRiseBacay(message);
        try {
            //progress.gameObject.SetActive(false);

            players[getPlayer(masterID)].setMoneyChip(0);
            gameControl.sound.startTineCountAudio();

            turntimeBC = message.reader().ReadByte();
            for (int i = 0; i < players.Length; i++) {
                players[i].resetData();
            }
            if (turntimeBC == -1) {
                turntimeBC = message.reader().ReadByte();
                //lbl_timeBC.text = ("Bàn đang bắt đầu đặt cược!");
                //lbl_timeBC.gameObject.SetActive(true);
                players[0].setPlaying(false);
                showButtonCuoc(false);
            } else {
                players[0].setPlaying(true);
                //if (players[0].isMaster()) {
                //lbl_timeBC.text = ("Thời gian nhận cược còn lại: ");
                //} else {
                //lbl_timeBC.text = ("Thời gian đặt cược còn lại: ");
                //}
                //lbl_timeBC.gameObject.SetActive(true);
                timeReceiveTurnBC = GetCurrentMilli();
                if (!players[0].isMaster()) {
                    showButtonCuoc(true);
                }
            }
            if (timerWaiting != null)
                timerWaiting.setActiveCuoc(turntimeBC);
            for (int i = 0; i < players.Length; i++) {
                ((LiengPlayer)players[i]).setVisibleDiemPlayer();
            }
        } catch (Exception e) {
            // TODO: handle exception
            Debug.LogException(e);
        }
    }
    public override void disableAllBtnTable() {
        base.disableAllBtnTable();
        btn_cuoc.gameObject.SetActive(false);
        btn_bocuoc.gameObject.SetActive(false);
    }
    public override void onCuoc3Cay(Message message) {
        base.onCuoc3Cay(message);
        try {
            if (message.reader().ReadByte() == 1) {
                string nickCuoc = message.reader().ReadUTF();
                long moneyCuoc = message.reader().ReadLong();
                tongMoney += moneyCuoc * 2;
                players[getPlayer(nickCuoc)].setMoneyChip(moneyCuoc);
                players[getPlayer(nickCuoc)].chipBay.onMoveto(players[getPlayer(nickCuoc)].getMoneyChip(), 1);
                for (int i = 0; i < players.Length; i++) {
                    if (players[i].isMaster()) {
                        players[i].setMoneyChip(tongMoney / 2);
                        players[i].chipBay.onMoveto(players[i].getMoneyChip(), 1);
                    }
                }
                chip_tong.setMoneyChipChu(tongMoney);
            } else {
                string mess = message.reader().ReadUTF();
                // CasinoActivity.gI().showToast(mess);
                showButtonCuoc(true);
            }

        } catch (Exception e) {
            // TODO: handle exception
        }
    }
    public override void onInfome(Message message) {
        base.onInfome(message);
        try {
            isStart = true;
            players[0].setPlaying(true);
            if (message.reader().ReadByte() == 1) {
                turntimeBC = message.reader().ReadByte();
                long chip = message.reader().ReadLong();
                if (chip >= 0) {
                    players[0].setMoneyChip(chip);
                }
                timeReceiveTurnBC = GetCurrentMilli();
                if (!players[0].isMaster()) {
                    showButtonCuoc(true);
                }
            } else {
                turntimeBC = message.reader().ReadByte();
                timeReceiveTurnBC = GetCurrentMilli();
                //lbl_timeBC.gameObject.SetActive(true);
                sbyte len = message.reader().ReadByte();
                int[] cardHand = new int[len];

                for (int i = 0; i < cardHand.Length; i++) {
                    cardHand[i] = message.reader().ReadByte();
                }
                players[0].setCardHand(new int[] { 52, 52, 52 }, true, false,
                        false);
                players[0].cardHand3Cay
                        .setArrCard(cardHand, true, false, false);
            }
            timerWaiting.setActiveCuoc(turntimeBC);
        } catch (Exception e) {
            // TODO: handle exception
        }
    }
    //public override void onJoinTableSuccess(Message message) {
    //    base.onJoinTableSuccess(message);
    //    int_cuoc1 = 0;
    //    int_cuoc2 = BaseInfo.gI().moneyTable * 2;
    //    int_cuoc3 = BaseInfo.gI().moneyTable * 3;
    //    int_cuoc4 = BaseInfo.gI().moneyTable * 4;
    //    int_cuoc5 = BaseInfo.gI().moneyTable * 5;
    //    int_cuoc6 = BaseInfo.gI().moneyTable * 10;
    //}
}
