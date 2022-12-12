using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Mail;

namespace Mobile_Store
{
    public partial class Apple_Store : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public Apple_Store()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        SqlConnection con;
        Timer timer;
        string mobileprice;

        private void Mobile_Store_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-D9LR60L\\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                lbl_connect_server.Visible = false;
                lv_itemlist.Visible = true;
                executecmd("Mac");
                con.Close();
                timer.Stop();
            }
            catch (SqlException)
            {
                if (con.State != ConnectionState.Open)
                {
                    lbl_connect_server.Text = "Connection To The Server Failed!";
                }
            }
        }

        private void lbl_mac_Click(object sender, EventArgs e)
        {
            try
            {
                changefont();
                lbl_mac.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Bold);
                lv_itemlist.Items.Clear();
                con.Open();
                executecmd("Mac");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_ipad_Click(object sender, EventArgs e)
        {
            try
            {
                changefont();
                lbl_ipad.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Bold);
                lv_itemlist.Items.Clear();
                con.Open();
                executecmd("iPad");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_iphone_Click(object sender, EventArgs e)
        {
            try
            {
                changefont();
                lbl_iphone.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Bold);
                lv_itemlist.Items.Clear();
                con.Open();
                executecmd("iPhone");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_watch_Click(object sender, EventArgs e)
        {
            try
            {
                changefont();
                lbl_watch.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Bold);
                lv_itemlist.Items.Clear();
                con.Open();
                executecmd("Watch");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_airpods_Click(object sender, EventArgs e)
        {
            try
            {
                changefont();
                lbl_airpods.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Bold);
                lv_itemlist.Items.Clear();
                con.Open();
                executecmd("Airpods");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_tv_home_Click(object sender, EventArgs e)
        {
            try
            {
                changefont();
                lbl_tv_home.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Bold);
                lv_itemlist.Items.Clear();
                con.Open();
                executecmd("Home");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changefont()
        {
            lbl_mac.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Regular);
            lbl_ipad.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Regular);
            lbl_iphone.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Regular);
            lbl_watch.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Regular);
            lbl_airpods.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Regular);
            lbl_tv_home.Font = new Font("Microsoft YaHei UI", 11.25f, FontStyle.Regular);
        }

        int imgindex = 0;
        int selecteditem = 0;
        string selecteditem_name;
        private void executecmd(string category)
        {
            try
            {
                int x = 0;
                SqlCommand cmd = new SqlCommand("SELECT * FROM MobileStore WHERE MobCategory='" + category + "'", con);
                cmd.ExecuteNonQuery();
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        string imagename = read["imagename"].ToString();
                        string mobilename = read["MobName"].ToString();
                        imageList1.Images.Add((Image)MobileStoreImages.MobileStoreImages.ResourceManager.GetObject(imagename));
                        lv_itemlist.Items.Add(mobilename, imgindex);
                        imgindex++;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting To The Server!", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lv_itemlist_ItemActivate(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT imagename,MobPrice FROM MobileStore WHERE MobName='" + selecteditem_name + "'", con);
            cmd.ExecuteNonQuery();
            using (SqlDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    string imagename = read["imagename"].ToString();
                    mobileprice = read["MobPrice"].ToString();
                    pb_device.Image = (Image)MobileStoreImages.MobileStoreImages.ResourceManager.GetObject(imagename);
                }
            }
            con.Close();
            lbl_device_name.Text = selecteditem_name;
            lbl_price.Text = mobileprice;
            pnl_purchase.Visible = true;
            lbl_mac.Visible = false;
            lbl_ipad.Visible = false;
            lbl_iphone.Visible = false;
            lbl_watch.Visible = false;
            lbl_airpods.Visible = false;
            lbl_tv_home.Visible = false;
            pnl_purchase.BringToFront();
        }

        private void lv_itemlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selecteditem = e.ItemIndex;
            selecteditem_name = e.Item.Text;
           // e.Item.ImageIndex;
        }

        private void lbl_back_Click(object sender, EventArgs e)
        {
            pnl_purchase.Visible = false;
            lbl_mac.Visible = true;
            lbl_ipad.Visible = true;
            lbl_iphone.Visible = true;
            lbl_watch.Visible = true;
            lbl_airpods.Visible = true;
            lbl_tv_home.Visible = true;
            cmb_color.SelectedIndex = 0;
            cmb_capacity.SelectedIndex = 0;
            pnl_purchase.SendToBack();
        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(cmb_color.Text) || String.IsNullOrEmpty(cmb_capacity.Text))
            {
                MessageBox.Show("Cannot Be Null");
            }
            else
            {
                pnl_payements.Visible = true;
            }
        }

        private void lbl_back2_Click(object sender, EventArgs e)
        {
            txtFName.Clear();
            txtLName.Clear();
            txtAddress.Clear();
            txtStreet.Clear();
            txtProvince.Clear();
            mskdMobile.Clear();
            txtEmail.Clear();
            pnl_payements.Visible = false;
        }

        private void btn_purchase_Click(object sender, EventArgs e)
        {
            int mobileid=0;
            SqlCommand fetch = new SqlCommand("SELECT MobileID FROM mobilestore WHERE MobName = '" + selecteditem_name + "'",con);
            con.Open();
            fetch.ExecuteNonQuery();
            using(SqlDataReader read = fetch.ExecuteReader())
            {
                while (read.Read())
                {
                    mobileid = Convert.ToInt32(read["MobileID"].ToString());
                }
            }
            SqlCommand cmd = new SqlCommand("INSERT INTO MobileCustomer(FName,LName,CusAddress,Street,Province,Mobile,Email,MobileID,Color,Capacity,Price) VALUES ('" + txtFName.Text + "','" + txtLName.Text + "','"+txtAddress.Text+"','"+txtStreet.Text+"','"+txtProvince.Text+"','"+mskdMobile.Text+"','"+txtEmail.Text+"','"+mobileid+"','"+cmb_color.Text+"','"+cmb_capacity.Text+"',"+Convert.ToInt32(lbl_price.Text)+")", con);
            cmd.ExecuteNonQuery();
            con.Close();
            sendmail();
            pnl_purchase.Hide();
            lbl_mac.Visible = true;
            lbl_ipad.Visible = true;
            lbl_iphone.Visible = true;
            lbl_watch.Visible = true;
            lbl_airpods.Visible = true;
            lbl_tv_home.Visible = true;
            pnl_payements.Hide();
        }

        private void sendmail()
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("ashensandeepa1998@gmail.com");
                message.To.Add(new MailAddress(txtEmail.Text));
                message.Subject = "Mobile Purchase";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "Successfully Purchased From Apple Store. Thank You!";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ashensandeepa1998@gmail.com", "26Pass03#");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception se)
            {
                MessageBox.Show("Check Your Internet Connection And Try Again!","Internet Connection Issue",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void pb_close_Click(object sender, EventArgs e)
        {
            GC.WaitForFullGCComplete(10);
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            this.Close();
        }

        bool mousedown;
        Point lastlocation;
        private void pb_logo_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
            lastlocation = e.Location;
        }

        private void pb_logo_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                this.Location = new Point((this.Location.X - lastlocation.X) + e.X, (this.Location.Y - lastlocation.Y) + e.Y);
                this.Update();
            }
        }

        private void pb_logo_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void cmb_capacity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_capacity.SelectedIndex)
            {
                case 0:
                    lbl_price.Text = mobileprice;
                    break;
                case 1:
                    lbl_price.Text = (Convert.ToInt32(mobileprice) + 2000).ToString();
                    break;
                case 2:
                    lbl_price.Text = (Convert.ToInt32(mobileprice) + 10000).ToString();
                    break;
                case 3:
                    lbl_price.Text = (Convert.ToInt32(mobileprice) + 25000).ToString();
                    break;
                case 4:
                    lbl_price.Text = (Convert.ToInt32(mobileprice) + 50000).ToString();
                    break;
                case 5:
                    lbl_price.Text = (Convert.ToInt32(mobileprice) + 100000).ToString();
                    break;
            }
        }
    }
}
