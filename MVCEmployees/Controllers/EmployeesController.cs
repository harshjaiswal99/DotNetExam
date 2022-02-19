using ModelBinding.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelBinding.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            //List<Employee> emps = new List<Employee>();
            //emps.Add(new Employee { EmpNo = 1, Name = "Vikram", Basic = 10000, DeptNo = 10 });
            //emps.Add(new Employee { EmpNo = 2, Name = "Shweta", Basic = 10000, DeptNo = 10 });
            //emps.Add(new Employee { EmpNo = 3, Name = "Harsh", Basic = 10000, DeptNo = 10 });
            //emps.Add(new Employee { EmpNo = 4, Name = "Ananya", Basic = 10000, DeptNo = 10 });
            //return View(emps);
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Employees ";
            List<Employee> emps = new List<Employee>();
            try
            {
                SqlDataReader dr = cmdInsert.ExecuteReader();
                while (dr.Read())
                {
                    emps.Add(new Employee { EmpNo = (int)dr["EmpNo"], Name = dr["Name"].ToString(), Basic = dr.GetDecimal(2), DeptNo = dr.GetInt32(3) });
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            cn.Close();
            return View(emps);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id=9)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Employees where EmpNo=@EmpNo";
            cmdInsert.Parameters.AddWithValue("@EmpNo", id);

            SqlDataReader dr = cmdInsert.ExecuteReader();
            Employee obj = null;
            if (dr.Read())
                obj = new Employee { EmpNo = id, Name = dr.GetString(1), Basic = dr.GetDecimal(2), DeptNo = dr.GetInt32(3) };
            else
            {
                //not found
                ViewBag.ErrorMessage = "Not found";
            }
            cn.Close();
            return View(obj);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employee obj)
        {
            try
            {
                // TODO: Add insert logic here
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJan22;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                //cmdInsert.CommandText = "insert into Employees values(4,'Pratik',50000,30)";
                cmdInsert.CommandText = "insert into Employees values(@EmpNo,@Name,@Basic,@DeptNo)";

                cmdInsert.Parameters.AddWithValue("EmpNo", obj.EmpNo);
                cmdInsert.Parameters.AddWithValue("Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("Basic", obj.Basic);
                cmdInsert.Parameters.AddWithValue("DeptNo", obj.DeptNo);
                cmdInsert.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id=0)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "select * from Employees where EmpNo=@EmpNo";
            cmdInsert.Parameters.AddWithValue("@EmpNo", id);

            SqlDataReader dr = cmdInsert.ExecuteReader();
            Employee obj = null;
            if (dr.Read())
                obj = new Employee { EmpNo = id, Name = dr.GetString(1), Basic = dr.GetDecimal(2), DeptNo = dr.GetInt32(3) };
            else
            {
                //not found
                ViewBag.ErrorMessage = "Not found";
            }
            cn.Close();
            return View(obj);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee obj)
        {
                // TODO: Add update logic here
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJan22;Integrated Security=True";
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                //cmdInsert.CommandText = "insert into Employees values(4,'Pratik',50000,30)";
                cmdInsert.CommandText = "update Employees set EmpNo=@EmpNo, Name=@Name, Basic=@Basic, DeptNo=@DeptNo where EmpNo=@EmpNo";
                cmdInsert.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Basic", obj.Basic);
                cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptNo);
            try
                {
                    cmdInsert.ExecuteNonQuery();
                    Console.WriteLine("no errros");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            cn.Close();
            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id=10)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JkJan22;Integrated Security=True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "delete from Employees where EmpNo=@EmpNo";
            cmdInsert.Parameters.AddWithValue("@EmpNo", id);
            cmdInsert.ExecuteNonQuery();
            cn.Close();
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee obj)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
