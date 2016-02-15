using UnityEngine;
using System.Collections;

public class TargetInfo : Packet {

    static int curhealth, maxhealth, level, expgive, damage;

    public TargetInfo(int _curhealth, int _maxhealth, int _level, int _expgive, int _damage)
    {
        curhealth = _curhealth;
        maxhealth = _maxhealth;
        level = _level;
        expgive = _expgive;
        damage = _damage;
    }

	public JSONObject buildPacket()
    {
        JSONObject targetInfo = new JSONObject();
        targetInfo.AddField("curhealth", curhealth);
        targetInfo.AddField("tothealth", maxhealth);
        targetInfo.AddField("level", level);
        targetInfo.AddField("damage", damage);
        targetInfo.AddField("expgive", expgive);
        return targetInfo;
    }
}
