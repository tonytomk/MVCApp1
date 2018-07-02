using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace MVCApp1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_post()
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee();
                TryUpdateModel<Employee>(emp);
                EmployeeBusinessLayer employeeBusiness = new EmployeeBusinessLayer();
                employeeBusiness.AddEmployee(emp);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            EmployeeBusinessLayer objEmployee = new EmployeeBusinessLayer();
            Employee employee = objEmployee.Employees.Single(emp => emp.EmployeeId == id);
            return View(employee);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {

            EmployeeBusinessLayer employeeBusinessLayer =
                new EmployeeBusinessLayer();
            // empl.Name = employeeBusinessLayer.Employees.Single(x => x.EmployeeId == empl.EmployeeId).Name;
             Employee employee = employeeBusinessLayer.Employees.Single(x => x.EmployeeId == id);

            // UpdateModel(employee,new string[] { "EmployeeId","Gender","City", "DateOfBirth" });
            // UpdateModel(employee,  null, null, new string[] { "Name" });
            UpdateModel<IEmployee>(employee);
            if (ModelState.IsValid)
            {
             
                employeeBusinessLayer.EditEmployee(employee);

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer =
                new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

    }
}