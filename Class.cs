﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsServerWeb
{
    public class Class
    {

        public Class(int id, String name) {
            this._id = id;
            this._name = name;
        }


        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override string ToString()
        {
            return this._name;
        }
        
    }
}
