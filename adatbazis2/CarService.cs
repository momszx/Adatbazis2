using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace adatbazis2
{
    class CarService:ICarService
    {
        string con = "Data Source=193.225.33.71;User Id=vlngfw;Password=EKE2020";
        private static Random random = new Random();
        private static Dictionary<string, User> Users = new Dictionary<string, User>();
        public static List<Car> Cars = new List<Car>();

        public string Login(string username, string password)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(con))
                {
                    string ID = string.Empty;
                    string queryString = String.Format("SELECT id,name from Users", username, password);
                    OracleCommand command = new OracleCommand(queryString, connection);
                    connection.Open();
                    OracleDataReader dataReader = command.ExecuteReader();
                    var temp = dataReader.RowSize;
                    if (dataReader.HasRows)
                    {
                        User user = null;
                        while (dataReader.Read())
                        {
                            user = new User(dataReader.GetInt32(0), dataReader.GetString(1));
                        }
                        ID = Guid.NewGuid().ToString();
                        Users.Add(ID, user);
                        command.CommandText = String.Format("INSERT INTO Logs( info, user_id, time)values ('Login',{0},CURRENT_TIMESTAMP)",user.Id);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("Database connection error");
                    }
                    dataReader.Close();
                    return ID;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Logout(string ID)
        {
            try
            {
                if (Users.ContainsKey(ID))
                {
                    return Users.Remove(ID);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public bool Add(string alvazszam, string rendszam, string tulajdonos, string tipus, string marka,string ID)
        {
            try
            {
                if (Users.ContainsKey(ID))
                {
                    try
                    {
                        string queryString = String.Format("INSERT INTO TULAJDONOS( Name) values ({0})", tulajdonos);
                        OracleDataReader temp;
                        using (OracleConnection connection = new OracleConnection(con))
                        {
                            OracleCommand command = new OracleCommand(queryString, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            command.CommandText = String.Format("Select id from TULAJDONOS");
                            temp = command.ExecuteReader();
                            command.CommandText = String.Format("Insert Into Auto( tulajdonos_id, rendszam, alvazszam)  values ({0},{1},{2})",temp.GetInt32(0),rendszam, alvazszam);
                            command.ExecuteNonQuery();
                            command.CommandText = String.Format("Select id from Auto");
                            temp = command.ExecuteReader();
                            command.CommandText = String.Format("Insert Into tipusok( AUTO_ID, MARKA, TIPUS)  values ({0},{1},{2})",temp.GetInt32(0),marka, tipus);
                            command.ExecuteNonQuery();
                            command.Clone();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Database connection error");
                    }
                }
                else
                {
                    throw new Exception("Log in first");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Delete(int tulajID,int carID,int tipisID, string ID)
        {
            try
            {
                if (Users.ContainsKey(ID))
                {
                    try
                    {
                        Car temp = new Car();
                        foreach (var car in Cars)
                        {
                            if (car.TulajdonosId==tulajID && car.AutoId==carID && car.TipusId==tipisID)
                            {
                                temp = car;
                            }
                        }
                        string queryString = String.Format("DELETE FROM Tulajdonos WHERE id = {0}", temp.TulajdonosId);
                        using (OracleConnection connection = new OracleConnection(con))
                        {
                            OracleCommand command = new OracleCommand(queryString, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            command.CommandText = String.Format("DELETE FROM Auto WHERE id = {0}", temp.AutoId);
                            command.ExecuteNonQuery();
                            command.CommandText = String.Format("DELETE FROM Tipusok WHERE id = {0}", temp.TipusId);
                            command.ExecuteNonQuery();
                            command.Clone();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Database connection error");
                    }
                    
                }
                else
                {
                    throw new Exception("Log in first");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Modify(int tulajID,int carID,int tipisID, string alvazszam, string rendszam, string tulajdonos, string tipus, string marka,string ID)
        {
            try
            {
                if (Users.ContainsKey(ID))
                {
                    try
                    {
                        Car temp = new Car();
                        foreach (var car in Cars)
                        {
                            if (car.TulajdonosId==tulajID && car.AutoId==carID && car.TipusId==tipisID)
                            {
                                temp = car;
                            }
                        }
                        string queryString = String.Format("update TULAJDONOS set NAME={0} where id={1}", tulajdonos,temp.TulajdonosId);
                        using (OracleConnection connection = new OracleConnection(con))
                        {
                            OracleCommand command = new OracleCommand(queryString, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            command.CommandText = String.Format("update Auto set RENDSZAM={0},ALVAZSZAM={1} where id={2}",rendszam,alvazszam, temp.AutoId);
                            command.ExecuteNonQuery();
                            command.CommandText = String.Format("update tipusok set MARKA={0},tipus={1'where id={2}",marka,tipus, temp.TipusId);
                            command.ExecuteNonQuery();
                            command.Clone();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Database connection error");
                    }
                }
                else
                {
                    throw new Exception("Log in first");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool GenerateData(int num,string ID)
        {
            try
            {
                if (Users.ContainsKey(ID))
                {
                    try
                    {
                        List<string> marka = new List<string>{"VW", "OPEL", "BMW", "AUDI", "FORD"};
                        List<string> tipus = new List<string> {"POLO", "MUSTANG", "FIESTA", "CORSA"};
                        List<string> names = new List<string>
                            {"Gibsz", "Jakab", "Tesz", "Elek", "Béla", "Kis", "Kováncs", "Kettes"};
                        List<Car> generated = new List<Car>();
                        for (int i = 0; i < num; i++)
                        {
                            Car tempCar = new Car();
                            tempCar.Alvazszam = RandomString(17);
                            tempCar.Marka = marka[random.Next(marka.Count - 1)];
                            tempCar.Rendszam = RandomStringChar(3) + "-" + RandomStringNum(3);
                            tempCar.Tipus = tipus[random.Next(tipus.Count - 1)];
                            tempCar.Tulajdonos = names[random.Next(names.Count - 1)] + " " +
                                                 names[random.Next(names.Count - 1)];
                        }

                        foreach (var car in generated)
                        {
                            string queryString = String.Format("INSERT INTO TULAJDONOS( Name) values ({0})", car.Tulajdonos);
                            OracleDataReader temp;
                            using (OracleConnection connection = new OracleConnection(con))
                            {
                                OracleCommand command = new OracleCommand(queryString, connection);
                                connection.Open();
                                command.ExecuteNonQuery();
                                command.CommandText = String.Format("Select id from TULAJDONOS");
                                temp = command.ExecuteReader();
                                command.CommandText = String.Format("Insert Into Auto( tulajdonos_id, rendszam, alvazszam)  values ({0},{1},{2})",temp.GetInt32(0),car.Rendszam, car.Alvazszam);
                                command.ExecuteNonQuery();
                                command.CommandText = String.Format("Select id from Auto");
                                temp = command.ExecuteReader();
                                command.CommandText = String.Format("Insert Into tipusok( AUTO_ID, MARKA, TIPUS)  values ({0},{1},{2})",temp.GetInt32(0),car.Marka, car.Tipus);
                                command.ExecuteNonQuery();
                                command.Clone();
                            }
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Database connection error");
                    }
                }
                else
                {
                    throw new Exception("Log in first");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Car> List(string ID)
        {
            try
            {
                if (Users.ContainsKey(ID))
                {
                    try
                    {
                        using (OracleConnection connection = new OracleConnection(con))
                        {
                            string queryString = string.Format(
                                "SELECT tulajdonos.id,tulajdonos.Name,Auto.id,Auto.alvazszam,Auto.rendszam,tipusok.id,tipusok.marka,tipusok.tipus from tulajdonos inner join AUTO on tulajdonos.id = Auto.tulajdonos_id inner join TIPUSOK on Auto.id = tipusok.auto_id");
                            OracleCommand command = new OracleCommand(queryString, connection);
                            connection.Open();
                            OracleDataReader r = command.ExecuteReader();
                            if (r.HasRows)
                            {
                                Cars = new List<Car>();
                                while (r.Read())
                                {
                                    Cars.Add(new Car(r.GetInt32(0), r.GetString(1), r.GetInt32(2), r.GetString(3),
                                        r.GetString(4), r.GetInt32(5), r.GetString(6), r.GetString(7)));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Database connection error");
                    }
                }
                else
                {
                    throw new Exception("Log in first");
                }
                return Cars;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static string RandomStringChar(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomStringNum(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
