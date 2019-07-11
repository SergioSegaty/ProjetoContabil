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
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            List<Cliente> clientes = clienteRepository.ObterTodos();

            ViewBag.Clientes = clientes;
            ViewBag.Categorias = categorias;



            return View();
        }

        public ActionResult Editar(int id)
        {
            

            return View();
        }



    }
}