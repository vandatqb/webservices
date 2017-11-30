using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameraService
{
    public class ListChat
    {
        private string id;
        private string full_name;
        private string content;
        private string time;

        public string Full_name
        {
            get { return full_name; }
            set { full_name = value; }
        }
        private string image;


        public string Id
        {
            get { return id; }
            set { id = value; }
        }


        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }


        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public ListChat()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ListChat(string idaccount, string full_name, string image, string content, string time)
        {
            // TODO: Complete member initialization
            this.content = content;
            this.time = time;
            this.id = idaccount;
            this.full_name = full_name;
            this.image = image;
        }
    }

}