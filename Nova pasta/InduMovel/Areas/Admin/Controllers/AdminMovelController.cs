using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InduMovel.Context;
using InduMovel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.Extensions.Options;
using NuGet.DependencyResolver;

namespace InduMovel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMovelController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ConfiguraImagem _confImg;
        private readonly IWebHostEnvironment _hostingEnvireoment;

        public AdminMovelController(AppDbContext context, IOptions<ConfiguraImagem> confImg, IWebHostEnvironment hostingEnvireoment)
        {
            _context = context;
            _confImg = confImg.Value;
            _hostingEnvireoment = hostingEnvireoment;
        }

        // GET: Admin/AdminMovel
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Moveis.Include(m => m.Categoria);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminMovel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Moveis == null)
            {
                return NotFound();
            }

            var movel = await _context.Moveis
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.MovelId == id);
            if (movel == null)
            {
                return NotFound();
            }

            return View(movel);
        }

        // GET: Admin/AdminMovel/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "Nome");
            ViewBag.Arquivos = GetFilesCad();
            ViewData["Path"] = Path.Combine(_hostingEnvireoment.WebRootPath,
                     _confImg.NomePastaImagemItem);
            return View();
        }

        // POST: Admin/AdminMovel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovelId,Nome,Cor,Descricao,ImagemUrl,ImagemCurta,Valor,EmProducao,Promocao,CategoriaId")] Movel movel, IFormFile Imagem, IFormFile Imagemcurta)
        {


           if(Imagem != null){
             string imagemr = await SalvarArquivo(Imagem);
             movel.ImagemUrl = imagemr;
           }
            
        if(Imagemcurta != null){
            string imagemcr = await SalvarArquivo(Imagemcurta);
            movel.ImagemCurta = imagemcr;
        }
                


                if (ModelState.IsValid)
                {
                    _context.Add(movel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } 
            
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "Nome", movel.CategoriaId);
            return View(movel);
        }

        // GET: Admin/AdminMovel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Moveis == null)
            {
                return NotFound();
            }

            var movel = await _context.Moveis.FindAsync(id);
            if (movel == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "Nome", movel.CategoriaId);
            return View(movel);
        }

        // POST: Admin/AdminMovel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovelId,Nome,Cor,Descricao,ImagemUrl,ImagemCurta,Valor,EmProducao,Promocao,CategoriaId")] Movel movel, IFormFile Imagem, IFormFile Imagemcurta)
        {
            if (id != movel.MovelId)
            {
                return NotFound();
            }

            

            if(Imagem != null){
             Deletefile(movel.ImagemUrl);
             string imagemr = await SalvarArquivo(Imagem);
             movel.ImagemUrl = imagemr;
           }
            
           if(Imagemcurta != null){
            Deletefile(movel.ImagemCurta);
            string imagemcr = await SalvarArquivo(Imagemcurta);
            movel.ImagemCurta = imagemcr;
           }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(movel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MovelExists(movel.MovelId))
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
                 
            
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "Nome", movel.CategoriaId);
            return View(movel);
        }

        // GET: Admin/AdminMovel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Moveis == null)
            {
                return NotFound();
            }

            var movel = await _context.Moveis
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.MovelId == id);
            if (movel == null)
            {
                return NotFound();
            }

            return View(movel);
        }

        // POST: Admin/AdminMovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Moveis == null)
            {
                return Problem("Entity set 'AppDbContext.Moveis'  is null.");
            }
            var movel = await _context.Moveis.FindAsync(id);
            if (movel != null)
            {
                Deletefile(movel.ImagemCurta);
                Deletefile(movel.ImagemUrl);
                _context.Moveis.Remove(movel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovelExists(int id)
        {
            return _context.Moveis.Any(e => e.MovelId == id);
        }

        public List<string> GetFilesCad()
        {

            try
            {
                var userImagesPath = Path.Combine(_hostingEnvireoment.WebRootPath,
                     _confImg.NomePastaImagemItem);

                DirectoryInfo dir = new DirectoryInfo(userImagesPath);
                FileInfo[] files = dir.GetFiles();
                List<string> arquivos = new List<string>();

                if (files.Length == 0)
                {
                    return null;
                }
                else
                {
                    foreach (var file in files)
                    {
                        arquivos.Add(_confImg.NomePastaImagemItem + "/" + file.Name);
                    }
                }
                return arquivos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<string> SalvarArquivo(IFormFile Imagem)
        {
            var filePath = Path.Combine(_hostingEnvireoment.WebRootPath, _confImg.NomePastaImagemItem);
            if (Imagem.FileName.Contains(".jpg") || Imagem.FileName.Contains(".gif") || Imagem.FileName.Contains(".svg") || Imagem.FileName.Contains(".png"))
            {
                string novoNome = $"{Guid.NewGuid()}.{Path.GetExtension(Imagem.FileName)}";
                var fileNameWithPath = string.Concat(filePath, "\\", novoNome);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                     await Imagem.CopyToAsync(stream);
                }
                return "~/" + _confImg.NomePastaImagemItem + "/" + novoNome;
            }
            return null;
        }

         public void Deletefile(string fname)
        {
            if (fname != null){

            
            int pi = fname.LastIndexOf("/")+1;
            int pf = fname.Length - pi;
            string nomearquivo = fname.Substring(pi,pf);
            try
            {
                string _imagemDeleta = Path.Combine(_hostingEnvireoment.WebRootPath,
                _confImg.NomePastaImagemItem+ "\\", nomearquivo);

                if ((System.IO.File.Exists(_imagemDeleta)))
                {
                    System.IO.File.Delete(_imagemDeleta);
                }
                Console.WriteLine(_imagemDeleta+"oioooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
            }
            catch (Exception)
            {
               throw;
            }
            }
            
        }
    }
}
