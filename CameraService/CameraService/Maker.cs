using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameraService
{
    public class Maker
    {
        private String id_maker;

        public String Id_maker
        {
            get { return id_maker; }
            set { id_maker = value; }
        }
        private String name_maker;

        public String Name_maker
        {
            get { return name_maker; }
            set { name_maker = value; }
        }

        public Maker()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Maker(string id_maker, string name_maker)
        {
            // TODO: Complete member initialization
            this.id_maker = id_maker;
            this.name_maker = name_maker;
        }
    }
}