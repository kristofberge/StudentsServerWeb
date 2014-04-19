using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsServerWeb
{
    public class Student
    {
        public Student(long id, String name, double grade, int height, Class class_id)
        {
            this._id = id;
            this._name = name;
            this._grade = grade;
            this._height = height;
            this._class_id = class_id;
        }


        private String _name;

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _grade;

        public double grade
        {
            get { return Math.Round(_grade, 1); }
            set { _grade = value; }
        }

        private int _height;

        public int height
        {
            get { return _height; }
            set { _height = value; }
        }


        private long _id;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Class _class_id;

        public Class class_id
        {
            get { return _class_id; }
            set { _class_id = value; }
        }


        public override string ToString() { 
            return _name;
        } 
        
    }
}
