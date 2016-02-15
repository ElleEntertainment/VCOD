using UnityEngine;
using System.Collections;

public class PlayerText : Packet {

    int health, maxhealth, damage, level, exp, expToNextLevel;
	public PlayerText(int _health, int _maxhealth, int _damage, int _level, int _exp, int _expToNextLevel)
    {
        health = _health;
        maxhealth = _maxhealth;
        damage = _damage;
        level = _level;
        exp = _exp;
        expToNextLevel = _expToNextLevel;
    }

    public JSONObject buildPacket()
    {
        JSONObject playerTextMessage = new JSONObject();
        playerTextMessage.AddField("health", health);
        playerTextMessage.AddField("maxhealth", maxhealth);
        playerTextMessage.AddField("damage", damage);
        playerTextMessage.AddField("level", level);
        playerTextMessage.AddField("exp", exp);
        playerTextMessage.AddField("exptonextlevel", expToNextLevel);
        return playerTextMessage;
    }
}
