using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MySql.Data.MySqlClient;

namespace VCOD_Server.Database
{
	class Player
	{
		public int id = 0; //Comment: 
		int OLD_id = 0;

        public string name = null; //Comment: 
		string OLD_name = null;

        public int level = 0; //Comment: 
		int OLD_level = 0;

        public int exp = 0; //Comment: 
		int OLD_exp = 0;

        public int expToNextLvl = 0; //Comment: 
		int OLD_expToNextLvl = 0;

        public int health = 0; //Comment: 
		int OLD_health = 0;

        public int maxhealth = 0; //Comment: 
		int OLD_maxhealth = 0;

        public float position_x = 0.0f; //Comment: 
		float OLD_position_x = 0.0f;

        public float position_y = 0.0f; //Comment: 
		float OLD_position_y = 0.0f;

        public float position_z = 0.0f; //Comment: 
		float OLD_position_z = 0.0f;

        public float orientation_x = 0.0f; //Comment: 
		float OLD_orientation_x = 0.0f;

        public float orientation_y = 0.0f; //Comment: 
		float OLD_orientation_y = 0.0f;

        public float orientation_z = 0.0f; //Comment: 
		float OLD_orientation_z = 0.0f;

        public int savetype = 0; //Comment: 
		int OLD_savetype = 0;

		MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=vcod;password=test;");

		public Player(int _id, string _name, int _level, int _exp, int _expToNextLvl, int _health, int _maxhealth, float _position_x, float _position_y, float _position_z, float _orientation_x, float _orientation_y, float _orientation_z, int _savetype)
		{
			id = _id;
			name = _name;
			level = _level;
			exp = _exp;
			expToNextLvl = _expToNextLvl;
			health = _health;
			maxhealth = _maxhealth;
			position_x = _position_x;
			position_y = _position_y;
			position_z = _position_z;
			orientation_x = _orientation_x;
			orientation_y = _orientation_y;
			orientation_z = _orientation_z;
			savetype = _savetype;
			updateOldValues();
		}

        public Player(string name)
        {
            conn.Open();
            string sql = "SELECT id FROM player WHERE savetype = 1;";
            MySqlCommand com = new MySqlCommand(sql, conn);
            MySqlDataReader l = com.ExecuteReader();
            int c = 0;
            while (l.Read())
            {
                c++;
            }
            l.Close();
            string result = "";
            if (c > 0)
            {
                //il player torna dal menù
                sql = "SELECT id, level, exp, expToNextLvl, health, maxhealth, position_x, position_y, position_z, orientation_x, orientation_y, orientation_z FROM player WHERE name='" + name + "' AND savetype = 1;";
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comm.ExecuteReader();

                if (reader.Read())
                {
                    id = Convert.ToInt16(reader["id"]);
                    level = Convert.ToInt16(reader["level"]);
                    exp = Convert.ToInt16(reader["exp"]);
                    expToNextLvl = Convert.ToInt16(reader["expToNextLvl"]);
                    health = Convert.ToInt16(reader["health"]);
                    maxhealth = Convert.ToInt16(reader["maxhealth"]);
                    position_x = (float)Convert.ToDouble(reader["position_x"]);
                    position_y = (float)Convert.ToDouble(reader["position_y"]);
                    position_z = (float)Convert.ToDouble(reader["position_z"]);
                    orientation_x = (float)Convert.ToDouble(reader["orientation_x"]);
                    orientation_y = (float)Convert.ToDouble(reader["orientation_y"]);
                    orientation_z = (float)Convert.ToDouble(reader["orientation_z"]);
                }
                reader.Close();
                //cancello il salvataggio perchè non mi serve più.
                MySqlCommand cancella = new MySqlCommand("DELETE FROM player WHERE savetype = 1;", conn);
                cancella.ExecuteNonQuery();
            }
            else
            {
                sql = "SELECT id, level, exp, expToNextLvl, health, maxhealth, position_x, position_y, position_z, orientation_x, orientation_y, orientation_z FROM player WHERE name='" + name + "' AND savetype = 0;";
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comm.ExecuteReader();

                if (reader.Read())
                {
                    id = Convert.ToInt16(reader["id"]);
                    level = Convert.ToInt16(reader["level"]);
                    exp = Convert.ToInt16(reader["exp"]);
                    expToNextLvl = Convert.ToInt16(reader["expToNextLvl"]);
                    health = Convert.ToInt16(reader["health"]);
                    maxhealth = Convert.ToInt16(reader["maxhealth"]);
                    position_x = (float)Convert.ToDouble(reader["position_x"]);
                    position_y = (float)Convert.ToDouble(reader["position_y"]);
                    position_z = (float)Convert.ToDouble(reader["position_z"]);
                    orientation_x = (float)Convert.ToDouble(reader["orientation_x"]);
                    orientation_y = (float)Convert.ToDouble(reader["orientation_y"]);
                    orientation_z = (float)Convert.ToDouble(reader["orientation_z"]);
                }
                reader.Close();
            }
            conn.Close();
            updateOldValues();
        }

		public void delete()
		{
			conn.Open();
			MySqlCommand cmd = new MySqlCommand("", conn);
			cmd.CommandText = "DELETE FROM player WHERE id = @id AND name = @name AND level = @level AND exp = @exp AND expToNextLvl = @expToNextLvl AND health = @health AND maxhealth = @maxhealth AND position_x = @position_x AND position_y = @position_y AND position_z = @position_z AND orientation_x = @orientation_x AND orientation_y = @orientation_y AND orientation_z = @orientation_z AND savetype = @savetype;";
			MySqlParameter idParameter = new MySqlParameter("@id", MySqlDbType.VarChar, 0);
			MySqlParameter nameParameter = new MySqlParameter("@name", MySqlDbType.VarChar, 0);
			MySqlParameter levelParameter = new MySqlParameter("@level", MySqlDbType.VarChar, 0);
			MySqlParameter expParameter = new MySqlParameter("@exp", MySqlDbType.VarChar, 0);
			MySqlParameter exptonextlvlParameter = new MySqlParameter("@expToNextLvl", MySqlDbType.VarChar, 0);
			MySqlParameter healthParameter = new MySqlParameter("@health", MySqlDbType.VarChar, 0);
			MySqlParameter maxhealthParameter = new MySqlParameter("@maxhealth", MySqlDbType.VarChar, 0);
			MySqlParameter position_xParameter = new MySqlParameter("@position_x", MySqlDbType.VarChar, 0);
			MySqlParameter position_yParameter = new MySqlParameter("@position_y", MySqlDbType.VarChar, 0);
			MySqlParameter position_zParameter = new MySqlParameter("@position_z", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_xParameter = new MySqlParameter("@orientation_x", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_yParameter = new MySqlParameter("@orientation_y", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_zParameter = new MySqlParameter("@orientation_z", MySqlDbType.VarChar, 0);
			MySqlParameter savetypeParameter = new MySqlParameter("@savetype", MySqlDbType.VarChar, 0);
			idParameter.Value = id;
			nameParameter.Value = name;
			levelParameter.Value = level;
			expParameter.Value = exp;
			exptonextlvlParameter.Value = expToNextLvl;
			healthParameter.Value = health;
			maxhealthParameter.Value = maxhealth;
			position_xParameter.Value = position_x;
			position_yParameter.Value = position_y;
			position_zParameter.Value = position_z;
			orientation_xParameter.Value = orientation_x;
			orientation_yParameter.Value = orientation_y;
			orientation_zParameter.Value = orientation_z;
			savetypeParameter.Value = savetype;
			cmd.Parameters.Add(idParameter);
			cmd.Parameters.Add(nameParameter);
			cmd.Parameters.Add(levelParameter);
			cmd.Parameters.Add(expParameter);
			cmd.Parameters.Add(exptonextlvlParameter);
			cmd.Parameters.Add(healthParameter);
			cmd.Parameters.Add(maxhealthParameter);
			cmd.Parameters.Add(position_xParameter);
			cmd.Parameters.Add(position_yParameter);
			cmd.Parameters.Add(position_zParameter);
			cmd.Parameters.Add(orientation_xParameter);
			cmd.Parameters.Add(orientation_yParameter);
			cmd.Parameters.Add(orientation_zParameter);
			cmd.Parameters.Add(savetypeParameter);
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		public void update()
		{
			conn.Open();
			MySqlCommand cmd = new MySqlCommand("", conn);
			cmd.CommandText = "UPDATE player SET id = @newid, name = @newname, level = @newlevel, exp = @newexp, expToNextLvl = @newexpToNextLvl, health = @newhealth, maxhealth = @newmaxhealth, position_x = @newposition_x, position_y = @newposition_y, position_z = @newposition_z, orientation_x = @neworientation_x, orientation_y = @neworientation_y, orientation_z = @neworientation_z, savetype = @newsavetype WHERE id = @id AND name = @name AND level = @level AND exp = @exp AND expToNextLvl = @expToNextLvl AND health = @health AND maxhealth = @maxhealth AND position_x = @position_x AND position_y = @position_y AND position_z = @position_z AND orientation_x = @orientation_x AND orientation_y = @orientation_y AND orientation_z = @orientation_z AND savetype = @savetype;";
			MySqlParameter OLD_idParameter = new MySqlParameter("@id", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_nameParameter = new MySqlParameter("@name", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_levelParameter = new MySqlParameter("@level", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_expParameter = new MySqlParameter("@exp", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_exptonextlvlParameter = new MySqlParameter("@expToNextLvl", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_healthParameter = new MySqlParameter("@health", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_maxhealthParameter = new MySqlParameter("@maxhealth", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_position_xParameter = new MySqlParameter("@position_x", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_position_yParameter = new MySqlParameter("@position_y", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_position_zParameter = new MySqlParameter("@position_z", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_orientation_xParameter = new MySqlParameter("@orientation_x", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_orientation_yParameter = new MySqlParameter("@orientation_y", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_orientation_zParameter = new MySqlParameter("@orientation_z", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_savetypeParameter = new MySqlParameter("@savetype", MySqlDbType.VarChar, 0);
			MySqlParameter idParameter = new MySqlParameter("@newid", MySqlDbType.VarChar, 0);
			MySqlParameter nameParameter = new MySqlParameter("@newname", MySqlDbType.VarChar, 0);
			MySqlParameter levelParameter = new MySqlParameter("@newlevel", MySqlDbType.VarChar, 0);
			MySqlParameter expParameter = new MySqlParameter("@newexp", MySqlDbType.VarChar, 0);
			MySqlParameter exptonextlvlParameter = new MySqlParameter("@newexpToNextLvl", MySqlDbType.VarChar, 0);
			MySqlParameter healthParameter = new MySqlParameter("@newhealth", MySqlDbType.VarChar, 0);
			MySqlParameter maxhealthParameter = new MySqlParameter("@newmaxhealth", MySqlDbType.VarChar, 0);
			MySqlParameter position_xParameter = new MySqlParameter("@newposition_x", MySqlDbType.VarChar, 0);
			MySqlParameter position_yParameter = new MySqlParameter("@newposition_y", MySqlDbType.VarChar, 0);
			MySqlParameter position_zParameter = new MySqlParameter("@newposition_z", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_xParameter = new MySqlParameter("@neworientation_x", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_yParameter = new MySqlParameter("@neworientation_y", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_zParameter = new MySqlParameter("@neworientation_z", MySqlDbType.VarChar, 0);
			MySqlParameter savetypeParameter = new MySqlParameter("@newsavetype", MySqlDbType.VarChar, 0);
			idParameter.Value = id;
			nameParameter.Value = name;
			levelParameter.Value = level;
			expParameter.Value = exp;
			exptonextlvlParameter.Value = expToNextLvl;
			healthParameter.Value = health;
			maxhealthParameter.Value = maxhealth;
			position_xParameter.Value = position_x;
			position_yParameter.Value = position_y;
			position_zParameter.Value = position_z;
			orientation_xParameter.Value = orientation_x;
			orientation_yParameter.Value = orientation_y;
			orientation_zParameter.Value = orientation_z;
			savetypeParameter.Value = savetype;
			OLD_idParameter.Value = OLD_id;
			OLD_nameParameter.Value = OLD_name;
			OLD_levelParameter.Value = OLD_level;
			OLD_expParameter.Value = OLD_exp;
			OLD_exptonextlvlParameter.Value = OLD_expToNextLvl;
			OLD_healthParameter.Value = OLD_health;
			OLD_maxhealthParameter.Value = OLD_maxhealth;
			OLD_position_xParameter.Value = OLD_position_x;
			OLD_position_yParameter.Value = OLD_position_y;
			OLD_position_zParameter.Value = OLD_position_z;
			OLD_orientation_xParameter.Value = OLD_orientation_x;
			OLD_orientation_yParameter.Value = OLD_orientation_y;
			OLD_orientation_zParameter.Value = OLD_orientation_z;
			OLD_savetypeParameter.Value = OLD_savetype;
			cmd.Parameters.Add(idParameter);
			cmd.Parameters.Add(nameParameter);
			cmd.Parameters.Add(levelParameter);
			cmd.Parameters.Add(expParameter);
			cmd.Parameters.Add(exptonextlvlParameter);
			cmd.Parameters.Add(healthParameter);
			cmd.Parameters.Add(maxhealthParameter);
			cmd.Parameters.Add(position_xParameter);
			cmd.Parameters.Add(position_yParameter);
			cmd.Parameters.Add(position_zParameter);
			cmd.Parameters.Add(orientation_xParameter);
			cmd.Parameters.Add(orientation_yParameter);
			cmd.Parameters.Add(orientation_zParameter);
			cmd.Parameters.Add(savetypeParameter);
			cmd.Parameters.Add(OLD_idParameter);
			cmd.Parameters.Add(OLD_nameParameter);
			cmd.Parameters.Add(OLD_levelParameter);
			cmd.Parameters.Add(OLD_expParameter);
			cmd.Parameters.Add(OLD_exptonextlvlParameter);
			cmd.Parameters.Add(OLD_healthParameter);
			cmd.Parameters.Add(OLD_maxhealthParameter);
			cmd.Parameters.Add(OLD_position_xParameter);
			cmd.Parameters.Add(OLD_position_yParameter);
			cmd.Parameters.Add(OLD_position_zParameter);
			cmd.Parameters.Add(OLD_orientation_xParameter);
			cmd.Parameters.Add(OLD_orientation_yParameter);
			cmd.Parameters.Add(OLD_orientation_zParameter);
			cmd.Parameters.Add(OLD_savetypeParameter);
			cmd.ExecuteNonQuery();
			conn.Close();
			updateOldValues();
		}

		private void updateOldValues()
		{
			OLD_id = id;
			OLD_name = name;
			OLD_level = level;
			OLD_exp = exp;
			OLD_expToNextLvl = expToNextLvl;
			OLD_health = health;
			OLD_maxhealth = maxhealth;
			OLD_position_x = position_x;
			OLD_position_y = position_y;
			OLD_position_z = position_z;
			OLD_orientation_x = orientation_x;
			OLD_orientation_y = orientation_y;
			OLD_orientation_z = orientation_z;
			OLD_savetype = savetype;
		}
	}
}
