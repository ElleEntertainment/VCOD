using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MySql.Data.MySqlClient;

namespace VCOD_Server.Database
{
	class Nemici_info
	{
		public int idSpawn = 0; //Comment: 
		int OLD_idSpawn = 0;

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

		static MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=vcod;password=test;");

        public static List<Nemici_info> GetAllNemiciInfo()
        {
            conn.Open();
            string query = "SELECT idSpawn, position_x, position_y, position_z, orientation_x, orientation_y, orientation_z FROM nemici_info;";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            string[] result = new string[6];
            int contatore = 0;
            List<Nemici_info> list = new List<Nemici_info>();
            while (reader.Read())
            {
                list.Add(new Nemici_info(Convert.ToInt16(reader["idSpawn"]),
                                        (float)Convert.ToDouble(reader["position_x"]),
                                        (float)Convert.ToDouble(reader["position_y"]),
                                        (float)Convert.ToDouble(reader["position_z"]),
                                        (float)Convert.ToDouble(reader["orientation_x"]),
                                        (float)Convert.ToDouble(reader["orientation_y"]),
                                        (float)Convert.ToDouble(reader["orientation_z"])));
                contatore++;
            }
            conn.Close();
            return list;
        }

		public Nemici_info(int _idSpawn, float _position_x, float _position_y, float _position_z, float _orientation_x, float _orientation_y, float _orientation_z)
		{
			idSpawn = _idSpawn;
			position_x = _position_x;
			position_y = _position_y;
			position_z = _position_z;
			orientation_x = _orientation_x;
			orientation_y = _orientation_y;
			orientation_z = _orientation_z;
			updateOldValues();
		}

		public void delete()
		{
			conn.Open();
			MySqlCommand cmd = new MySqlCommand("", conn);
			cmd.CommandText = "DELETE FROM nemici_info WHERE idSpawn = @idSpawn AND position_x = @position_x AND position_y = @position_y AND position_z = @position_z AND orientation_x = @orientation_x AND orientation_y = @orientation_y AND orientation_z = @orientation_z;";
			MySqlParameter idspawnParameter = new MySqlParameter("@idSpawn", MySqlDbType.VarChar, 0);
			MySqlParameter position_xParameter = new MySqlParameter("@position_x", MySqlDbType.VarChar, 0);
			MySqlParameter position_yParameter = new MySqlParameter("@position_y", MySqlDbType.VarChar, 0);
			MySqlParameter position_zParameter = new MySqlParameter("@position_z", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_xParameter = new MySqlParameter("@orientation_x", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_yParameter = new MySqlParameter("@orientation_y", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_zParameter = new MySqlParameter("@orientation_z", MySqlDbType.VarChar, 0);
			idspawnParameter.Value = idSpawn;
			position_xParameter.Value = position_x;
			position_yParameter.Value = position_y;
			position_zParameter.Value = position_z;
			orientation_xParameter.Value = orientation_x;
			orientation_yParameter.Value = orientation_y;
			orientation_zParameter.Value = orientation_z;
			cmd.Parameters.Add(idspawnParameter);
			cmd.Parameters.Add(position_xParameter);
			cmd.Parameters.Add(position_yParameter);
			cmd.Parameters.Add(position_zParameter);
			cmd.Parameters.Add(orientation_xParameter);
			cmd.Parameters.Add(orientation_yParameter);
			cmd.Parameters.Add(orientation_zParameter);
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		public void update()
		{
			conn.Open();
			MySqlCommand cmd = new MySqlCommand("", conn);
			cmd.CommandText = "UPDATE nemici_info SET idSpawn = @newidSpawn, position_x = @newposition_x, position_y = @newposition_y, position_z = @newposition_z, orientation_x = @neworientation_x, orientation_y = @neworientation_y, orientation_z = @neworientation_z WHERE idSpawn = @idSpawn AND position_x = @position_x AND position_y = @position_y AND position_z = @position_z AND orientation_x = @orientation_x AND orientation_y = @orientation_y AND orientation_z = @orientation_z;";
			MySqlParameter OLD_idspawnParameter = new MySqlParameter("@idSpawn", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_position_xParameter = new MySqlParameter("@position_x", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_position_yParameter = new MySqlParameter("@position_y", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_position_zParameter = new MySqlParameter("@position_z", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_orientation_xParameter = new MySqlParameter("@orientation_x", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_orientation_yParameter = new MySqlParameter("@orientation_y", MySqlDbType.VarChar, 0);
			MySqlParameter OLD_orientation_zParameter = new MySqlParameter("@orientation_z", MySqlDbType.VarChar, 0);
			MySqlParameter idspawnParameter = new MySqlParameter("@newidSpawn", MySqlDbType.VarChar, 0);
			MySqlParameter position_xParameter = new MySqlParameter("@newposition_x", MySqlDbType.VarChar, 0);
			MySqlParameter position_yParameter = new MySqlParameter("@newposition_y", MySqlDbType.VarChar, 0);
			MySqlParameter position_zParameter = new MySqlParameter("@newposition_z", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_xParameter = new MySqlParameter("@neworientation_x", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_yParameter = new MySqlParameter("@neworientation_y", MySqlDbType.VarChar, 0);
			MySqlParameter orientation_zParameter = new MySqlParameter("@neworientation_z", MySqlDbType.VarChar, 0);
			idspawnParameter.Value = idSpawn;
			position_xParameter.Value = position_x;
			position_yParameter.Value = position_y;
			position_zParameter.Value = position_z;
			orientation_xParameter.Value = orientation_x;
			orientation_yParameter.Value = orientation_y;
			orientation_zParameter.Value = orientation_z;
			OLD_idspawnParameter.Value = OLD_idSpawn;
			OLD_position_xParameter.Value = OLD_position_x;
			OLD_position_yParameter.Value = OLD_position_y;
			OLD_position_zParameter.Value = OLD_position_z;
			OLD_orientation_xParameter.Value = OLD_orientation_x;
			OLD_orientation_yParameter.Value = OLD_orientation_y;
			OLD_orientation_zParameter.Value = OLD_orientation_z;
			cmd.Parameters.Add(idspawnParameter);
			cmd.Parameters.Add(position_xParameter);
			cmd.Parameters.Add(position_yParameter);
			cmd.Parameters.Add(position_zParameter);
			cmd.Parameters.Add(orientation_xParameter);
			cmd.Parameters.Add(orientation_yParameter);
			cmd.Parameters.Add(orientation_zParameter);
			cmd.Parameters.Add(OLD_idspawnParameter);
			cmd.Parameters.Add(OLD_position_xParameter);
			cmd.Parameters.Add(OLD_position_yParameter);
			cmd.Parameters.Add(OLD_position_zParameter);
			cmd.Parameters.Add(OLD_orientation_xParameter);
			cmd.Parameters.Add(OLD_orientation_yParameter);
			cmd.Parameters.Add(OLD_orientation_zParameter);
			cmd.ExecuteNonQuery();
			conn.Close();
			updateOldValues();
		}

		private void updateOldValues()
		{
			OLD_idSpawn = idSpawn;
			OLD_position_x = position_x;
			OLD_position_y = position_y;
			OLD_position_z = position_z;
			OLD_orientation_x = orientation_x;
			OLD_orientation_y = orientation_y;
			OLD_orientation_z = orientation_z;
		}
	}
}
