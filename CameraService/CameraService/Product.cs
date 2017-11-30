using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameraService
{
    public class Product
    {
        private String id_produc;

        public String Id_produc
        {
            get { return id_produc; }
            set { id_produc = value; }
        }
        private String id_account;

        public String Id_account
        {
            get { return id_account; }
            set { id_account = value; }
        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String id_maker;

        public String Id_maker
        {
            get { return id_maker; }
            set { id_maker = value; }
        }
        private String type;

        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        private int mega;

        public int Mega
        {
            get { return mega; }
            set { mega = value; }
        }
        private int video;

        public int Video
        {
            get { return video; }
            set { video = value; }
        }
        private String addition;

        public String Addition
        {
            get { return addition; }
            set { addition = value; }
        }
        private String price;

        public String Price
        {
            get { return price; }
            set { price = value; }
        }
        private String time;

        public String Time
        {
            get { return time; }
            set { time = value; }
        }

        private string status;
        private string mega1;
        private string video1;
        private string image;
        private string image1;
        private string image2;


        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public Product()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Product(string id_produc, string id_account, string name, string id_maker, string type, string mega1, string video1, string add, string price, string time, string image, string image1, string image2, string status1)
        {
            // TODO: Complete member initialization
            this.id_produc = id_produc;
            this.id_account = id_account;
            this.name = name;
            this.id_maker = id_maker;
            this.type = type;
            this.mega1 = mega1;
            this.video1 = video1;
            this.addition = add;
            this.price = price;
            this.time = time;
            this.image = image;
            this.image1 = image1;
            this.image2 = image2;
            this.status = status1;
        }
    }
}