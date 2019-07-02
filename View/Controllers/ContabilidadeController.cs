using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace View.Controllers
{
    public class ContabilidadeController : Controller
    {
        private ContabilidadeRepository repository;

        public Contabilidadecontroller()
        {
            repositoru = new ContabilidadeRepository();
        }

        //GET : Contabilidade 
        public ActionResult Index()
        {
            List<Contabilidade> contabilidades = repository.ObterTodos();
            ViewBag.Contabilidadex = contabilidades;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Nome = nome;
            repository.Inserir(contabilidade);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContabilidadeController contabilidade = repostiory.ObterPeloId(id);
            ViewBag.Contabilidade = contabilidade;
            return View();

        }

        public ActionResult Update(int id, string nome)
        {
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Id = id;
            contabilidade.Nome = nome;
            repository.Alterar(categoria);
            return RedirectToAction("Index");
        }
    }
}