using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {

        ContaPagarRepository repository;
        ClienteRepository clienteRepository;
        CategoriaRepository categoriaRepository;

        public ContaPagarController()
        {
            repository = new ContaPagarRepository();
            clienteRepository = new ClienteRepository();
            categoriaRepository = new CategoriaRepository();
        }

        // GET: ContaPagar
        public ActionResult Index()
        {
            List<ContaPagar> contasPagar = repository.ObterTodos();
            ViewBag.ContasPagar = contasPagar;

            return View();
        }


        public ActionResult Cadastro()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos();

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos();

            return View();
        }
            

        public ActionResult Store(string nome, int IdCliente, int IdCategoria, DateTime dataPagamento, DateTime dataVencimento, decimal valor)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Nome = nome;
            contaPagar.IdCliente = IdCliente;
            contaPagar.IdCategoria = IdCategoria;
            contaPagar.DataPagamento = dataPagamento;
            contaPagar.DataVencimento = dataVencimento;
            contaPagar.Valor = valor;

            repository.Inserir(contaPagar);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContaPagar contaPagar = repository.ObterPeloId(id);

            ViewBag.ContaPagar = contaPagar;

            ClienteRepository clienteRepository = new ClienteRepository();
            ViewBag.Clientes = clienteRepository.ObterTodos();

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            ViewBag.Categorias = categoriaRepository.ObterTodos();

            return View();
        }

        public ActionResult Update(int id, string nome, int IdCliente, int IdCategoria, DateTime dataPagamento, DateTime dataVencimento, decimal valor)
        {
            ContaPagar contaPagar = repository.ObterPeloId(id);
            contaPagar.Id = id;
            contaPagar.Nome = nome;
            contaPagar.IdCliente = IdCliente;
            contaPagar.IdCategoria = IdCategoria;
            contaPagar.DataPagamento = dataPagamento;
            contaPagar.DataVencimento = dataVencimento;

            contaPagar.Valor = valor;
            repository.Alterar(contaPagar);

            return RedirectToAction("Index");
        }
    }



}
