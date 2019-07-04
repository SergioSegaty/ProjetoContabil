using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CompraController : Controller
    {
        private CompraRepository repositorio;
        public CompraController()
        {
            repositorio = new CompraRepository();
        }
        // GET: Compra
        public ActionResult Index()
        {
            List<Compra> compras = repositorio.ObterTodos();
            ViewBag.Compras = compras;
            return View();
        }

        public ActionResult Cadastro()
        {
            CompraRepository compraRepository = new CompraRepository();
            List<Compra> compras = compraRepository.ObterTodos();
            ViewBag.Compras = compras;
            return View();
        }

        public ActionResult Store(int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Compra compra = new Compra();
            compra.IdCartao = idCartaoCredito;
            compra.Valor = valor;
            compra.DataCompra = dataCompra;
            repositorio.Inserir(compra);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Compra compra = repositorio.ObterPeloId(id);
            ViewBag.Compra = compra;
            CompraRepository compraRepository = new CompraRepository();
            List<Compra> compras = compraRepository.ObterTodos();
            ViewBag.Compra = compras;
            return View();
        }

        public ActionResult Update(int id, int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Compra compra = new Compra();
            compra.Id = id;
            compra.IdCartao = idCartaoCredito;
            compra.Valor = valor;
            compra.DataCompra = dataCompra;
            repositorio.Alterar(compra);

            return RedirectToAction("Index");

        }
    }
}