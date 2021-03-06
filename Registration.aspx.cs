using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions; // for Regular expression
using System.Drawing; // for change of color
using System.Diagnostics;
using System.IO;

namespace SITConnect
{
    public partial class Registration : System.Web.UI.Page
    {
        string MYDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        string firstname;
        string lastname;
        string creditcard;
        string email;
        string password;
        string DOB;


        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName,@LastName, @CreditCardInfo, @Email, @PasswordHash, @PasswordSalt, @DateofBirth, @Photo, @IV, @Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", firstname.Trim());
                            cmd.Parameters.AddWithValue("@LastName", lastname.Trim());
                            cmd.Parameters.AddWithValue("@CreditCardInfo", Convert.ToBase64String(encryptData(creditcard.Trim())));
                            cmd.Parameters.AddWithValue("@Email", email.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DateofBirth", DOB.Trim());
                            cmd.Parameters.AddWithValue("@Photo", tb_photo.FileName);
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Connection = con;
                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                //con.Close();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.ToString());
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            // implement codes for the button event
            // Extract data from textbox
            int scores = checkPassword(tb_password.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Very Strong";
                    break;
                default:
                    break;
            }
            lbl_pwdchecker.Text = "Status : " + status;
            if (scores < 4)
            {
                lbl_pwdchecker.ForeColor = Color.Red;
                return;
            }
            lbl_pwdchecker.ForeColor = Color.Green;

            int mark = checkCard(tb_creditcard.Text);
            string strength = "";
            switch (mark)
            {
                case 1:
                    strength = "Very Strong";
                    break;
                default:
                    break;
            }
            Label1.Text = "Status : " + strength;
            if (mark < 0)
            {
                Label1.ForeColor = Color.Red;
                return;
            }
            Label1.ForeColor = Color.Green;

            firstname = HttpUtility.HtmlEncode(tb_firstname.Text);
            lastname = HttpUtility.HtmlEncode(tb_lastname.Text);
            creditcard = HttpUtility.HtmlEncode(tb_creditcard.Text);
            email = HttpUtility.HtmlEncode(tb_email.Text);
            password = HttpUtility.HtmlEncode(tb_password.Text);
            DOB = HttpUtility.HtmlEncode(tb_DOB.Text);

            //string pwd = get value from your Textbox
            string pwd = password.ToString().Trim(); ;
            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];
            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);
            SHA512Managed hashing = new SHA512Managed();
            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
            finalHash = Convert.ToBase64String(hashWithSalt);
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            string folderPath = Server.MapPath("~/Images/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            tb_photo.SaveAs(folderPath + Path.GetFileName(tb_photo.FileName));

            createAccount();
            Response.Redirect("Login.aspx", false);
        }

        private int checkPassword(string password)
        {
            int score = 0;

            if (password.Length < 12)
            {
                return 1;
            }
            else
            {
                score = 1;
            }

            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (password.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                score++;
            }
            return score;
        }
        private int checkCard(string card)
        {
            int score = 0;

            if (card.Length < 16)
            {
                return 1;
            }
            if (card.Length > 16)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            return score;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void tb_password_TextChanged(object sender, EventArgs e)
        {

        }

    }
    
}