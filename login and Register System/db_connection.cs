using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_Register_System
{
    class db_connection
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=mydb;Uid=root;Pwd=;SslMode=none;");
        MySqlCommand command = new MySqlCommand();                
        
        public db_connection()
        {
            connect();
            connection.Open();
            
        }

        public void connect()
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=mydb;Uid=root;Pwd=;SslMode=none;");
            MySqlCommand command = new MySqlCommand();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            connection.Open();
        }
    
        public void registerUser(get_set user)
        {
            try
            {
                string reg = "INSERT INTO db_users VALUES ('" + "null" + "','" + user.Kullaniciadi + "','" + user.Sifre + "')";
                command = new MySqlCommand(reg, connection);
                command.ExecuteNonQuery();             
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

        }

        public void saveData(get_set user)
        {
            try
            {
                string register = "INSERT INTO db_musteri VALUES  ('" + user.AracPlaka + "','" + user.MusteriAdi + "','" + user.MusteriSoyadi + "','" + user.TelefonNo + "', 'null' ,'" + user.MusteriMail + "')";
                command = new MySqlCommand(register, connection);
                command.ExecuteNonQuery();

            }
            catch
            {
                return;

            }           

        }

        public void getMusteriInfo(get_set user)
        {

            string sqlCom = " select * from db_musteri where aracplaka = '" + user.AracPlaka + "'";
            MySqlCommand Command = new MySqlCommand(sqlCom, connection);
            MySqlDataReader DB_Reader = Command.ExecuteReader();

            try
            {

                if (DB_Reader.Read() == true)
                {
                    user.AracPlaka      = (DB_Reader["aracplaka"].ToString())      ;
                    user.MusteriAdi     = (DB_Reader["musteriadi"].ToString())     ;
                    user.MusteriSoyadi  = (DB_Reader["musterisoyadi"].ToString())  ;
                    user.TelefonNo      = (DB_Reader["telefonno"].ToString())      ;
                    user.MusteriNo      = (DB_Reader["MüsteriNo"].ToString())      ;
                    user.MusteriMail    = (DB_Reader["MusteriMail"].ToString())    ;

                }
                else
                {
                    user.AracPlaka      = "";
                    user.MusteriAdi     = "";
                    user.MusteriSoyadi  = "";
                    user.TelefonNo      = "";
                }

            }

            catch
            {
                return;

            }

            finally
            {
                DB_Reader.Close();
            }

        }

        public void getCarInfo(get_set user)
        {
            
            string sqlCom = " select * from db_aracdurum where AracPlaka = '" + user.AracPlaka + "'";
            MySqlCommand Command = new MySqlCommand(sqlCom, connection);
            MySqlDataReader DB_Reader = Command.ExecuteReader();

            try
            {

                if (DB_Reader.Read() == true)
                {                    
                    user.Marka = (DB_Reader["Marka"].ToString());
                    user.Model = (DB_Reader["Model"].ToString());
                    user.ModelYılı = (DB_Reader["ModelYılı"].ToString());
                    user.SaseNo = (DB_Reader["SaseNo"].ToString());
                    user.AracPlaka = (DB_Reader["AracPlaka"].ToString());
                    user.KayitTarihi = (DB_Reader["KayitTarihi"].ToString());
                    user.TahminiCikisTarihi = (DB_Reader["TahminiServisBitisTarihi"].ToString());
                    user.Durum = (DB_Reader["Durum"].ToString());
                }
                else
                {
                    user.Marka = "Araç Bulunamadı !";
                    user.Model = "";
                    user.ModelYılı = "";
                    user.SaseNo = "";
                    user.AracPlaka = "";
                    user.KayitTarihi = "";
                    user.TahminiCikisTarihi = "";
                    user.Durum = "";
                }
                
            } 
            
            catch
            {
                return;
                
            }

            finally
            {                               
               DB_Reader.Close();                
            }


        }

        public void saveCar(get_set user)
        {
            try
            {                
                string dt;
                string dt2;
                DateTime date = DateTime.Now;
                DateTime date2 = DateTime.Now;
                dt = date.ToLongTimeString();        // display format:  11:45:44 AM
                dt2 = date2.ToShortDateString();     // display format:  5/22/2010               

                string register = "INSERT INTO db_aracdurum VALUES ('Atama bekliyor', '"+user.AracPlaka+"', '"+user.TahminiServisBitisTarihi+"', '"+dt+" "+dt2+"', '"+user.Marka+"', '"+user.Model+"', '"+user.ModelYılı+"', '"+user.SaseNo+ "', 'kayit_no')";
                command = new MySqlCommand(register, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                return;
            }           

        }       
     
        public void updateCar(get_set user)

        {            
            try
            {
                string register = " UPDATE db_aracdurum SET Durum = '" + user.Durum + "' where AracPlaka = '" + user.AracPlaka + "'";
                command = new MySqlCommand(register, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                return;
            }
            

        }
        public void updateMusteri(get_set user)

        {
            try
            {
                string register = " UPDATE db_musteri "+
                    "SET musteriadi= '" + user.MusteriAdi+"',"+
                    "musterisoyadi= '" + user.MusteriSoyadi+"',"+
                    "aracplaka= '" + user.AracPlaka+"',"+
                    "MusteriMail= '" + user.MusteriMail+"'"+
                    "where MüsteriNo = '" + user.MusteriNo +"'";
                command = new MySqlCommand(register, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                return;
            }


        }

        public void createTicket(get_set user)
        {
            try
            {                
                string dt;
                string dt2;
                DateTime date = DateTime.Now;
                DateTime date2 = DateTime.Now;
                dt = date.ToLongTimeString();        // display format:  11:45:44 AM
                dt2 = date2.ToShortDateString();     // display format:  5/22/2010

                var generator = new RandomGenerator();
                var randomString = generator.RandomString(6);

                string register = "INSERT INTO db_kayitlar VALUES ('" + "Uid" + "','" + user.TicketMessage + "','" + user.Konu + "','" + user.UserMail + "','" + dt + " " + dt2 + "','" + "Değerlendiriliyor." + "','" + randomString + "')";
                command = new MySqlCommand(register, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                return;
            }           

        }

        public void getTicket(get_set user)
        {

            string sqlCom = " Select * from db_kayitlar where KayitID = '" + user.KayitID + "'";              
            MySqlCommand Command = new MySqlCommand(sqlCom, connection);
            MySqlDataReader DB_Reader = Command.ExecuteReader();
            try
            {
                
                DB_Reader.Read();
                user.AnswerTicket = (DB_Reader["KayitDurum"].ToString());

            }
            catch (Exception)
            {

                return;
            }
            finally
            {
                DB_Reader.Close();
            }

        }

        public void loginUser(get_set user)
        {

            string login = "SELECT * FROM db_users WHERE kullaniciadi= '" + user.Kullaniciadi + "' and sifre= '" + user.Sifre + "'";
            command = new MySqlCommand(login, connection);
            MySqlDataReader dr = command.ExecuteReader();
            try
            {               
                if (dr.Read() == true)
                {
                    new DashboardPage().Show();                   
                    
                }                

            }
            catch (Exception)
            {

                return;
            }      
            
            finally
            {
                dr.Close();
            }

        }         

        public class RandomGenerator
        {           
            private readonly Random _random = new Random();
                 
            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }
               
            public string RandomString(int size, bool lowerCase = false)
            {
                var builder = new StringBuilder(size);
               
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; 

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }
            // Generates a random password.  
            // 4-LowerCase + 4-Digits + 2-UpperCase  
            public string RandomPassword()
            {
                var passwordBuilder = new StringBuilder();

                // 4-Letters lower case   
                passwordBuilder.Append(RandomString(4, true));

                // 4-Digits between 1000 and 9999  
                passwordBuilder.Append(RandomNumber(1000, 9999));

                // 2-Letters upper case  
                passwordBuilder.Append(RandomString(2));
                return passwordBuilder.ToString();
            }
        }











    }
}
