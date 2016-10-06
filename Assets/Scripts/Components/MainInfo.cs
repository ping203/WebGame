using UnityEngine;
using System.Collections;

public class MainInfo {
    public string nick = "";
    public long userid = 0;
    public string fullname = "";
    public string displayname = "";
    public int gender = 0;
    public string birthday = "";
    public string address = "";
    public string status = "";
    public string email = "";
    public string cmnd = "";
    public string phoneNumber = "";
    public string link_Avatar = "";
    public int idAvata = 0;

    public long exp = 0;
    public long score_vip;
    public long total_money_charging = 0;
    public long total_time_play = 0;

    /// <summary>
    /// Tien free
    /// </summary>
    public long moneyFree = -1;
    /// <summary>
    /// tien vip
    /// </summary>
    public long moneyVip = 0;
    public int level = 1;
    public string[] cardName;
    public int[][] score;
    public int[] listAvatar;
    public string[] listNameAvatar;
    // public int idAvatar = -1;
    public long timeRuongServer = 0;

    // ----- new
    public string soLanThang;
    public string soLanThua;
    public long moneyVipMax = 0;
    public long moneyFreeMax = 0;
    public int soGDThanhCong;
    public string LanDangNhapCuoi;
    public int level_vip = 0;
    public sbyte isVIP;
}
