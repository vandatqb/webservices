using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameraService
{
    public class CameraAccount
    {
        public CameraAccount()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private String id_account;

        public String Id_account
        {
            get { return id_account; }
            set { id_account = value; }
        }
        private String login_name;

        public String Login_name
        {
            get { return login_name; }
            set { login_name = value; }
        }
        private String full_name;

        public String Full_name
        {
            get { return full_name; }
            set { full_name = value; }
        }
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private String pass;

        public String Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        private String address;

        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private String image;

        public String Image
        {
            get { return image; }
            set { image = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public CameraAccount(string id_account, string loginName, string full_name, string phone, string address, string email, string image, String type, String trangthai)
        {
            this.id_account = id_account;
            this.login_name = loginName;
            this.full_name = full_name;
            this.phone = phone;
            this.address = address;
            this.email = email;
            this.image = image;
            this.type = type;
            this.status = trangthai;
        }
    }
}