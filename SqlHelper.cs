using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StudentsServerWeb
{
    class SqlHelper
    {
        private StudentsDatabase _db;

        public static readonly String TBL_STUDENTS = "tbl_students";
        public static readonly String TBL_CLASSES = "tbl_classes";

        public static readonly String CLASS_ID = "class_id";
        public static readonly String CLASS_NAME = "class_name";

        public static readonly String STUD_ID = "stud_id";
        public static readonly String STUD_NAME = "stud_name";
        public static readonly String STUD_GRADE = "stud_grade";
        public static readonly String STUD_HEIGHT = "stud_height";

        public SqlHelper(StudentsDatabase db) {
            this._db = db;
        }

  

        public List<Class> GetClasses() {
            List<Class> classList = new List<Class>();
            String sql = "SELECT * FROM " + TBL_CLASSES + ";";

            DataTableReader records = _db.ExecuteSelect(sql);

            while (records.Read()) {
                classList.Add(new Class(Convert.ToInt32(records[CLASS_ID]), (String) records[CLASS_NAME]));
            }
            records.Close();

            return classList;
        }


        public List<Student> GetStudentsInClass(Class fromClass)
        {
            List<Student> studentsList = new List<Student>();
            String sql = "SELECT * FROM " + TBL_STUDENTS
                + " WHERE " + CLASS_ID + "=" + fromClass.id + ";";
            DataTableReader records = _db.ExecuteSelect(sql);

            while (records.Read())
            {
                studentsList.Add(new Student(
                    Convert.ToInt32(records[STUD_ID]),
                    (String)records[STUD_NAME],
                    Convert.ToDouble(records[STUD_GRADE]),
                    Convert.ToInt32(records[STUD_HEIGHT]),
                    fromClass
                    )); 
            }
            records.Close();

            return studentsList;
        }

        public bool Add(Class newClass) {
            string sql = "INSERT INTO " + TBL_CLASSES
                + " (" + CLASS_NAME + ") "
                + "VALUES ('" + newClass.name + "');";
            return _db.ExecuteInsertUpdateDelete(sql);
        }

        public bool Add(Student newStudent) {
            string gradeString = newStudent.grade.ToString().Replace(',', '.');
            string sql = "INSERT INTO " + TBL_STUDENTS
                + " (" + STUD_NAME + ", " + STUD_GRADE + ", " + STUD_HEIGHT + ", " + CLASS_ID + ") "
                + "VALUES('" + newStudent.name + "', " + ConvertGrade(newStudent.grade) + ", " + newStudent.height + ", " + newStudent.class_id.id + ");";
            return _db.ExecuteInsertUpdateDelete(sql);
        }

        public bool Edit(Class editClass) {
            string sql = "UPDATE " + TBL_CLASSES + " SET "
                + CLASS_NAME + "='" + editClass.name + "' "
                + "WHERE " + CLASS_ID + "=" + editClass.id + ";";
            return _db.ExecuteInsertUpdateDelete(sql);
        }

        public bool Edit(Student editStudent)
        {
            string sql = "UPDATE " + TBL_STUDENTS + " SET "
                + STUD_NAME + "='" + editStudent.name + "', "
                + STUD_HEIGHT + "=" + editStudent.height + ", "
                + STUD_GRADE + "=" + ConvertGrade(editStudent.grade) + ", "
                + CLASS_ID + "=" + editStudent.class_id.id
                + " WHERE " + STUD_ID + "=" + editStudent.id + ";";
            return _db.ExecuteInsertUpdateDelete(sql);
        }

        public bool Delete(Class editClass) 
        {
            string sql = "DELETE FROM " + TBL_CLASSES
                + " WHERE " + CLASS_ID + "=" + editClass.id + ";";
            return _db.ExecuteInsertUpdateDelete(sql);
        }

        public bool Delete(Student editStudent)
        {
            string sql = "DELETE FROM " + TBL_STUDENTS
                + " WHERE " + STUD_ID + "=" + editStudent.id + ";";
            return _db.ExecuteInsertUpdateDelete(sql);
        }

        private string ConvertGrade(double g){
            return g.ToString().Replace(',', '.');
        }
    }
}
