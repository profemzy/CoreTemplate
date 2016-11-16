using System;
using System.Diagnostics;
using System.IO;
using CoreTemplate.Contracts;
using CoreTemplate.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreTemplate.Controllers
{
    [Authorize]
    public class ArchivesController : Controller
    {
        private readonly IArchiveData _archiveData;

        public ArchivesController(IArchiveData archiveData)
        {
            _archiveData = archiveData;
        }

      [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _archiveData.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Archive model)
        {
            if (ModelState.IsValid)
            {
                var archive = new Archive
                {
                    Name = model.Name,
                    CaseDate = model.CaseDate,
                    FilePath = model.FilePath
                };

                // Specify a name for your top-level folder.
                const string folderName = @"c:\OndoLaw";

                // To create a string that specifies the path to a subfolder under your 
                // top-level folder, add a name for the subfolder to folderName.
                var pathString = Path.Combine(folderName, "CASE" + Guid.NewGuid() + archive.Name);
                
                archive.FilePath = pathString;
                _archiveData.Add(archive);

                // Create the subfolder. You can verify in File Explorer that you have this
                // structure in the C: drive.
                //    Local Disk (C:)
                //        Top-Level Folder
                //            SubFolder
                Directory.CreateDirectory(pathString);
                _archiveData.Commit();

                return RedirectToAction("Details", new { id = archive.Id });
            }
            return View();
        }

        public IActionResult OpenFile(int id)
        {
            var archive = _archiveData.Get(id);
            if (archive == null)
            {
                return NotFound();
            }
            Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", archive.FilePath);
            return View("details", archive);
        }

        public IActionResult Details(int id)
        {
            var model = _archiveData.Get(id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
       
        public IActionResult Edit(int id)
        {
            var archive = _archiveData.Get(id);
            if (archive == null)
            {
                return RedirectToAction("Index");
            }
            return View(archive);
        }

            [HttpPost]
        public IActionResult Edit(int id, Archive inputArchive)
            {
                var archive = _archiveData.Get(id);
          
                if ( archive != null && ModelState.IsValid)
                {
                    archive.Name = inputArchive.Name;
                    archive.CaseDate = inputArchive.CaseDate;
                    archive.FilePath = inputArchive.FilePath;

                    _archiveData.Commit();
                    return RedirectToAction("Details", new { id = archive.Id });
            }
                return View(archive);

        }

        public IActionResult Delete(int id)
        {
            var archive = _archiveData.Get(id);
            if (archive == null)
            {
                return NotFound();
            }

            return View(archive);
        }

        [HttpPost]
        public IActionResult Delete(int id, Archive archive)
        {
            if (archive == null)
            {
                return RedirectToAction("Index");
            }
            _archiveData.Delete(archive);
            _archiveData.Commit();
            return RedirectToAction("Index");
        }

    }
}
