using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class BaseInfo {
    private static BaseInfo instance;
    public static BaseInfo gI() {
        if (instance == null) {
            instance = new BaseInfo();

        }
        return instance;
    }
    public string txt_phoneNumber = "0984.40.84.84";
    public string txt_phoneNumber2 = "0936.88.84.84";
    public string txt_emailDoithuong = "hotro.bm";
    public string txt_emailNaptien = "bmw.hotro@gmail.com";
    public string txt_fanpage = "";
    public string id_fanpage = "";
    public bool isView = false;
    public bool regOuTable = false;
    public string pass = "";
    public string username = "";

    public string SMS_CHANGE_PASS_SYNTAX;
    public string SMS_CHANGE_PASS_NUMBER;
    public sbyte isDoiThuong = 0;// disable, 1: enable

    public int idRoom = 1;
    public string nameTale;

    public MainInfo mainInfo = new MainInfo();

    protected static string strMoney = "";
    public int choinhanh = 0;//0:choi binh thuong, 1:choi nhanh
    public long moneyNeedTable;
    public int numberPlayer = 4;
    public short idTable;
    public long moneyTable, moneyMinTo;
    public string moneyName = "";
    public long betMoney;
    public float timerTurnTable = 30;
    public long needMoney = 0;
    public long maxMoney = 0;

    public List<InfoWin> infoWin = new List<InfoWin>();
    public List<AlertMess> msgAlert = new List<AlertMess>();
    public int soTinNhan = 0;

    public String syntax10, syntax15;
    public String port10 = "", port15 = "";
    public int sms10 = 0, sms15 = 0;
    public int tyle_xu_sang_chip = 0, tyle_chip_sang_xu = 0;
    public List<TyGia> list_tygia = new List<TyGia>();
    public int isCharging;

    public bool tuDongRutTien = false;
    public long soTienRut;

    public long currentMaxMoney;
    public long currentMinMoney;
    public long moneyto;

    public List<InfoWinTo> infoWinTo;
    public bool isHideTabeFull = true;
    public bool nhacnen = true;
    public bool rung = true;
    public bool isNhanLoiMoiChoi = true;
    public bool isAutoReady = false;
    public int typetableLogin = Res.ROOMVIP;
    public bool isFirstJoinTable = false;
    public bool isFirstLogin = false;

    public int soDu = 50000;
    public bool isPurchase = false;
    public int isHidexuchip;

    //Khuyen mai.
    public string khuyenMai;
    public int phanTram;
    public bool isVIP = false;

    public List<long> listBetMoneysVIP = new List<long>();
    public List<long> listBetMoneysFREE = new List<long>();

    public int type_sort = 0;
    public bool sort_giam_dan_bancuoc, sort_giam_dan_muccuoc, sort_giam_dan_nguoichoi;

    public bool isSound = PlayerPrefs.GetInt("sound") == 0 ? true : false;
    public bool isVibrate = PlayerPrefs.GetInt("rung") == 0 ? true : false;

    public int TELCO_CODE = 1;

    public int typePhongSelected = 1;//Default = 1

    public static string formatMoney(long money) {
        try {
            if (money < 0) {
                money = 0;
            }
            // strMoney.delete(0, strMoney.length());
            long strm = (long)(money / 1000000);
            long strk = 0;
            long strh = 0;
            if (strm > 0) {
                strk = (long)((money % 1000000) / 1000);
                if (strk > 100) {
                    strMoney = strm + "," + strk + "M";
                } else if (strMoney.Length > 0) {
                    strMoney = strm + "," + "0" + strk + "M";
                }

            } else {
                strk = (long)(money / 1000);
                if (strk > 0) {
                    strh = (money % 1000 / 100);
                    if (strh > 0) {
                        strMoney = strk + "," + strh + "K";
                    } else if (strMoney.Length >= 0) {
                        strMoney = strk + "K";
                    }

                } else if (strMoney.Length >= 0) {
                    strMoney = money + "";
                }
            }
        } catch (Exception e) {
            Debug.LogException(e);

        }
        return strMoney.ToString();
    }

    public static string formatMoneyNormal(long m) {
        //return m.ToString ("###.###");
        string str = m + "";// = m.ToString("000,000");//String.Format ("{0: 000.000}", m).ToString ();

        if (m <= 0) {
            str = "0";
        } else if (m < 100000 && m > 0) {
            str = m.ToString("#,#");
        } else if (m < 1000000) {
            str = (m / 1000).ToString("#,#K");
        } else if (m >= 1000000) {
            str = (m / 1000000).ToString("#,#M");
        }
        return str;
    }
    /*
    public static string formatMoneyNormal2(long money) {
        //try {
        if (money < 0) {
            money = 0;
        }

        string str = money + "";
        string s = str;
        if (money < 1000000 && money > 0) {
            if (str.Length >= 4)
                s = str.Substring(0, str.Length - 3) + "," + str.Substring(str.Length - 3, 3);
            else
                s = str.Substring(str.Length - 3, 3);
        } else if (money >= 1000000 && money < 100000000) {
            str = money / 1000 + "";
            if (str.Length >= 4)
                s = str.Substring(0, str.Length - 3) + "," + str.Substring(str.Length - 3, 3) + "K";
            else
                s = str.Substring(str.Length - 3, 3) + "K";
        } else if (money >= 100000000) {
            str = (money / 1000000) + "";
            if (str.Length >= 4)
                s = str.Substring(0, str.Length - 3) + "," + str.Substring(str.Length - 3, 3) + "M";
            else
                s = str.Substring(str.Length - 3, 3) + "M";
        }
        // strMoney.delete(0, strMoney.length());
        /*long strm = (long) (money / 1000000);
        long strk = 0;
        long strh = 0;
        if(strm > 0) {
            strk = (long) ((money % 1000000) / 1000);
            if(strk > 100) {
                strMoney = strm + "." + strk + "K";
            } else if(strMoney.Length > 0) {
                strMoney = strm + "." + "0" + strk + "K";
            }

        } else {
            strk = (long) (money / 1000);
            if(strk > 0) {
                strh = (money % 1000);
                if(strh > 0) {
                    if(strh > 100)
                        strMoney = strk + "." + strh + "";
                    else if(strh > 10)
                        strMoney = strk + ".0"+ strh + "";
                    else
                        strMoney = strk + ".00" + strh + "";
                } else if(strh == 0) {
                    strMoney = strk + "." + "000";
                } else if(strMoney.Length >= 0) {
                    strMoney = strk + "";
                }

            } else if(strMoney.Length >= 0) {
                strMoney = money + "";
            }
        }*

        return s.ToString();
        /*} catch(Exception e) {
            Debug.LogException (e);
        }*
    }
*/
    public static string formatMoneyDetail(long money) {
        if (money < 0) {
            money = 0;
        }
        String st = "";
        String rs = "";
        st = money + "";
        for (int i = 0; i < st.Length; i++) {
            rs = rs + st[(st.Length - i - 1)];
            if ((i + 1) % 3 == 0 && i < st.Length - 1) {
                rs = rs + ",";
            }
        }
        st = "";
        for (int i = 0; i < rs.Length; i++) {
            st = st + rs[(rs.Length - i - 1)];
        }
        return st;

    }

    public static string formatMoneyDetailDot(long money) {
        if (money < 0) {
            money = 0;
        }
        String st = "";
        String rs = "";
        st = money + "";
        for (int i = 0; i < st.Length; i++) {
            rs = rs + st[(st.Length - i - 1)];
            if ((i + 1) % 3 == 0 && i < st.Length - 1) {
                rs = rs + ".";
            }
        }
        st = "";
        for (int i = 0; i < rs.Length; i++) {
            st = st + rs[(rs.Length - i - 1)];
        }
        return st;

    }


    public bool isHaPhom { get; set; }

    public bool checkNumber(string test) {
        for (int i = 0; i < test.Length; i++) {
            char c = test[i];
            if ((c < '0') || (c > '9')) {
                return false;
            }
        }
        return true;
    }

    public bool checkMail(string mail) {
        if (!mail.Contains("@")) {
            return false;
        }
        return true;
    }

    private static int[] sortValue(int[] arr) {// mang cac so thu tu quan bai tu
        // 0-51
        int[] turn = arr;
        int length = turn.Length;
        for (int i = 0; i < length - 1; i++) {
            int min = i;
            for (int j = i + 1; j < length; j++) {
                if (((getValue(turn[j]) < getValue(turn[min])) || getValue(turn[min]) == 1) && getValue(turn[j]) != 1) {
                    // swap
                    min = j;
                }
            }
            int temp = turn[i];
            turn[i] = turn[min];
            turn[min] = temp;
        }
        return turn;
    }

    public static string tinhDiem(int[] cardhand) {
        cardhand = sortValue(cardhand);
        if (isSap(cardhand)) {
            return "Sáp";
        } else if (isLieng(cardhand)) {
            return "Liêng";
        } else if (isHinh(cardhand)) {
            return "Ảnh";
        } else if (getScoreFinal(cardhand) >= 0) {
            return getScoreFinal(cardhand) % 10 + " điểm";
        } else {
            return "";
        }
    }
    /// <summary>
    /// 0- 9 diem, 1 - anh, 2 - lieng, 3 sap
    /// </summary>
    public static string tinhDiemNew(int[] cardhand) {
        cardhand = sortValue(cardhand);
        if (isSap(cardhand)) {
            return "sap";// "Sáp";
        } else if (isLieng(cardhand)) {
            return "lieng";
        } else if (isHinh(cardhand)) {
            return "anh";
        } else if (getScoreFinal(cardhand) == 9) {
            return "9diem";
        } else {
            return "";
        }
    }

    private static bool isHinh(int[] cardhand) {
        if (cardhand == null || cardhand.Length < 3) {
            return false;
        }
        for (int i = 0; i < cardhand.Length; i++) {
            if (getValue(cardhand[i]) < 11) {
                return false;
            }
        }
        return true;
    }

    private static bool isLieng(int[] cardhand) {
        if (cardhand == null) {
            return false;
        }
        if (cardhand.Length < 3) {
            return false;
        }

        if (getValue(cardhand[0]) == 2 && getValue(cardhand[1]) == 3 && getValue(cardhand[2]) == 1) {
            return true;
        }

        if (getValue(cardhand[0]) == 12 && getValue(cardhand[1]) == 13 && getValue(cardhand[2]) == 1) {
            return true;
        }

        for (int i = 0; i < cardhand.Length - 1; i++) {
            int value1 = getValue(cardhand[i]);
            int value2 = getValue(cardhand[i + 1]);
            if ((Math.Abs(value2 - value1) > 1) || (value2 == value1)) {
                return false;
            }

        }
        return true;

    }


    public static int getScoreFinal(int[] src) {
        if (src == null || src.Length < 3) {
            return -1;
        }
        int sc = 0;
        if (isSap(src)) {
            sc = 100;
        } else {
            for (int i = 0; i < src.Length; i++) {
                sc += (getValue(src[i]) > 10 ? 0 : getValue(src[i]));
            }
        }
        return sc;
    }

    private static int getValue(int i) {
        return i % 13 + 1;
    }

    private static bool isSap(int[] cardhand) {
        if (cardhand == null || cardhand.Length < 3) {
            return false;
        }
        for (int i = 0; i < cardhand.Length; i++) {
            if (getValue(cardhand[i]) != getValue(cardhand[0])
                    || cardhand[i] == 52) {
                return false;
            }
        }
        return true;
    }
    public bool checkHettien() {
        if (mainInfo.moneyFree < needMoney) {
            return true;
        }
        return false;
    }
}
