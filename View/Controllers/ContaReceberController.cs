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
            ViewBag.Clientes = clienteRepository.ObterTodos();

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos();

            return View();
        }

        public ActionResult Store (string nome, int IdCliente, int IdCategoria, DateTime dataPagamento, decimal valor)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Nome = nome;
            contaReceber.IdCliente = IdCliente;
            contaReceber.IdCategoria = IdCategoria;
            contaReceber.DataPagamento = dataPagamento;
            contaReceber.Valor = valor;

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
            ViewBag.Clientes = clienteRepository.ObterTodos();

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos();

            return View();
        }

        public ActionResult Update(int id, string nome, int IdCliente, int IdCategoria, DateTime dataPagamento, decimal valor)
        {
            ContaReceber contaReceber = repositorio.ObterPeloId(id);
            contaReceber.Id = id;
            contaReceber.Nome = nome;
            contaReceber.IdCliente = IdCliente;
            contaReceber.IdCategoria = IdCategoria;
            contaReceber.DataPagamento = dataPagamento;
            contaReceber.Valor = valor;
            repositorio.Alterar(contaReceber);

            return RedirectToAction("Index");
        }
    }
}