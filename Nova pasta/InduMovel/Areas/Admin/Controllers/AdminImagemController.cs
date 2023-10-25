using InduMovel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InduMovel.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]

    public class AdminImagemController : Controller
    {
        private readonly ConfiguraImagem _confImg;
        private readonly IWebHostEnvironment _hostingEnvireoment;

        public AdminImagemController(IOptions<ConfiguraImagem> confImg, IWebHostEnvironment hostingEnvireoment)
        {
            _confImg = confImg.Value;
            _hostingEnvireoment = hostingEnvireoment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFile(List<IFormFile> files){
             
             if( files == null || files.Count ==0){
                ViewData["Erro"] = "Erro: Nenhum arquivo selecionado";
                return View(ViewData);
             }
             if(files.Count >10){
                 ViewData["Erro"] = "Erro: Quantidade de arquivos selecionados excedido";
                 return View(ViewData);
             }
           try{
             long size = files.Sum(f => f.Length);
             var filePathName = new List<string>();
             var filePath = Path.Combine(_hostingEnvireoment.WebRootPath, _confImg.NomePastaImagemItem);

             foreach(var formFile in files){
                  if(formFile.FileName.Contains(".jpg")|| formFile.FileName.Contains(".gif")||formFile.FileName.Contains(".svg")|| formFile.FileName.Contains(".png"))
                  {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    filePathName.Add(fileNameWithPath);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create)){
                            await formFile.CopyToAsync(stream);
                    }
                  }
             }
             ViewData["Resultado"] = $"{files.Count} enviados para o servidor. Com um total de {size} bytes enviados.";
            ViewBag.Arquivos = filePathName;
            return View(ViewData);
           }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Erro : {ex.Message}";
                return View(ViewData);
            }

        }

        public IActionResult GetImagens()
        {
            GerenciaArquivoModel model = new GerenciaArquivoModel();

            try
            {
                var userImagesPath = Path.Combine(_hostingEnvireoment.WebRootPath,
                     _confImg.NomePastaImagemItem);

                DirectoryInfo dir = new DirectoryInfo(userImagesPath);
                FileInfo[] files = dir.GetFiles();
                model.PathImagemItem = _confImg.NomePastaImagemItem;

                if (files.Length == 0)
                {
                    ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
                }
               model.File = files;
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Erro : {ex.Message}";
            }
            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            try
            {
                string _imagemDeleta = Path.Combine(_hostingEnvireoment.WebRootPath,
                _confImg.NomePastaImagemItem+ "\\", fname);

                if ((System.IO.File.Exists(_imagemDeleta)))
                {
                    System.IO.File.Delete(_imagemDeleta);
                    ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
                }
            }
            catch (Exception ex)
            {
                ViewData["Erro"] = $"Erro : {ex.Message}";
            }
            return View("index");
        }

        

    }
}