using Microsoft.AspNetCore.Mvc;
using WebApplicationMaxim.DAL;
using WebApplicationMaxim.Models;
using WebApplicationMaxim.ViewModel.Service;

namespace WebApplicationMaxim.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class ServiceController : Controller
    {

             
        

            AppDbContext _context;
            private readonly IWebHostEnvironment _environment;

            public ServiceController(AppDbContext context, IWebHostEnvironment environment)
            {
                _context = context;
                _environment = environment;
            }

            public IActionResult Index()
            {
                var services = _context.Services.ToList();
                return View(services);
            }

            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Create(CreateServicesVm servicevm)
            {

                if (!servicevm.ImgFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImgFile", "Duzgun format daxil edin");
                    return View();
                }
              
                string path = _environment.WebRootPath + @"\Upload\Service\";
                string filename = Guid.NewGuid() + servicevm.ImgFile.FileName;


                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    servicevm.ImgFile.CopyTo(stream);
                }









                if (!ModelState.IsValid)
                {
                    return View();
                }
                Services service = new Services()
                {
                    Name = servicevm.Name,
                    Description = servicevm.Description,
                    ImgUrl = filename,
                };
                _context.Services.Add(service);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            public IActionResult Delete(int id)
            {
                var service = _context.Services.FirstOrDefault(x => x.Id == id);

                if (service != null)
                {
                    string path = _environment.WebRootPath + @"\Upload\Service\" + service.ImgUrl;
                    FileInfo fileInfo = new FileInfo(path);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                    _context.Services.Remove(service);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();

            }
            public IActionResult Update(int id)
            {
               Services service = _context.Services.FirstOrDefault(X => X.Id == id);
                UpdateServicesVm servicevm = new UpdateServicesVm()
                {
                    Id = service.Id,
                    Name= service.Name,
                    Description = service.Description,
                    ImgUrl = service.ImgUrl
                };
                if (service == null)
                {
                    return RedirectToAction("Index");
                }
                return View(servicevm);
            }
            [HttpPost]
            public IActionResult Update(UpdateServicesVm service)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var oldservice = _context.Services.FirstOrDefault(x => x.Id == service.Id);
                if (oldservice == null) { return RedirectToAction("Index"); }
                oldservice.Name = service.Name;
                oldservice.Description = service.Description;
                oldservice.ImgUrl = service.ImgUrl;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }

