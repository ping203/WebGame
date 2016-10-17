using UnityEngine;
using System.Collections;
using System;

public class ProcessHandler : MessageHandler {

    protected override void serviceMessage(Message message, int messageId) {
        try {
            DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
                int check = 0, card = -1;
                sbyte b;
                switch (messageId) {
                    case CMDClient.CMD_SET_NEW_MASTER:
                        listenner.onSetNewMaster(message.reader().ReadUTF());
                        break;
                    //case CMDClient.CMD_UPDATE_VERSION:
                    //    listenner.onUpdateVersion(message);
                    //    break;
                    case CMDClient.CMD_SMS:
                        listenner.onInfoSMS(message);
                        break;
                    case CMDClient.CMD_VIEW_INFO_FRIEND:
                        //Debug.LogError ("CMD_VIEW_INFO_FRIEND");
                        listenner.onDetailUser(message);
                        break;
                    //case CMDClient.CMD_SET_MONEY:
                    //    listenner.onSetMoneyTable(message.reader().ReadInt());
                    //    break;
                    //case CMDClient.CMD_CHAT:
                    //    listenner.onMsgChat(message.reader().ReadUTF(), message
                    //    .reader().ReadUTF());
                    //    break;
                    case CMDClient.CMD_LOGIN:
                        //if (!SendData.isLogin) {
                        //    break;
                        //}
                        b = message.reader().ReadByte();
                       // Debug.Log(" -------------- LOGIN " + b);
                        if (b == 1) {
                            listenner.onLoginSuccess(message);
                        } else {
                            listenner.onLoginFail(b, message.reader().ReadUTF());
                        }
                        //SendData.isLogin = false;
                        break;
                    //case CMDClient.CMD_JOIN_ROOM:

                    //    break;
                    case CMDClient.CMD_LOGIN_FIRST:
                        listenner.onLoginfirst(message);
                        break;
                    case CMDClient.CMD_JOIN_TABLE_PLAY:
                        // check = SerializerHelper.readInt(message);
                        //Debug.LogError ("CMD_JOIN_TABLE_PLAY");
                        check = message.reader().ReadByte();
                        if (check == 1) {
                            BaseInfo.gI().numberPlayer = message.reader().ReadByte();
                            BaseInfo.gI().idTable = message.reader().ReadShort();
                            BaseInfo.gI().betMoney = message.reader().ReadLong();
                            BaseInfo.gI().needMoney = message.reader().ReadLong();
                            BaseInfo.gI().maxMoney = message.reader().ReadLong();
                            BaseInfo.gI().currentMinMoney = BaseInfo.gI().needMoney;
                            BaseInfo.gI().currentMaxMoney = BaseInfo.gI().maxMoney;
                            // BaseInfo.gI().choinhanh = message.reader().ReadInt();
                            BaseInfo.gI().moneyTable = BaseInfo.gI().betMoney;
                            listenner.onJoinTablePlaySuccess(message);
                            GameControl.instance.disableAllDialog();
                        } else {
                            if (check == -1) {
                                string info = message.reader().ReadUTF();
                                listenner.onJoinTablePlayFail(info);
                            } else {
                                message.reader().ReadInt();
                                // SendData.onJoinTableForView(message.reader().ReadInt(),
                                //           "");

                                string info = message.reader().ReadUTF();
                                listenner.onMessageServer(info);
                            }
                        }
                        break;
                    case CMDClient.CMD_JOIN_TABLE:
                        //Debug.LogError ("CMD_JOIN_TABLE");
                        check = message.reader().ReadByte();//Join success or fail. 1 is success, 0 is fail.
                        if (check == 1) {
                            BaseInfo.gI().numberPlayer = message.reader().ReadByte();
                            BaseInfo.gI().idTable = message.reader().ReadShort();
                            BaseInfo.gI().betMoney = message.reader().ReadLong();
                            BaseInfo.gI().needMoney = message.reader().ReadLong();
                            BaseInfo.gI().maxMoney = message.reader().ReadLong();
                            BaseInfo.gI().currentMinMoney = BaseInfo.gI().needMoney;
                            BaseInfo.gI().currentMaxMoney = BaseInfo.gI().maxMoney;
                            //BaseInfo.gI().choinhanh = message.reader().ReadInt();

                            //TODO: new data.

                            //Luat choi
                            //int luatchoi = message.reader ().ReadByte ();
                            //string chuphong = message.reader ().ReadUTF ();
                            //int nPlayer = message.reader ().ReadByte ();
                            //BaseInfo.gI ().numberPlayer = nPlayer;
                            //int timeTimeOut = message.reader ().ReadInt ();
                            //bool statusTable = message.reader ().ReadBoolean ();

                            listenner.onJoinTableSuccess(message);

                            GameControl.instance.disableAllDialog();
                        } else {
                            listenner.onJoinTableFail(message.reader().ReadUTF());
                        }
                        break;
                    case CMDClient.CMD_START_GAME:
                        int by = message.reader().ReadByte();
                        if (by == 0) {
                            string info = message.reader().ReadUTF();
                            listenner.onStartFail(info);
                        } else if (by == 1) {
                            listenner.onStartSuccess(message);
                        } else if (by == 2) {
                            listenner.onStartForView(message);
                        }
                        break;
                    case CMDClient.CMD_SET_TURN:
                        listenner.onSetTurn(message);
                        break;
                    //case CMDClient.CMD_LIST_ROOM:// nhan ds room tu server
                    //    listenner.onListRoom(message);
                    //    break;
                    case CMDClient.CMD_EXIT_VIEW:// nhan ds room tu server
                        listenner.onExitView(message);
                        break;
                    case CMDClient.CMD_LIST_TABLE:
                        card = message.reader().ReadShort();
                        //Debug.LogError ("CMDClient.CMD_LIST_TABLE:::::totalTable: " + card);
                        if (card == -1) {
                            listenner.onJoinRoomFail(message.reader().ReadUTF());
                        } else {
                            listenner.onListTable(card, message);
                        }
                        break;
                    case CMDClient.CMD_PROFILE:
                        //Debug.LogError ("CMDClient.CMD_PROFILE");
                        listenner.onProfile(message);
                        break;
                    case CMDClient.CMD_JOIN_GAME:
                        listenner.onJoinGame(message);
                        break;
                    case CMDClient.CMD_USER_JOIN_TABLE:
                        listenner.onUserJoinTable(message.reader().ReadShort(), message
                                    .reader().ReadUTF(), message.reader().ReadUTF(), message.reader().ReadUTF(), message.reader().ReadInt(),
                                    message.reader().ReadByte(), message.reader()
                                            .ReadLong(), message.reader()
                                            .ReadLong());
                        break;
                    case CMDClient.CMD_EXIT_TABLE:
                        //Debug.LogError("CMDClient.CMD_EXIT_TABLE");
                        listenner.onUserExitTable(message.reader().ReadShort(), message
                            .reader().ReadUTF(), message.reader().ReadUTF());
                        break;
                    case CMDClient.CMD_READY:
                        listenner.onReady(message);
                        break;
                    case CMDClient.CMD_ALLCARD_FINISH:
                        listenner.onAllCardPlayerFinish(message);
                        break;
                    case CMDClient.CMD_GAMEOVER:
                        listenner.onFinishGame(message);
                        break;
                    case CMDClient.CMD_GET_PASS:
                        //if (SendData.getPass) {
                            listenner.onGetPass(message);
                        //}
                        break;
                    case CMDClient.CMD_REGISTER:
                        card = message.reader().ReadByte();
                        if (card == 0) {
                            listenner.onRegFail(message.reader().ReadUTF());
                        } else {
                            listenner.onRegSuccess(message);
                        }
                        break;
                    case CMDClient.CMD_INVITE_FRIEND:// nhan loi moi tu a vao tbid
                        sbyte confirm = message.reader().ReadByte();
                        if (confirm == 0) {

                        } else {
                            if (!BaseInfo.gI().isView) {
                                string nickInvite = message.reader().ReadUTF();
                                string displayName = message.reader().ReadUTF();
                                sbyte gameid = message.reader().ReadByte();
                                //short roomid = message.reader().ReadShort();
                                short tblid = message.reader().ReadShort();
                                long money_ = message.reader().ReadLong();
                                long minMoney = message.reader().ReadLong();
                                long maxMoney = message.reader().ReadLong();
                                sbyte roomid = message.reader().ReadByte();

                                listenner.onInvite(nickInvite, displayName, gameid, roomid, tblid, money_, minMoney, maxMoney);
                            }
                        }
                        break;
                    case CMDClient.CMD_KICK:// 41
                        // card = SerializerHelper.readInt(message);
                        card = message.reader().ReadByte();
                        switch (card) {
                            case 0:
                                break;
                            case 1:
                                listenner.onKickOK();
                                break;
                            case 2:
                                listenner.onSysKick();
                                break;
                        }
                        break;
                    case CMDClient.CMD_UPDATE_ROOM:
                        //Debug.LogError ("CMD_UPDATE_ROOM");
                        int numberTablesPerRoom = message.reader().ReadShort();
                        listenner.onUpdateRoom(numberTablesPerRoom, message);
                        break;
                    case CMDClient.CMD_CHAT_MSG:
                        listenner.onChatMessage(message.reader().ReadUTF(), message.reader().ReadUTF(), message.reader().ReadBoolean());
                        break;
                    //case CMDClient.CMD_VESION:
                    //    listenner.onUpdateVersion(message.reader().ReadUTF(), message
                    //    .reader().ReadUTF(), message.reader().ReadUTF());
                    //    break;
                    //case CMDClient.CMD_PING_PONG:

                    //    break;
                    case CMDClient.CMD_UPDATE_PROFILE:
                        listenner.onUpdateProfile(1, "");
                        break;
                    case CMDClient.CMD_DEL_MESSAGE:
                        listenner.onDelMessage(message);
                        break;
                    case CMDClient.CMD_GET_INBOX_MESSAGE:
                        listenner.onInboxMessage(message);
                        break;
                    case CMDClient.CMD_READ_MESSAGE:
                        listenner.onReadMessage(message);
                        break;
                    case CMDClient.CMD_CHANGE_BETMONEY:
                        listenner.onChangeBetMoney(message);
                        break;
                    case CMDClient.CMD_ID_GAME:
                        //Debug.LogError ("CMD_ID_GAME");
                        listenner.onGameID(message);
                        break;
                    case CMDClient.CMD_SERVER_MESSAGE:
                        listenner.onMessageServer(message.reader().ReadUTF());
                        break;
                    case CMDClient.CMD_UPDATE_MONEY:
                        string nick = message.reader().ReadUTF();
                        //int type = message.reader().ReadInt();//Bo
                        int type = 0; //Xu
                        long moneyVip = message.reader().ReadLong();
                        long moneyFree = message.reader().ReadLong();
                        listenner.onUpdateMoneyMessage(nick, type, moneyVip, moneyFree);
                        break;
                    case CMDClient.CMD_LIST_INVITE:
                        listenner.onListInvite(message);
                        break;
                    case CMDClient.CMD_CHANGERULETBL:
                        listenner.onChangeRuleTbl(message.reader().ReadByte());
                        break;
                    case CMDClient.CMD_UPDATEMONEY_PLAYER_INTBL:
                        listenner.onUpdateMoneyTbl(message);
                        break;
                    case CMDClient.CMD_BAO_SAM:
                        listenner.onBaoXam(message);
                        break;
                    case CMDClient.CMD_ALERT_LINK:
                        listenner.onGetAlertLink(message);
                        break;
                    case CMDClient.CMD_INFO_WINPLAYER:
                        listenner.infoWinPlayer(message);
                        break;
                    case CMDClient.CMD_INFOPLAYER_TBL:
                        listenner.InfoCardPlayerInTbl(message);
                        break;
                    case CMDClient.CMD_INFO_GIFT2:
                        listenner.InfoGift(message);
                        break;
                    case CMDClient.CMD_START_FLIP:
                        listenner.startFlip(message);
                        break;
                    case CMDClient.CMD_FLIP_CARD:
                        listenner.onCardFlip(message);
                        break;
                    //case CMDClient.CMD_UPDATE_VERSION_NEW:
                    //    listenner.onUpdateVersionNew(message.reader().ReadUTF());
                    //    break;
                    //case CMDClient.INTRODUCE_FRIEND:
                    //    listenner.onIntroduceFriend(message.reader().ReadUTF(), message
                    //            .reader().ReadUTF());
                    //    break;
                    case CMDClient.CMD_CALMB_RANKS:
                        listenner.onRankMauBinh(message);
                        break;
                    case CMDClient.CMD_FINAL_MAUBINH:
                        listenner.onFinalMauBinh(message.reader().ReadUTF());
                        break;
                    case CMDClient.CMD_WINMAUBINH:
                        listenner.onWinMauBinh(message.reader().ReadUTF(), message.reader().ReadByte());
                        break;
                    case CMDClient.CMD_INFO_ME:
                        listenner.onInfoMe(message);
                        break;
                    case CMDClient.CMD_BEGINRISE_3CAY:
                        listenner.onBeginRiseBacay(message);
                        break;
                    case CMDClient.CMD_FLIP_3CAY:
                        listenner.onFlip3Cay(message);
                        break;
                    case CMDClient.CMD_CUOC_3CAY:
                        listenner.onCuoc3Cay(message);
                        break;

                    case CMDClient.CMD_SET_MONEY:
                        listenner.onSetMoneyTable(message.reader().ReadLong());
                        break;
                    //case CMDClient.CMD_CHAT:
                    //    listenner.onMsgChat(message.reader().ReadUTF(), message
                    //    .reader().ReadUTF());
                    //    break;

                    //case CMDClient.CMD_JOIN_ROOM:

                    //    break;

                    //case CMDClient.CMD_REGISTER:

                    //    card = message.reader().ReadByte();
                    //    if (card == 0)
                    //    {
                    //        listenner.onRegFail(message.reader().ReadUTF());
                    //    }
                    //    else
                    //    {
                    //        listenner.onRegSuccess(message);
                    //    }
                    //    break;


                    case CMDClient.CMD_TOP:
                        //Debug.LogError("CMD TOP");
                        listenner.onTop(message);
                        break;
                    case CMDClient.CMD_QUESTINFO:
                        listenner.onInfoNhiemvu(message);
                        break;
                    case CMDClient.CMD_UPDATE_QUEST:
                        listenner.onUpdateNhiemvu(message);
                        break;
                    case CMDClient.CMD_RECEIVE_FREE_MONEY:
                        listenner.onReceiveFreeMoney(message);
                        break;
                    case CMDClient.CMD_GET_MONEY:
                        listenner.onGetMoney();
                        break;
                    case CMDClient.CMD_TIME_AUTOSTART:
                        listenner.onTimeAuToStart(message);
                        break;

                    case CMDClient.CMD_LIST_EVENT:
                        listenner.onListEvent(message);
                        break;
                    case CMDClient.CMD_POPUP_NOTIFY:
                        listenner.onPopupNotify(message);
                        break;
                    //case CMDClient.CMD_NEW_EVENT:
                    //    listenner.onNewEvent(message);
                    //    break;
                    //case CMDClient.CMD_INFO_SMS_APPSTORE:
                    //    listenner.onInfoSMSAppStore(message);
                    //    break;
                    case CMDClient.CMD_BUY_ITEM:
                        //sbyte ib = message.reader().ReadByte();
                        //if (ib == 0)
                        listenner.onBuyItem(message);
                        break;
                    case CMDClient.CMD_FOR_VIEW:
                        check = message.reader().ReadByte();
                        if (check == 1) {
                            BaseInfo.gI().numberPlayer = message.reader().ReadByte();
                            BaseInfo.gI().idTable = message.reader().ReadShort();
                            BaseInfo.gI().betMoney = message.reader().ReadLong();
                            BaseInfo.gI().moneyTable = BaseInfo.gI().betMoney;
                            listenner.onJoinView(message);
                        } else {
                            listenner.onJoinTableFail(message.reader().ReadUTF());
                        }
                        break;
                    case CMDClient.CMD_CREATE_TABLE:
                        listenner.onCreateTable(message);
                        break;
                    case CMDClient.CMD_UPDATE_AVATA:
                        listenner.onUpdataAvata(message);
                        break;
                    case CMDClient.CMD_CHANGE_NAME:
                        listenner.onChangeName(message);
                        break;
                    case CMDClient.CMD_LIST_BET_MONEY:
                        listenner.onListBetMoney(message);
                        break;
                    case CMDClient.CMD_RATE_SCRATCH_CARD:
                        //Debug.LogError ("CMD_RATE_SCRATCH_CARD");
                        listenner.onRateScratchCard(message);
                        break;
                    case CMDClient.CMD_XU_TO_NICK:
                        listenner.onXuToNick(message);
                        break;
                    case CMDClient.CMD_XU_TO_CHIP:
                        listenner.onXuToChip(message);
                        break;
                    case CMDClient.CMD_CHIP_TO_XU:
                        listenner.onChipToXu(message);
                        break;
                    case CMDClient.CMD_GETPHONECSKH:
                        listenner.onGetPhoneCSKH(message);
                        break;
                    case CMDClient.CMD_SMS_9029:
                        listenner.onSMS9029(message);
                        break;
                    case CMDClient.CMD_CARD_XEP_MB:
                        listenner.onCardXepMB(message);
                        break;
                    case CMDClient.CMD_PHOM_HA:
                        listenner.onPhomha(message);
                        break;
                    //Xoc dia
                    case CMDClient.CMD_BEGIN_XOCDIA:
                        listenner.onBeGinXocDia(message);
                        break;
                    case CMDClient.CMD_BEGIN_XOCDIA_CUOC:
                        listenner.onBeginXocDiaTimeDatcuoc(message);
                        break;
                    case CMDClient.CMD_MO_BAT:
                        listenner.onXocDiaMobat(message);
                        break;
                    case CMDClient.CMD_XOCDIA_DATCUOC:
                        listenner.onXocDiaDatcuoc(message);
                        break;
                    case CMDClient.CMD_ARR_BET_XD:
                        listenner.onXocDiaCacMucCuoc(message);
                        break;
                    case CMDClient.CMD_DATLAI:
                        listenner.onXocDiaDatlai(message);
                        break;
                    case CMDClient.CMD_GAPDOI:
                        listenner.onXocDiaDatGapdoi(message);
                        break;
                    case CMDClient.CMD_HUYCUOC:
                        listenner.onXocDiaHuycuoc(message);
                        break;
                    case CMDClient.CMD_UPDATE_CUA:
                        listenner.onXocDiaUpdateCua(message);
                        break;
                    case CMDClient.CMD_HISTORY_XD:
                        listenner.onXocDiaHistory(message);
                        break;
                    case CMDClient.CMD_CHUCNANG_HUYCUA:
                        listenner.onXocDiaChucNangHuycua(message);
                        break;
                    case CMDClient.CMD_BEGIN_XOCDIA_DUNGCUOC:
                        listenner.onXocDiaBeginTimerDungCuoc(message);
                        break;
                    case CMDClient.CMD_GET_FREE_MONEY:
                        listenner.onMoneyFree(message.reader().ReadLong());
                        break;
                    //Xoc dia.
                    case CMDClient.CMD_HIDE_NAPTIEN:
                        BaseInfo.gI().isHidexuchip = message.reader().ReadInt();
                        break;
                    case CMDClient.CMD_JOIN_XENG:
                        listenner.onXeng_joinGame(message);
                        break;
                    case CMDClient.CMD_EXIT_XENG:
                        listenner.onXeng_exitGame(message);
                        break;
                    case CMDClient.CMD_CUOC_XENG:
                        listenner.onXeng_datCuoc(message);
                        break;
                    case CMDClient.CMD_GAMEOVER_XENG:
                        listenner.onXeng_gameOver(message);
                        break;
                    case CMDClient.CMD_START_XENG:
                        listenner.onXeng_quayXeng(message);
                        break;
                    //Tai xiu
                    case CMDClient.CMD_UPDATE_MONEY_TAIXIU:
                        listenner.onupdatemoneyTaiXiu(message);
                        break;
                    case CMDClient.CMD_JOIN_TAIXIU:
                        listenner.onjoinTaiXiu(message);
                        break;
                    case CMDClient.CMD_TIME_START_TAIXIU:
                        listenner.onTimestartTaixiu(message);
                        break;
                    case CMDClient.CMD_AUTO_START_TAIXIU:
                        listenner.onAutostartTaixiu(message);
                        break;
                    case CMDClient.CMD_GAMEOVER_TAIXIU:
                        listenner.onGameoverTaixiu(message);
                        break;
                    case CMDClient.CMD_CUOC_TAIXIU:
                        listenner.onCuocTaiXiu(message);
                        break;
                    case CMDClient.CMD_INFO_TAIXIU:
                        listenner.oninfoTaiXiu(message);
                        break;
                    case CMDClient.CMD_XEM_LS_THEO_PHIEN:
                        listenner.oninfoLSTheoPhienTaiXiu(message);
                        break;
                    case CMDClient.CMD_EXIT_TAIXIU:
                        listenner.onExitTaiXiu(message);
                        break;
                    //end tai xiu
                    case CMDClient.CMD_UP_VIP:
                        break;
                    default:
                        if (secondHandler != null) {
                            secondHandler.processMessage(message);
                        }
                        break;
                }
            });
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }

    public override void onConnectionFail() {
        throw new System.NotImplementedException();
    }

    public override void onDisconnected() {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            listenner.onDisConnect();
        });

    }

    public override void onConnectOk() {
        Debug.Log("Connect OK...");
    }

    private static ProcessHandler instance;
    int send = 0;
    static int step;
    private static IChatListener listenner;

    public ProcessHandler() {

    }

    public static ProcessHandler getInstance() {
        if (instance == null) {
            instance = new ProcessHandler();
        }

        return instance;
    }

    public static void setListenner(ListernerServer listener) {
        listenner = listener;
    }

    public static void setSecondHandler(MessageHandler handler) {
        secondHandler = null;
        secondHandler = handler;
    }

    private static MessageHandler secondHandler;
}
