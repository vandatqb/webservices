using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace CameraService
{
    /// <summary>
    /// Summary description for Services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Services : System.Web.Services.WebService
    {
        DataAccess da = new DataAccess();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        //mã hóa md 5
        public string calculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        //chat

        [WebMethod]
        public List<ListChat> getListChat(String id)
        {
            List<ListChat> list = new List<ListChat>();
            String qr = "select Id_account,Full_name,Image  from account where id_account !='" + id + "' and id_account in " +
                "( select distinct id_send from chat where id_receive = '" + id + "') or Id_account in " +
                "( select distinct id_receive from chat where id_send = '" + id + "')";
            DataTable dt = da.docDuLieuDataTable(qr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String idaccount = dt.Rows[i]["Id_account"].ToString();
                String full_name = dt.Rows[i]["Full_name"].ToString();
                String image = dt.Rows[i]["Image"].ToString();
                ListChat chat = getLastChat(id, idaccount);
                String content = chat.Content;
                String time = chat.Time;
                ListChat a = new ListChat(idaccount, full_name, image, content, time);
                list.Add(a);
            }
            return list;
        }
        [WebMethod]
        public ListChat getLastChat(String id_send, String id_receive)
        {
            ListChat chat; ;
            String qr = "SELECT TOP 1 * FROM chat where (id_send='" + id_send + "' and id_receive='" + id_receive + "') or(id_send='" + id_receive + "' and id_receive='" + id_send + "')  ORDER BY ID DESC";
            DataTable dt = da.docDuLieuDataTable(qr);
            String content = dt.Rows[0]["content"].ToString();
            String time = dt.Rows[0]["time"].ToString();
            chat = new ListChat("id", "fuull", "image", content, time);
            return chat;
        }

        [WebMethod]
        public List<Chat> getChatDetail(String idsend, String idreceive)
        {
            List<Chat> list = new List<Chat>();
            String qr = "select * from chat where ( id_send = '" + idsend + "' and id_receive = '" + idreceive + "' ) or" +
                "( id_send = '" + idreceive + "' and id_receive = '" + idsend + "')";
            DataTable dt = da.docDuLieuDataTable(qr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String id_send = dt.Rows[i]["id_send"].ToString();
                String id_receive = dt.Rows[i]["id_receive"].ToString();
                String time = dt.Rows[i]["Time"].ToString();
                String content = dt.Rows[i]["content"].ToString();
                String node = dt.Rows[i]["node"].ToString();
                Chat a = new Chat(id_send, id_receive, time, content, node);
                list.Add(a);
            }
            return list;
        }
        [WebMethod]
        public string addChat(int idsend, int idreceive, String content)
        {
            String qr = "Select node from chat where (id_send='" + idsend + "' and id_receive ='" + idreceive + "') or "
                + "(id_receive='" + idsend + "' and id_send ='" + idreceive + "')";
            String node = da.docDLDuyNhat(qr);
            if (!node.Equals(""))
            {
                String qr1 = "insert into chat values('" + idsend + "','" + idreceive + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "',N'" + content + "','" + node + "')";
                da.capNhatDuLieu(qr1);
            }
            else
            {
                node = idsend + "vs" + idreceive;
                String qr2 = "insert into chat values('" + idsend + "','" + idreceive + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "',N'" + content + "','" + node + "')";
                da.capNhatDuLieu(qr2);
            }
            return node;
        }
        [WebMethod]
        public string getNode(int idsend, int idreceive)
        {
            String qr = "Select node from chat where (id_send='" + idsend + "' and id_receive ='" + idreceive + "') or "
                + "(id_receive='" + idsend + "' and id_send ='" + idreceive + "')";
            String node = da.docDLDuyNhat(qr);
            if (node.Equals(""))
            {
                node = "0";
            }
            return node;
        }

        // Camera
        [WebMethod]
        public List<Maker> getListHang()
        {
            List<Maker> list = new List<Maker>();
            String qr = "select * from Maker";
            DataTable dt = da.docDuLieuDataTable(qr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String id_maker = dt.Rows[i]["id_maker"].ToString();
                String name_maker = dt.Rows[i]["name_maker"].ToString();

                Maker a = new Maker(id_maker, name_maker);
                list.Add(a);
            }
            return list;
        }

        [WebMethod]
        public int deleteProduct(String id)
        {
            String qr = "delete product where id_produc = '" + id + "'";
            int a = da.capNhatDuLieu(qr);
            if (a > 0)
                return 1;
            else
                return 0;
        }
        [WebMethod]
        public int addProduct(String id_account, String name, String id_maker, String type, String mega,
            String video, String add, String price, String time, String image, String image1, String image2, String status)
        {

            String qr = "insert into product values('" + id_account + "',N'" + name + "','" + id_maker + "','" + type + "','"
                + mega + "','" + video + "','" + add + "','" + price + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','"
                + image + "',' " + image1 + "','" + image2 + "','0')";
            int a = da.capNhatDuLieu(qr);
            if (a > 0)
                return 1;
            else
                return 0;
        }

        [WebMethod]
        public List<Product> getListProduct()
        {
            List<Product> list = new List<Product>();
            String qr = "select top(30) *  from product, maker where product.id_maker = maker.id_maker";
            DataTable dt = da.docDuLieuDataTable(qr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String id_produc = dt.Rows[i]["id_produc"].ToString();
                String id_account = dt.Rows[i]["id_account"].ToString();
                String name = dt.Rows[i]["name"].ToString();
                String id_maker = dt.Rows[i]["name_maker"].ToString();
                String type = dt.Rows[i]["Type"].ToString();
                String mega = dt.Rows[i]["mega"].ToString();
                String video = dt.Rows[i]["video"].ToString();
                String add = dt.Rows[i]["addition"].ToString();
                String price = dt.Rows[i]["price"].ToString();
                String time = dt.Rows[i]["time"].ToString();
                String image = dt.Rows[i]["image"].ToString();
                String image1 = dt.Rows[i]["image1"].ToString();
                String image2 = dt.Rows[i]["image2"].ToString();
                String status = dt.Rows[i]["Status"].ToString();
                Product a = new Product(id_produc, id_account, name, id_maker, type, mega, video, add, price, time, image, image1, image2, status);
                list.Add(a);
            }
            return list;
        }
        public List<Product> getListProductWithSQl(String sql)
        {
            List<Product> list = new List<Product>();
            DataTable dt = da.docDuLieuDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String id_produc = dt.Rows[i]["id_produc"].ToString();
                String id_account = dt.Rows[i]["id_account"].ToString();
                String name = dt.Rows[i]["name"].ToString();
                String id_maker = dt.Rows[i]["name_maker"].ToString();
                String type = dt.Rows[i]["Type"].ToString();
                String mega = dt.Rows[i]["mega"].ToString();
                String video = dt.Rows[i]["video"].ToString();
                String add = dt.Rows[i]["add"].ToString();
                String price = dt.Rows[i]["price"].ToString();
                String time = dt.Rows[i]["time"].ToString();
                String image = dt.Rows[i]["image1"].ToString();
                String image1 = dt.Rows[i]["image2"].ToString();
                String image2 = dt.Rows[i]["image3"].ToString();
                String status = dt.Rows[i]["Status"].ToString();
                Product a = new Product(id_produc, id_account, name, id_maker, type, mega, video, add, price, time, image, image1, image2, status);
                list.Add(a);
            }
            return list;
        }

        //admin
        [WebMethod]
        public int updateStatusAccount(String id, String trangthai)
        {
            String qr = "update account set Status = '" + trangthai + "' where id_account = '" + id + "'";
            int a = da.capNhatDuLieu(qr);
            if (a > 0)
                return 1;
            else
                return 0;
        }
        [WebMethod]
        public int updateAccountInfor(String id, String fullName, String phone, String address, String email, String image)
        {
            String qr;
            if (image.Length > 40)
            {
                qr = "update account set full_name = '" + fullName + "', phone='" + phone + "',address='" + address + "',email='" + email + "',image='" + image + "' where id_account = '" + id + "'";
            }
            else
            {
                qr = "update account set full_name = '" + fullName + "', phone='" + phone + "',address='" + address + "',email='" + email + "' where id_account = '" + id + "'";
            }

            int a = da.capNhatDuLieu(qr);
            if (a > 0)
                return 1;
            else
                return 0;
        }
        [WebMethod]
        public int updateStatusPost(String id, String status)
        {
            String qr = "update product set Status = '" + status + "' where id_produc = '" + id + "'";
            int a = da.capNhatDuLieu(qr);
            if (a > 0)
            {
                if (status.Equals("1"))
                {
                    sendMail(id);
                }
                return 1;
            }
            else
            {
                return 0;
            }

        }
        [WebMethod]
        public List<CameraAccount> getListCameraAccount()
        {
            List<CameraAccount> list = new List<CameraAccount>();
            String qr = "select *  from account";
            DataTable dt = da.docDuLieuDataTable(qr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String id_account = dt.Rows[i]["id_account"].ToString();
                String login_name = dt.Rows[i]["login_name"].ToString();
                String full_name = dt.Rows[i]["Full_name"].ToString();
                String phone = dt.Rows[i]["phone"].ToString();
                String address = dt.Rows[i]["address"].ToString();
                String email = dt.Rows[i]["email"].ToString();
                String image = dt.Rows[i]["image"].ToString();
                String type = dt.Rows[i]["type"].ToString();
                String status = dt.Rows[i]["status"].ToString();
                CameraAccount a = new CameraAccount(id_account, login_name, full_name, phone, address, email, image, type, status);
                list.Add(a);
            }
            return list;

        }
        //add account
        [WebMethod]
        public String addAccount(String login_name, String full_name, String phone, String pass, String address, String email, String image)
        {
            String id = "";
            String qr = "insert into account values('" + login_name + "',N'" + full_name + "','" + phone + "','"
                + calculateMD5Hash(pass) + "',N'" + address + "','" + email + "','" + image + "','0','0')";
            if (da.capNhatDuLieu(qr) > 0)
            {
                String qr1 = "select id_account from account where login_name = '" + login_name + "'";
                id = da.docDLDuyNhat(qr1).ToString();
            }
            return id;
        }
        [WebMethod]
        public Boolean checkAccount(String loginName)
        {
            String qr2 = "select id_account from account where login_name = '" + loginName + "'";
            if (da.docDLDuyNhat(qr2).Equals(""))
                return false;
            else return true;
        }
        [WebMethod]
        public String checkLogin(String loginName, String pass)
        {
            String qr2 = "select id_account from account where login_name = '" + loginName + "' and pass='" + calculateMD5Hash(pass) + "'";
            String id = da.docDLDuyNhat(qr2);
            if (id.Equals(""))
                return 0 + "";
            else return id;
        }
        [WebMethod]
        public CameraAccount getAccountInfor(String idlogin)
        {
            CameraAccount account = new CameraAccount();
            DataTable table;
            try
            {
                String qr1 = "select * from account where login_name = '" + idlogin + "' or id_account ='" + idlogin + "'";
                table = da.docDuLieuDataTable(qr1);
            }
            catch (Exception e)
            {
                String qr2 = "select * from account where login_name = '" + idlogin + "'";
                table = da.docDuLieuDataTable(qr2);
            }

            if (table.Rows.Count == 1)
            {
                String id_account = table.Rows[0]["id_account"].ToString();
                String login_name = table.Rows[0]["login_name"].ToString();
                String full_name = table.Rows[0]["full_name"].ToString();
                String phone = table.Rows[0]["phone"].ToString();
                String address = table.Rows[0]["address"].ToString();
                String email = table.Rows[0]["email"].ToString();
                String image = table.Rows[0]["image"].ToString();
                String type = table.Rows[0]["type"].ToString();
                String status = table.Rows[0]["status"].ToString();
                account = new CameraAccount(id_account, login_name, full_name, phone, address, email, image, type, status);
            }
            return account;
        }
        [WebMethod]
        public String changePass(String id, String oldPass, String newpass)
        {
            String qr2 = "update account set pass='" + calculateMD5Hash(newpass) + "' where id_account = '" + id + "' and pass='" + calculateMD5Hash(oldPass) + "'";
            if (da.capNhatDuLieu(qr2) < 1)
                return 0 + "";
            else return 1 + "";
        }
        public void sendMail(String id)
        {
            String qrNameProduct = "select name from product where id_produc ='" + id + "'";
            String qrPriceProduct = "select price from product where id_produc ='" + id + "'";
            String qrIdAccount = "select id_account from product where id_produc ='" + id + "'";
            String idAccount = da.docDLDuyNhat(qrIdAccount).ToString();
            String qrNameAccount = "select full_name from account where id_account ='" + idAccount + "'";
            String qrEmailAccount = "select email from account where id_account ='" + idAccount + "'";


            String nameProduct = da.docDLDuyNhat(qrNameProduct).ToString();
            String nameAccount = da.docDLDuyNhat(qrNameAccount).ToString();
            String email = da.docDLDuyNhat(qrEmailAccount).ToString();
            String priceProduct = da.docDLDuyNhat(qrPriceProduct).ToString();

            String body = "Chào : " + nameAccount + "!\n";
            body = body + "Sản phẩm : " + nameProduct + " với giá " + priceProduct + " trên app camera Đà Nẵng đã được chúng tôi phê duyệt ! \n";
            body = body + "Cảm ơn bạn đã sự dụng phần mềm của chúng tối !";

            var fromAddress = new MailAddress("hoclamaimai16@gmail.com", "Admin Camera Đà Nẵng");
            var toAddress = new MailAddress(email, nameAccount);
            const string fromPassword = "07031995";
            const string subject = "Bài đăng trên app";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
