using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace login_and_Register_System
{
    public partial class DashboardPage : Form
    {
        public DashboardPage()
        {
            InitializeComponent();
            textBilgiMesaji.Hide();
            label7.Hide();
            label21.Hide();
            label33.Hide();
            labelDurum2.Hide();
            label38.Hide();

            string strProvider = "Server=localhost;Database=mydb;Uid=root;Pwd=;SslMode=none;";
            string strSql = "SELECT * FROM db_aracdurum";
            MySqlConnection con = new MySqlConnection(strProvider);
            MySqlCommand cmd = new MySqlCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr = cmd.ExecuteReader();
            int nColunas = dr.FieldCount;
            for (int i = 0; i < nColunas; i++)
            {
                ServisDurumTable.Columns.Add(dr.GetName(i).ToString(), dr.GetName(i).ToString());
            }
            string[] linhaDados = new string[nColunas];
            while (dr.Read())
            {
                for (int a = 0; a < nColunas; a++)
                {
                    if (dr.GetFieldType(a).ToString() == "System.Int32")
                    {
                        linhaDados[a] = dr.GetInt32(a).ToString();
                    }
                    if (dr.GetFieldType(a).ToString() == "System.String")
                    {
                        linhaDados[a] = dr.GetString(a).ToString();
                    }
                    if (dr.GetFieldType(a).ToString() == "System.DateTime")
                    {
                        linhaDados[a] = dr.GetDateTime(a).ToString();
                    }
                }
                ServisDurumTable.Rows.Add(linhaDados);

            }
    }

        db_connection db = new db_connection();
        MailMessage message;
        SmtpClient smtp;    
        
        private void DashboardPage_Load(object sender, EventArgs e)
        {

        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hesabımToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
  
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            if (textAracPlaka.Text == "" || textMusteriAdi.Text == "" || textMusteriSoyadi.Text == "" || textTelefon.Text == "")
            {               
               // MessageBox.Show("Lütfen boş bırakılan alanları doldurunuz", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBilgiMesaji.ForeColor = Color.Red;
                textBilgiMesaji.Text = "Boş bırakılan alanları doldurun!";
                textBilgiMesaji.Show();
            }
            else
            {
                get_set newUser = new get_set();
                newUser.AracPlaka =          textAracPlaka.Text;
                newUser.MusteriAdi =         textMusteriAdi.Text;
                newUser.MusteriSoyadi =      textMusteriSoyadi.Text;
                newUser.TelefonNo =          textTelefon.Text;
                newUser.MusteriMail =        textBox19.Text;
                db.saveData(newUser);

                textAracPlaka.Text      =   "";
                textMusteriAdi.Text     =   "";
                textMusteriSoyadi.Text  =   "";
                textTelefon.Text        =   "";
                textBox19.Text          =   "";

                textBilgiMesaji.Text = "Kayıt başarı ile oluşturuldu!";
                textBilgiMesaji.ForeColor = Color.Green;
                textBilgiMesaji.Show();


            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBilgiMesaji_Click(object sender, EventArgs e)
        {
            
        }

        private void textTelefon_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            get_set user = new get_set();

            user.AracPlaka = textBox1.Text;
            db.getMusteriInfo(user);        
            
            textBox2.Text = user.MusteriAdi;
            textBox3.Text = user.MusteriSoyadi;
            textBox4.Text = user.TelefonNo;
            textBox5.Text = user.MusteriNo;
            textBox20.Text = user.MusteriMail;
            
        }     
        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void hesabımToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textticketMessage2.Text == "" || textContact2.Text == "" || comboBox12.SelectedIndex < 1)
            {
                MessageBox.Show("Lütfen boş bırakılan alanları doldurunuz", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelDurum2.Text = "Boş alanları doldurduktan sonra tekrar deneyiniz.";
                labelDurum2.ForeColor = Color.Red;
                labelDurum2.Show();
            }
            else
            {
                get_set user = new get_set();
                user.TicketMessage = textticketMessage2.Text;
                user.Konu = comboBox12.Text;
                user.UserMail = textContact2.Text;
                db.createTicket(user);           

                String to = textContact2.Text;                         
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("beylicek@gmail.com", "Deneme123.");               


                string sqlCom = " Select * from db_kayitlar where KayitID  =(SELECT max(KayitID) from db_kayitlar)";
                using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=mydb;Uid=root;Pwd=;SslMode=none;"))
                using (MySqlCommand Command = new MySqlCommand(sqlCom, connection))                                
                {

                    connection.Open();
                    MySqlDataReader DB_Reader = Command.ExecuteReader();

                    if (DB_Reader.Read() == true)
                    {
                        String takipNO = (DB_Reader["KayitID"].ToString());                    

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("beylicek@gmail.com", "BAHADIR OTOGAZ");
                        mail.To.Add(to);
                        mail.Subject = "Oluşturduğunuz Destek Kaydı Hakkında";
                        mail.IsBodyHtml = true;           
                        mail.Body = " Oluşturduğunuz '" + user.Konu + "' ile alakalı takip numaranız: '" + takipNO + "', " +
                        "<br /> Kaydınız : " + textticketMessage2.Text + "" +
                        "<br /> <br /> Kaydınıza 24 saat içinde dönüş yapılacaktır. İyi çalışmalar dileriz. ";

                        sc.Send(mail);
                        
                        textticketMessage2.Text = "";
                        comboBox12.Text = "";
                        textContact2.Text = "";
                        labelDurum2.Text = "Kaydınız başarı ile oluşturuldu! Takip numarası için mail adresinizi kontrol edin.";
                        labelDurum2.ForeColor = Color.Green;
                        labelDurum2.Show();
                        
                        
                    }
                    
                }
                

            }
            
        }        

        private void textticketMessage2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelCevap_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            get_set user = new get_set();
            user.KayitID = textBox6.Text;
            db.getTicket(user);

            labelCevap.Text = user.AnswerTicket;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox8.Text == ""|| textBox9.Text == "" || textBox10.Text == ""|| comboBox1.SelectedIndex < 1)
            {
                //MessageBox.Show("Lütfen boş bırakılan alanları doldurunuz", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label21.Text = "Boş alanları doldurduktan sonra tekrar deneyiniz.";
                label21.ForeColor = Color.Red;
                label21.Show();

            }
            else
            {
                get_set user = new get_set();
                user.Marka                       = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                user.Model                       = textBox7.Text;
                user.ModelYılı                   = textBox8.Text;
                user.SaseNo                      = textBox9.Text;
                user.AracPlaka                   = textBox10.Text;
                user.TahminiServisBitisTarihi    = dateTimePicker1.Text.ToString();
                db.saveCar(user);

                comboBox1.Items[comboBox1.SelectedIndex]                 = string.Empty;
                textBox7.Text                                            = "";
                textBox8.Text                                            = "";
                textBox9.Text                                            = "";
                textBox10.Text                                           = "";
                dateTimePicker1.Text                                     = string.Empty;

                label21.Text = "Araç başarı ile kaydedildi!";
                label21.ForeColor = Color.Green;
                label21.Show();


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            get_set user = new get_set();
            
            user.AracPlaka = textBox11.Text ;                      
            db.getCarInfo(user);
            db.getMusteriInfo(user);

            textBox12.Text = user.Marka;
            textBox16.Text = user.AracPlaka;
            textBox13.Text = user.Model;
            textBox14.Text = user.ModelYılı;
            textBox15.Text = user.SaseNo;
            textBox17.Text = user.KayitTarihi;
            textBox18.Text = user.TahminiCikisTarihi;
            textBox21.Text = user.MusteriMail;
            comboBox2.Text = user.Durum;
            

        }           

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (textBox12.Text == "" || textBox13.Text == "" || textBox14.Text == "" || textBox15.Text == "" || comboBox2.SelectedIndex < 1)
            {
                MessageBox.Show("Lütfen boş bırakılan alanları doldurunuz", "Güncelleme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                get_set user = new get_set();

                user.AracPlaka = textBox11.Text;
                user.Durum = comboBox2.Items[comboBox2.SelectedIndex].ToString();
                db.updateCar(user);             

                user.AracPlaka = textBox11.Text;
                db.getCarInfo(user);
                db.getMusteriInfo(user);
   
                String to = user.MusteriMail;
                
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("beylicek@gmail.com", "Deneme123.");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("beylicek@gmail.com", "BAHADIR OTOGAZ");
                mail.To.Add(to);
                mail.Subject = "Aracınızın Servis Durumu Hakkında";
                mail.IsBodyHtml = true;
                mail.Body = "Merhaba " + user.MusteriAdi + " "+" "+"  "+ user.MusteriSoyadi + ", " +
                    "<br /><br />  Servisimizdeki " + user.AracPlaka + " plakalı aracınızın durumu " + user.Durum + " olarak güncellenmiştir. " +
                    "<br /> Aracınızın servisten çıkış tarihi  " + user.TahminiCikisTarihi + " olarak belirlenmiştir." +
                    "<br /> <br /> Sağlıklı günler dileriz." +
                    "<br /> <br /> <br /> Aracınızın bilgileri aşağıda yer almaktadır ," +
                    "<br /> MARKA : " + user.Marka +
                    "<br /> MODEL : " + user.Model +
                    "<br /> MODEL YILI : " + user.ModelYılı +
                    "<br /> ŞASE NUMARASI : " + user.SaseNo +
                    "<br /> PLAKA : " + user.AracPlaka +
                    "<br /> SERVİSE GİRİŞ TARİHİ : " + user.KayitTarihi + "";                    

                sc.Send(mail);

                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
                textBox16.Text = "";
                textBox17.Text = "";
                textBox18.Text = "";
                comboBox2.SelectedItem = string.Empty;

                label33.Show();
                label33.ForeColor = Color.Green;
                label33.Text = "Araç bilgileri başarı ile güncellendi !";




                               
            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
          

        }

        private void label162_Click(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged_1(object sender, EventArgs e)
        {          
                           
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {            

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DashboardPage().Show();

        }

        private void tabPage7_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            get_set user = new get_set();
                        
            user.TelefonNo =     textBox22.Text;
            user.MusteriSoyadi = textBox23.Text;
            user.MusteriMail =   textBox24.Text;
            user.MusteriAdi =    textBox25.Text;
            user.MusteriNo =     textBox26.Text;
            user.AracPlaka =     textBox28.Text;
            db.updateMusteri(user);

            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            textBox26.Text = "";
            textBox28.Text = "";

            label38.Text = "Müşteri bilgileri güncellendi!";
            label38.ForeColor = Color.Green;
            label38.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            get_set user = new get_set();

            user.AracPlaka = textBox27.Text;
            db.getMusteriInfo(user);

            textBox25.Text = user.MusteriAdi;
            textBox23.Text = user.MusteriSoyadi;
            textBox24.Text = user.MusteriMail;
            textBox22.Text = user.TelefonNo;
            textBox26.Text = user.MusteriNo;
            textBox28.Text = user.AracPlaka;
            
        }
    }
}
