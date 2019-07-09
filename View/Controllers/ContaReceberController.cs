using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaReceberController : Controller
    {
        private ContaReceberRepository repositorio;

        public ContaReceberController()
        {
            repositorio = new ContaReceberRepository();
        }

        // GET: ContaReceber

        public ActionResult Index()
        {
            List<ContaReceber> contasReceber = repositorio.ObterTodos();
            ViewBag.ContasReceber = contasReceber;
            return View();
        }

        public ActionResult Cadastro()
        {
            ContaReceberRepository contaReceberRepository = new ContaReceberRepository();
            ViewBag.ContaReceber = contaReceberRepository.ObterTodos();

            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Cliente = clienteRepository.ObterTodos();

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categoria = categoriaRepository.ObterTodos();

            return View();
        }

        public ActionResult Store (string nome)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Nome = nome;
            repositorio.Inserir(contaReceber);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContaReceber contaReceber = repositorio.ObterPeloId(id);
            ViewBag.ContaReceber = contaReceber;

            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Cliente = clienteRepository.ObterTodos();

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categoria = categoriaRepository.ObterTodos();

            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            ContaReceber contaReceber = repositorio.ObterPeloId(id);
            contaReceber.Id = id;
            contaReceber.Nome = nome;
            repositorio.Alterar(contaReceber);

            return RedirectToAction("Index");
        }
    }
}