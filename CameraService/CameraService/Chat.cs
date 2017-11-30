using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameraService
{
    public class Chat
    {
        public Chat()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string id_send;
        private string id_receive;
        private string time;
        private string content;
        private string node;


        public string Id_send
        {
            get { return id_send; }
            set { id_send = value; }
        }

        public string Id_receive
        {
            get { return id_receive; }
            set { id_receive = value; }
        }


        public string Time
        {
            get { return time; }
            set { time = value; }
        }


        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Node
        {
            get { return node; }
            set { node = value; }
        }
        public Chat(string idsend, string idreceive, String time, String content, String node)
        {
            this.id_send = idsend;
            this.id_receive = idreceive;
            this.time = time;
            this.content = content;
            this.node = node;
        }
    }
}