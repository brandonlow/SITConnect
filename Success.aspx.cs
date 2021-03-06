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
using System.IO;

namespace SITConnect
{
    public partial class Success : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static byte[] Key = null;
        static byte[] IV = null;
        static byte[] card = null;
        static string userid = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            userid = Session["LoggedIn"].ToString();
            displayUserProfile(userid);
        }

        protected string decryptData(byte[] cipherText)
        {
            string plainText = null;

            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();

                        }
                    }
                }
            }


            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return plainText;
        }


        protected void displayUserProfile(string userid)
        {
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select * FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"] != DBNull.Value)
                        {
                            lbl_userID.Text = reader["Email"].ToString();

                        }
                        if (reader["FirstName"] != DBNull.Value)
                        {
                            Label1.Text = reader["FirstName"].ToString();
                        }
                        if (reader["LastName"] != DBNull.Value)
                        {
                             Label2.Text = reader["LastName"].ToString();
                        }
                        if (reader["CreditCardInfo"] != DBNull.Value)
                        {
                            card = Convert.FromBase64String(reader["CreditCardInfo"].ToString());

                        }
                        if (reader["Photo"] != DBNull.Value)
                        {
                            Image1.ImageUrl = "~/Images/" + Path.GetFileName(reader["Photo"].ToString());
                        }
                            if (reader["IV"] != DBNull.Value)
                        {
                            IV = Convert.FromBase64String(reader["IV"].ToString());

                        }
                        if (reader["Key"] != DBNull.Value)
                        {
                            Key = Convert.FromBase64String(reader["Key"].ToString());

                        }
                    }
                    lbl_card.Text = decryptData(card);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally
            {
                connection.Close();
            }
        }

    }


}