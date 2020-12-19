using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management
{
    class Student
    {
        private string name;
        private string gender;
        private string dob;
        private string address;
        private string phone;
        private string year;
        private string department;

        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Year { get => year; set => year = value; }
        public string Department { get => department; set => department = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Dob { get => dob; set => dob = value; }

        public Student(string name, string gender, string dob, string address, string phone, string year, string department)
        {
            this.Name = name;
            this.Gender = gender;
            this.Dob = dob;
            this.Address = address;
            this.Phone = phone;
            this.Year = year;
            this.Department = department;
        }
    }
}
