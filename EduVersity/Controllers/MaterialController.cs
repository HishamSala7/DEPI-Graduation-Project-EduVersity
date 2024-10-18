using EduVersity.Data.Models;
using EduVersity.Managers.MaterialManager;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Material;
using EduVersity.ViewModels.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EduVersity.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialManager _materialManager;

        public MaterialController(IMaterialManager materialManager)
        {
            _materialManager = materialManager;
        }
        public IActionResult Index(int CourseId, string CourseName)
        {
            var material = _materialManager.GetAllMaterials(CourseId);
            ViewBag.CourseId = CourseId;    
            ViewBag.CourseName = CourseName;
            return View(material);
        }

        [HttpGet]
        public IActionResult AddMaterial(int CourseId, string CourseName)
        {
            ViewBag.CourseId = CourseId;
            ViewBag.CourseName = CourseName;
            return View();
        }
        public async Task<IActionResult> AddMaterial(MaterialAddVM model, IFormFile FileLink,
            int CourseId, string CourseName)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload and convert to byte[]
                if (FileLink != null && FileLink.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await FileLink.CopyToAsync(memoryStream);
                        model.FileContent = memoryStream.ToArray(); // Convert file to byte[]
                    }

                }
                else
                {
                    ViewBag.CourseId = CourseId;
                    ViewBag.CourseName = CourseName;
                    return View(model);
                }

                _materialManager.Add(model);
                return Redirect($"~/Material/Index?CourseId={CourseId}&CourseName={CourseName}");
            }

            ViewBag.CourseId = CourseId;
            ViewBag.CourseName = CourseName;
            return View(model);
        }

        //public IActionResult Details()
        //{
        // var material = _materialManager.GetAllMaterials();
        // return View(material);
        //}




        //[HttpGet]
        //public IActionResult EditMaterial(int Id)
        //{
        //    var material = _materialManager.GetMaterialById(Id);
        //    if (material == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.depts = _materialManager.GetAllMaterials();
        //    return View(material);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditMaterial(int Id, MaterialEditVM material, IFormFile? file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Fetch existing material from the database
        //        var existingMaterial = _materialManager.GetMaterialById(Id);

        //        // If a new file is uploaded, replace the old one
        //        if (file != null && file.Length > 0)
        //        {
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                file.CopyTo(memoryStream);
        //                material.FileContent = memoryStream.ToArray(); // Save the new file content
        //            }
        //        }
        //        else
        //        {
        //            // Keep the old file content if no new file is uploaded
        //            material.FileContent = existingMaterial.FileContent;
        //        }

        //        // Update the material
        //        _materialManager.Update(material);

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.depts = _materialManager.GetAllMaterials();
        //    return View(material);
        //}




        //[HttpGet]
        //public IActionResult Delete(int Id)
        //{
        //    var material = _materialManager.GetMaterialById(Id);
        //    return View(material);
        //}

        //public IActionResult DeleteMaterial(int Id)
        //{
        //    _materialManager.Delete(Id);
        //    return View();
        //}

        //public async Task<IActionResult> DownloadFile(int id)
        //{
        //    // Get the material by id
        //    var material = _materialManager.GetMaterialById(id);

        //    if (material == null || string.IsNullOrEmpty(material.FileLink))
        //    {
        //        return NotFound();
        //    }

        //    // Physical path to the file
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", material.FileLink.TrimStart('/'));

        //    // Check if the file exists
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound();
        //    }

        //    // Get the content type (for PDF)
        //    string contentType = "application/pdf";

        //    // Download the file
        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    return File(fileBytes, contentType, Path.GetFileName(filePath));
        //}

    }

}

