using AdoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AdoMvc.Controllers
{
    public class StudentController:Controller
    {
       
        SqlConnection connection = new SqlConnection("Server = DESKTOP-958V6PH; Database = hello123; integrated security = SSPI;trusted_connection=true");


        public IActionResult Index()
        {
            
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * From Student", connection);
            var students = cmd.ExecuteReader();
            List<Student> studentList = new List<Student>();
            while (students.Read())
            {
                Student student = new Student();
                student.ID = Convert.ToInt32(students["Id"]);
                student.Name = Convert.ToString(students["Name"]);
                student.Email = Convert.ToString(students["Email"]);
                studentList.Add(student);
            }
            connection.Close();
            return View(studentList);
        }

        
        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO Student VALUES ('" + student.Name + "','" + student.Email + "')", connection);
            connection.Open();
            cmd1.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * From Student Where id="+id, connection);
            var  Reader = cmd.ExecuteReader();
            Student student1 = new Student();
            if (Reader.Read())
            {
                
                student1.ID = Convert.ToInt32(Reader["Id"]);
                student1.Name = Convert.ToString(Reader["Name"]);
                student1.Email = Convert.ToString(Reader["Email"]);
            }
            else
            {
                return NotFound();
            }
            connection.Close();
         return View("Edit", student1);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            SqlCommand cmd1 = new SqlCommand("UPDATE Student SET Name='"+student.Name+ "',Email='" + student.Email + "' WHERE Id="+student.ID+";", connection);
            connection.Open();
            cmd1.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Delete From Student Where id=" + id, connection);
            var Reader = cmd.ExecuteReader();
            connection.Close();
            return RedirectToAction("index");
        }

    }
}
