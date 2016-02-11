using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    //inizializzazione variabili
    public Text_Manager TM;
    float speed = 1;
    bool jump = false;
    bool falling = false;
    bool run = true;
    float YPos = 0;
    float jumpHeigth = 2;
    float attackTime = 1.0F;
    int health;
    int maxHealth;
    string damageRec = "";
    bool attackRec = false;
    Infetto currentTarget = null;
    bool wasDead = false;
    bool isAttacking;
    Vector3 spawnPos;
    int level = 1;
    int exp;
    int expToNextLevel;
    long tempo_attacco;
    long tempo_ora_regen_health;
    long t;
    Vector3 mousePosition;
    GameObject camera;
    bool isTargetting;
    GameObject armacamera;
    string query = "";
    float armacamera_p_x, armacamera_p_y, armacamera_p_z, armacamera_o_x, armacamera_o_y, armacamera_o_z, camera_p_x, camera_p_y, camera_p_z, camera_o_x, camera_o_y, camera_o_z; 
    //--------------------------------


    // Use this for initialization
    void Start()
    {
        jump = false;
        run = true;
        isAttacking = false;
        tempo_attacco = UnixTimeNow();
        tempo_ora_regen_health = UnixTimeNow();
        t = UnixTimeNow();
        mousePosition = Input.mousePosition;
        load("player");
        
        isTargetting = false;

        camera = GameObject.FindGameObjectWithTag("MainCamera");
        armacamera = GameObject.FindGameObjectWithTag("ArmaCamera");
        armacamera.SetActive(false);
        armacamera_p_x = armacamera.transform.position.x;
        armacamera_p_y = armacamera.transform.position.y;
        armacamera_p_z = armacamera.transform.position.z;
        armacamera_o_x = armacamera.transform.rotation.x;
        armacamera_o_y = armacamera.transform.rotation.y;
        armacamera_o_z = armacamera.transform.rotation.z;

        camera_p_x = camera.transform.position.x;
        camera_p_y = camera.transform.position.y;
        camera_p_z = camera.transform.position.z;
        camera_o_x = camera.transform.rotation.x;
        camera_o_y = camera.transform.rotation.y;
        camera_o_z = camera.transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {

        //Sistema di Rotazione (non ce n'è bisogno per ora)
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -1); //-1 sono i gradi di rotazione
        else
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(Vector3.up, 1);


        // FINE SISTEMA DI ROTAZIONE


        //Usiamo i tasti per muoverci
        if (Input.GetKey(KeyCode.K)) //Tasto per debuggare
        {
            long tempo_now = UnixTimeNow();
            long t = tempo_now - tempo_attacco;
        }

        //tasto per spawnare npc
        if (Input.GetKey(KeyCode.U))
        {
            insertNpc();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!isTargetting)
            {
                camera.transform.position = new Vector3(armacamera_p_x, armacamera_p_y, armacamera_p_z);
                camera.transform.rotation = new Quaternion(armacamera_o_x, armacamera_o_y, armacamera_o_z, 0);
                isTargetting = true;
                Debug.Log("Camera spostata sull'arma");
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (isTargetting)
            {
                camera.transform.position = new Vector3(camera_p_x, camera_p_y, camera_p_z);
                camera.transform.rotation = new Quaternion(camera_o_x, camera_o_y, camera_o_z, 0);
                isTargetting = false;
                Debug.Log("Camera spostata sul personaggio");
            }
        }
        

        //Fine sistema di movimento semplice
        /*
        if(Input.GetMouseButtonDown(0)){
            if(currentTarget!=null){
                currentTarget.stopParticle();
                currentTarget = null;
                TM.setTargetTrue(false);
            }
        }
        */
        if (Input.GetKeyDown(KeyCode.L))
        {
            save();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            load("player");
        }

        //andare nel menù
        if (Input.GetKey(KeyCode.Escape))
        {
            DbManager.setInstance();
            query = "INSERT INTO player(id, name, level, exp, expToNextLvl, health, maxhealth, position_x, position_y, position_z, orientation_x, orientation_y, orientation_z, savetype) VALUES(2, 'player', " + level + ", " + exp + ", " + expToNextLevel + ", " + health + ", " + maxHealth + ", " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ", " + transform.rotation.x + ", " + transform.rotation.y + ", " + transform.rotation.z + ", 1);";
            Application.LoadLevel("menu");
        }

        //Check morte del player
        if (health <= 0)
        {
            Debug.Log("Player morto.");
            //Respawna il player
            transform.position = spawnPos;
            //Resetta la vita
            health = maxHealth;
            wasDead = true;
            TM.SendMessage("playerText", health + "-" + maxHealth + "-" + " " + "-" + level + "-" + exp + "-" + expToNextLevel);
        }

        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= 2)
            {
                isAttacking = true;
                attackEnemy(currentTarget);
            }
            else
                isAttacking = false;
        }
        else
            isAttacking = false;

        if (currentTarget != null && !isAttacking)
        {
            TM.setTargetTrue(true);
            string targetInfo = currentTarget.getHealth() + "-" + currentTarget.getMaxHealth() + "-" + " " + "-" + currentTarget.getLevel() + "-";
            TM.SendMessage("targetText", targetInfo);
        }
        if (currentTarget != null)
        {
            if (currentTarget.isDead())
            {
                currentTarget = null;
                TM.setTargetTrue(false);
            }
        }

        if (currentTarget == null && !isAttacking)
        {

            /*
             * ********************************************
             * Health regeneration System
             * ********************************************/
            /*tempo_ora_regen_health = UnixTimeNow(); //registro quando sono uscito fuori dal combat
            
            if ((tempo_ora_regen_health - t) >= 5)
            {
                int max_health_player = getMaxHealthPlayer();
                if (health < max_health_player)
                    health = health + (max_health_player / 100) * 3; //+3% ogni 5 secondi (in questo caso +7.5)
                if (health >= max_health_player)
                    health = max_health_player;
                TM.SendMessage("playerText", health + "-" + maxHealth + "-" + " " + "-" + level + "-" + exp + "-" + expToNextLevel);
                t = UnixTimeNow();
                //Debug.Log("Doing Health regeneration");
                /*if (health == max_health_player)
                    Debug.Log("Health regeneration completed");
            }*/
        }


    }

    void attackEnemy(Infetto target)
    {
        if (attackTime - Time.deltaTime <= 0)
        {
            int dam = UnityEngine.Random.Range(10, 25);
            target.SendMessage("applyDamage", dam);
            string targetInfo = currentTarget.getHealth() + "-" + currentTarget.getMaxHealth() + "-" + dam + "-" + currentTarget.getLevel() + "-";
            TM.SendMessage("targetText", targetInfo);
            attackTime = 1.5F;
        }
        else
            attackTime -= Time.deltaTime;
    }

    void applyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
        damageRec = "<b>" + damage + "</b>";
        attackRec = true;
        TM.SendMessage("playerText", health + "-" + maxHealth + "-" + damage + "-" + level + "-" + exp + "-" + expToNextLevel);
        Debug.Log("Damage Received " + damage);
    }

    public int getHealthPlayer()
    {
        return health;
    }
    public int getMaxHealthPlayer()
    {
        return maxHealth;
    }
    public int getLevel()
    {
        return level;
    }

    void updateHealth(int healthToAdd)
    {
        int currentHealth = health;
        if (currentHealth < maxHealth)
        {
            if ((maxHealth - currentHealth) < 10)
                health = maxHealth;
            else
            {
                health = currentHealth + healthToAdd;
            }

        }
        TM.SendMessage("playerText", health + "-" + maxHealth + "-" + "" + "-" + level + "-" + exp + "-" + expToNextLevel);

    }

    void updateExp()
    {
        DbManager.setInstance();
        query = "UPDATE player SET exp = "+exp+", expToNextLvl = "+expToNextLevel+", level = "+level+" WHERE name = 'player';";
        DbManager.executeQuery(query);
    }
    void setExp(int experience)
    {
        exp = exp + experience;
        if (exp >= expToNextLevel)
        {
            level++;
            exp = 0;
            expToNextLevel = Mathf.RoundToInt((level * 150) * 1F);
        }
        string targetInfo = " - - - -" + experience;
        TM.SendMessage("targetText", targetInfo);
        TM.SendMessage("playerText", health + "-" + maxHealth + "-" + "" + "-" + level + "-" + exp + "-" + expToNextLevel);
    }

    public Infetto getTarget()
    {
        if (currentTarget != null)
            return currentTarget;
        else
        {
            return null;
        }
    }

    void insertNpc()
    {
        float pos_x = transform.position.x;
        float pos_y = transform.position.y;
        float pos_z = transform.position.z;
        float ori_x = transform.rotation.x;
        float ori_y = transform.rotation.y;
        float ori_z = transform.rotation.z;
        DbManager.setInstance();
        string npc = "INSERT INTO nemici_info(position_x, position_y, position_z, orientation_x, orientation_y, orientation_z) VALUES(" + pos_x + ", " + pos_y + ", " + pos_z + ", " + ori_x + ", " + ori_y + ", " + ori_z + ");";
        DbManager.executeQuery(npc);
        Debug.Log("npc aggiunto nel db");
    }
    void save()
    {
        DbManager.setInstance();
        string myData = "INSERT OR REPLACE INTO player VALUES(1, 'player', " + level + ", " + exp + ", " + health + ", " + maxHealth + ", " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ", " + transform.rotation.x + ", " + transform.rotation.y + ", " + transform.rotation.z + ", 0);";
        DbManager.executeQuery(myData);
    }
    public void load(string s)
    {
        DbManager.setInstance();
        JSONObject myData = DbManager.loadPlayer(s);
        Debug.Log(myData);
        if (myData.Count > 0)
        {
            /*char[] del = { '|' };
            string[] values = myData.Split(del);*/
            level = Convert.ToInt32(myData.GetField("level"));
            exp = Convert.ToInt32(myData.GetField("exp"));
            expToNextLevel = Convert.ToInt32(myData.GetField("expToNextLvl"));
            health = Convert.ToInt32(myData.GetField("health"));
            maxHealth = Convert.ToInt32(myData.GetField("maxhealth"));
            spawnPos.x = (float)Convert.ToDouble(myData.GetField("position_x"));
            spawnPos.y = (float)Convert.ToDouble(myData.GetField("position_y"));
            spawnPos.z = (float)Convert.ToDouble(myData.GetField("position_z"));
            transform.position = spawnPos;
            transform.Rotate((float)Convert.ToDouble(myData.GetField("orientation_x")), (float)Convert.ToDouble(myData.GetField("orientation_y")), (float)Convert.ToDouble(myData.GetField("orientation_z")));
            TM.SendMessage("playerText", health + "-" + maxHealth + "-" + "" + "-" + level + "-" + exp + "-" + expToNextLevel);
        }
    }
    public long UnixTimeNow()
    {
        var timeSpan = (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0));
        return (long)timeSpan.TotalSeconds;
    }

    public Player getPlayer()
    {
        return this;
    }
}
