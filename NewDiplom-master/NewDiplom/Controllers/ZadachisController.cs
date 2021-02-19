using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewDiplom.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Grid;
using Syncfusion.HtmlConverter;
using NewDiplom.Models.ZadachisTreeView;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewDiplom.Controllers
{
    public class ZadachisController : Controller
    {
        private readonly DiplomContext _context;


        public ZadachisController(DiplomContext context)
        {
            _context = context;
        }

        // GET: Zadachis
        //public async Task<IActionResult> Index()
        //{
        //    //var diplomContext = _context.Zadachis.Include(z => z.Status).Include(z => z.ZadachiParent);
        //    // return View(await diplomContext.ToListAsync());
        //}
        public IActionResult Index()
        {
            return View();
        }


        public string getJsonTreeData()
        {
            // Tree nodes from db
            List<Zadachi> treeNodes;

            treeNodes = _context.Zadachis.ToList();

            var treeNodesViewModel = treeNodes
                .Where(l => l.ZadachiParentId == null)
                    .Select(l => new TreeviewNodeEntity
                    {
                        id = l.Id,
                        parentid = l.ZadachiParentId,
                        text = $"{l.Task_Name} , {l.Task_Detail}",
                        children = GetChildren(treeNodes, l.Id).ToArray()
                    }).ToList();

            return JsonSerializer.Serialize(treeNodesViewModel);
        }

        private List<TreeviewNodeEntity> GetChildren(List<Zadachi> treeNodes, int parentId)
        {
            return treeNodes
                .Where(l => l.ZadachiParentId == parentId)
                .OrderBy(l => l.Task_Name)
                .Select(l => new TreeviewNodeEntity
                {
                    id = l.Id,
                    parentid = l.ZadachiParentId,
                    text = $"{l.Task_Name} , {l.Task_Detail}",
                    children = GetChildren(treeNodes, l.Id).ToArray()
                }).ToList();
        }

        // GET: Zadachis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadachi = await _context.Zadachis
                .Include(z => z.Status)
                .Include(z => z.ZadachiParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zadachi == null)
            {
                return NotFound();
            }

            return View(zadachi);
        }

        // GET: Zadachis/Create
        public IActionResult Create(int? id)
        {
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name");
            if (id == null)
                ViewData["ZadachiParentId"] = new SelectList(_context.Zadachis, "Id", "View");
            else
                ViewData["ZadachiParentId"] = new SelectList(_context.Zadachis, "Id", "View", id);
            return View();
        }

        // POST: Zadachis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Task_Name,Task_Detail,Date_Open,Date_Close,ZadachiParentId,StatusId")] Zadachi zadachi)
        {
            _context.Statuses.Load();
            if (ModelState.IsValid)
            {
                _context.Add(zadachi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name", zadachi.StatusId);
            ViewData["ZadachiParentId"] = new SelectList(_context.Zadachis, "Id", "View", zadachi.ZadachiParentId);
            return View(zadachi);
        }

        // GET: Zadachis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadachi = await _context.Zadachis.FindAsync(id);
            if (zadachi == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name", zadachi.StatusId);
            ViewData["ZadachiParentId"] = new SelectList(_context.Zadachis, "Id", "View", zadachi.ZadachiParentId);
            return View(zadachi);
        }

        // POST: Zadachis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Task_Name,Task_Detail,Date_Open,Date_Close,ZadachiParentId,StatusId")] Zadachi zadachi)
        {
            if (id != zadachi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zadachi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZadachiExists(zadachi.Id))
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
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name", zadachi.StatusId);
            ViewData["ZadachiParentId"] = new SelectList(_context.Zadachis, "Id", "View", zadachi.ZadachiParentId);
            return View(zadachi);
        }

        // GET: Zadachis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadachi = await _context.Zadachis
                .Include(z => z.Status)
                .Include(z => z.ZadachiParent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zadachi == null)
            {
                return NotFound();
            }

            return View(zadachi);
        }

        // POST: Zadachis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {


            var zadachi = await _context.Zadachis.FindAsync(id);
            _context.Zadachis.Remove(zadachi);
            await _context.SaveChangesAsync();

            var taskDist = _context.TaskDistributions
                .Where(s => s.Zadachi.Id == id);
            _context.TaskDistributions.RemoveRange(taskDist);

            return RedirectToAction(nameof(Index));
        }

        private bool ZadachiExists(int? id)
        {
            return _context.Zadachis.Any(e => e.Id == id);
        }




        // Тут вывод в файл
        public IActionResult Export(Zadachi zadachi)
        {
            using (var workbook = new XLWorkbook())
            {
                _context.Statuses.Load();
                var worksheet = workbook.Worksheets.Add("Zadachi");
                var currentRow = 1;
                #region Header
                worksheet.ColumnWidth = 30;
                worksheet.Range("A1:F1").Style.Font.Bold = true;
                worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Range("A1:F100").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 1).Value = "Название задачи";
                worksheet.Cell(currentRow, 2).Value = "Описание задачи";
                worksheet.Cell(currentRow, 3).Value = "Дата открытия";
                worksheet.Cell(currentRow, 4).Value = "Дата закрытия";
                worksheet.Cell(currentRow, 5).Value = "Главная задача";
                worksheet.Cell(currentRow, 6).Value = "Статус";
                #endregion
                #region Body
                foreach (var zadachii in _context.Zadachis)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = zadachii.Task_Name;
                    worksheet.Cell(currentRow, 2).Value = zadachii.Task_Detail;
                    worksheet.Cell(currentRow, 3).Value = zadachii.Date_Open;
                    worksheet.Cell(currentRow, 4).Value = zadachii.Date_Close;
                    worksheet.Cell(currentRow, 5).Value = zadachii.ZadachiParent?.Task_Name;
                    worksheet.Cell(currentRow, 6).Value = zadachii.Status.Status_name;

                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Zadachi.xlsx"
                        );
                }
            }
        }


        //public async Task<IActionResult> Export(Employee employee)
        //{
        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Employee");
        //        var currentRow = 1;
        //        #region Header
        //        worksheet.Cell(currentRow, 1).Value = "Фамилия";
        //        worksheet.Cell(currentRow, 2).Value = "Имя";
        //        worksheet.Cell(currentRow, 3).Value = "Отчество";
        //        #endregion
        //        #region Body
        //        foreach (var employeee in _context.Employees)
        //        {
        //            currentRow++;
        //            worksheet.Cell(currentRow, 1).Value = employeee.Surname;
        //            worksheet.Cell(currentRow, 2).Value = employeee.Name;
        //            worksheet.Cell(currentRow, 3).Value = employeee.Second_Name;

        //        }
        //        #endregion
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(
        //                content,
        //                  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "Employee.xlsx"
        //                );
        //        }
        //    }
        //}


        public IActionResult CreateDocument()
        {
            _context.Zadachis.Load();
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();




            //Add list to IEnumerable
            IEnumerable<Zadachi> dataTable = _context.Zadachis;
            //Assign data source.
            pdfGrid.DataSource = dataTable;

            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
            //Save the PDF document to stream
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "Output.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }



    }
}
