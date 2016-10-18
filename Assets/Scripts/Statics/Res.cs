using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Res {
    public static string version = "1.3.4";
    //public static string IP = "choibaidoithuong.org";
    public static string IP = "52.74.154.183";
    public static int PORT = 4326;
    //public static string IP = "101.99.3.131";
    //public static string IP = "192.168.1.120";
    //public static int PORT = 4323;
    //public static int PORT = 9999;
    public static string TXT_PhoneNumber = "0999999999";

    public static int ROOMFREE = 1;
    public static int ROOMVIP = 2;
    //---------------string 
    public static string MONEY_FREE = " Chip";
    public static string MONEY_VIP = " Xu";
    public static string MONEY_FREE_UPPERCASE = " Chip";
    public static string MONEY_VIP_UPPERCASE = " XU";
    public static string TXT_SANSANG = "Sẵn sàng";
    public static string TXT_BOSANSANG = "Bỏ sẵn sàng";
    public static string TXT_BATDAU = "Bắt đầu";

    public static string TXT_DAT = "Đặt";
    public static string TXT_FOLD = "Bỏ";
    public static string TXT_CHECK_FOLD = "Xem bài/Bỏ";
    public static string TXT_CHECK = "Xem bài";
    public static string TXT_CALL = "Theo";
    public static string TXT_CALL_ANY = "Theo mọi cược";
    public static string TXT_RAISE = "Tố";
    public static string TXT_ALLIN = "Tất tay";

    public static string TXT_BOLUOT = "Bỏ lượt";
    public static string TXT_DONGY = "Đồng ý";

    public static string TXT_DOILUAT = "Đổi luật";
    public static string TXT_XINCHO = "Xin chờ...";
    public static string TXT_DANH = "Đánh";
    public static string TXT_BOC = "Bốc";
    public static string TXT_AN = "Ăn";
    public static string TXT_HA = "Hạ Phỏm";

    public static string TXT_RUTTIEN = "Rút lại $";
    public static string TXT_CHONBAI = "Chọn 1 quân bài để mở: ";

    public static int AC_XEMBAI = 0;
    public static int AC_BOLUOT = 1;
    public static int AC_THEO = 2;
    public static int AC_UPBO = 3;
    public static int AC_TO = 4;

    public static float speedCard = 0.15f;

    public static string[] TypeCard_Name = new string[] { "mauthau", "doi", "thu", "samco", "sanh", "thung", "culu", "tuquy", "thungphasanh" };
    public static string[] TYPECARD = { "Mậu thầu", "Đôi", "Thú", "Sám cô", "Sảnh", "Thùng", "Cù lũ", "Tứ quý", "Thùng phá sảnh" };

   // public static Sprite[] list_avata = new Sprite[60];// = new List<Sprite>();
    public static Sprite[] list_cards;// = new List<Sprite>();
    //public static Sprite[] list_emotions;// = new List<Sprite>();
    public const int EMOTION_COUNT = 28;
    public const int AVATA_COUNT = 60;

    /*public static Sprite getAvataByID(int id) {
        for (int i = 0; i < list_avata.Length; i++) {
            if (id == int.Parse(list_avata[i].name)) {
                return list_avata[i];
            }
        }
        return null;
    }*/

    public static Sprite getCardByID(int id) {
        string str = "cardall_" + id;
        for (int i = 0; i < list_cards.Length; i++) {
            if (str.Trim().Equals(list_cards[i].name.Trim())) {
                return list_cards[i];
            }
        }
        return null;
    }

    public const string AS_AVATA = "avata";
    public const string AS_MAINSCENE = "mainscene";
    public const string AS_UI = "ui";


    #region SUBSCENES
    public const string AS_SUBSCENES = "subscenes";
    public const string AS_SUBSCENES_SETTING = "sub_setting";
    public const string AS_SUBSCENES_RANK = "sub_rank";
    public const string AS_SUBSCENES_PLAYER_INFO = "sub_player_info";
    public const string AS_SUBSCENES_CHANGE_AVATA = "sub_change_avata";
    public const string AS_SUBSCENES_CHANGE_NAME = "sub_change_name";
    public const string AS_SUBSCENES_CHANGE_PASS = "sub_change_pass";
    public const string AS_SUBSCENES_HELP = "sub_help";
    public const string AS_SUBSCENES_ADD_COIN = "sub_add_coin";
    public const string AS_SUBSCENES_MAIL = "sub_mail";
    public const string AS_SUBSCENES_CREATE_ROOM = "sub_create_room";
    public const string AS_SUBSCENES_GOTO_ROOM = "sub_goto_room";
    /// <summary>
    /// Scene doi thuong
    /// </summary>
    public const string AS_SUBSCENES_EXCHANGE = "sub_exchange";
    public const string AS_SUBSCENES_INVITE_GAME = "sub_invite_game";
    #endregion END SUBSCENES

    #region PREFABS
    public const string AS_PREFABS = "prefabs";
    public const string AS_PREFABS_ITEM_RANK = "ItemRank";
    public const string AS_PREFABS_BUTTON_AVATA = "Button_Avata";
    public const string AS_PREFABS_ITEM_MAIL = "ItemMail";
    public const string AS_PREFABS_BUTTON_GIFT = "Button_Gift";
    public const string AS_PREFABS_INVITE_GAME = "ItemMoiChoi";
    #endregion END PREFABS
}

public enum Align {
    None,
    Left,
    Center,
    Right,
    Top,
    Bot
};

