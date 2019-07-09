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
    }
}