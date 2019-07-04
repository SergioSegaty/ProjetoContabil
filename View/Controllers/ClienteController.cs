using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {

        private ClienteRepository clienteRepository;
        private ContabilidadeRepository contabilidadeRepository;

        public ClienteController()
        {
            clienteRepository = new ClienteRepository();
            contabilidadeRepository = new ContabilidadeRepository();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> clientes = clienteRepository.ObterTodos();

            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Cadastro()
        {
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }

        public ActionResult Store(int IdContabilidade, string nome, string cpf)
        {
            Cliente cliente = new Cliente();

            cliente.IdContabilidade = IdContabilidade;
            cliente.Cpf = cpf;
            cliente.Nome = nome;

            clienteRepository.Inserir(cliente);

            return RedirectToAction("Index");

        }

        public ActionResult Editar(int id)
        {
            Cliente cliente = clienteRepository.ObterPeloId(id);
            ViewBag.Cliente = cliente;

            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;

            return View();
        }


        public ActionResult Update(int id, int IdContabilidade, string nome, string cpf)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.IdContabilidade = IdContabilidade;
            cliente.Cpf = cpf;

            clienteRepository.Alterar(cliente);


            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            clienteRepository.Apagar(id);

            return RedirectToAction("Index");
        }
    }
}