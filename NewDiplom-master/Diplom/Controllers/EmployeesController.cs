using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using OfficeOpenXml;
using Diplom.Data;

namespace Diplom.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            ViewData["Filter"] = new Models.EmployeeFilter.Filter();
            return View(await _context.Employees.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("FilterSurname,FilterName,FilterSecond_Name")] Models.EmployeeFilter.Filter filter)
        {
            var contextEmployees = await _context.Employees.ToListAsync();
            if (!string.IsNullOrWhiteSpace(filter.FilterSurname))
            {
                contextEmployees = contextEmployees
                    .Where(s => s.Surname.Contains(filter.FilterSurname))
                    .ToList();
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterName))
            {
                contextEmployees = contextEmployees
                    .Where(s => s.Name.Contains(filter.FilterName))
                    .ToList();
            }
            if (!string.IsNullOrWhiteSpace(filter.FilterSecond_Name))
            {
                contextEmployees = contextEmployees
                    .Where(s => s.Second_Name.Contains(filter.FilterSecond_Name))
                    .ToList();
            }
            ViewData["Filter"] = filter;
            return View(contextEmployees);
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,Second_Name,Employee_Number")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Second_Name,Employee_Number")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        // Тут импорт
        [HttpPost]
        public string Upload()
        {
            string result = "Ошибка при загрузке файла";

            try
            {
                var filePath = Path.GetTempFileName();
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        string fileName = string.Empty;
                        using (var inputStream = new FileStream(filePath, FileMode.Create))
                        {
                            // read file to stream
                            formFile.CopyTo(inputStream);
                            // stream to byte array
                            byte[] array = new byte[inputStream.Length];
                            inputStream.Seek(0, SeekOrigin.Begin);
                            inputStream.Read(array, 0, array.Length);
                            // get file name
                            fileName = formFile.FileName;
                        }
                        if (!string.IsNullOrEmpty(fileName) && System.IO.File.Exists(filePath))
                        {
                            var fileInfo = new System.IO.FileInfo(fileName);
                            var itHasBeenSaved = false;
                            if (fileInfo.Extension == ".txt")
                            {
                                using (StreamReader reader = new StreamReader(filePath))
                                {
                                    string line;
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        string[] values = line.Split('\t');
                                        if (values.Length == 4)
                                        {
                                            if (!int.TryParse(values[3], out int employee_Number))
                                                employee_Number = -1;

                                            _context.Employees.Add(new Employee()
                                            {
                                                Surname = values[0],
                                                Name = values[1],
                                                Second_Name = values[2],
                                                Employee_Number = employee_Number
                                            }
                                            );
                                            itHasBeenSaved = true;
                                        }
                                    }
                                    if (itHasBeenSaved)
                                        _context.SaveChanges();
                                }
                            }
                            else if (fileInfo.Extension == ".xlsx")
                            {
                                using (ExcelPackage package = new ExcelPackage())
                                {
                                    using (var stream = System.IO.File.OpenRead(filePath))
                                    {
                                        package.Load(stream);
                                    }

                                    var worksheet = package.Workbook.Worksheets.First();
                                    if (worksheet != null)
                                    {
                                        var currentRow = 1;

                                        var valueSurname = string.Empty;
                                        var valueName = string.Empty;
                                        var valueSecond_Name = string.Empty;
                                        int valueEmployee_Number = -1;

                                        while (true)
                                        {
                                            var valueTest = worksheet.Cells[currentRow, 1];
                                            if (valueTest != null && valueTest.Value != null)
                                            {
                                                valueSurname = valueTest.Value.ToString();
                                                valueName = worksheet.Cells[currentRow, 2].Value.ToString();
                                                valueSecond_Name = worksheet.Cells[currentRow, 3].Value.ToString();
                                                if (!int.TryParse(worksheet.Cells[currentRow, 4].Value.ToString(), out valueEmployee_Number))
                                                    valueEmployee_Number = -1;
                                                _context.Employees.Add(new Employee()
                                                {
                                                    Surname = valueSurname,
                                                    Name = valueName,
                                                    Second_Name = valueSecond_Name,
                                                    Employee_Number = valueEmployee_Number
                                                });
                                                itHasBeenSaved = true;
                                                currentRow++;
                                            }
                                            else
                                                break;
                                        }
                                        if (itHasBeenSaved)
                                            _context.SaveChanges();
                                    }
                                }
                            }
                            result = "Файл загружен";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result += $" Подробно: {ex.Message}";
            }
            return result;
        }
    }
}
