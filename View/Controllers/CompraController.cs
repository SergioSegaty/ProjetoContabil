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
        private CompraRepository repository;
        private CartaoCreditoRepository cartaoRepository;
        public CompraController()
        {
            repository = new CompraRepository();
            cartaoRepository = new CartaoCreditoRepository();
        }

        // GET: Compra
        public ActionResult Index()
        {
            List<Compra> compras = repository.ObterTodos();
            ViewBag.Compras = compras;
            return View();
        }

        public ActionResult Cadastro()
        {
            List<CartaoCredito> cartoesCredito = cartaoRepository.ObterTodos();
            ViewBag.Cartoes = cartoesCredito;            
            

            return View();

        }

        public ActionResult Store(int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Compra compra = new Compra();
            compra.IdCartao = idCartaoCredito;
            compra.Valor = valor;
            compra.DataCompra = dataCompra;

            repository.Inserir(compra);

            return RedirectToAction("Index");
        }
    }
}