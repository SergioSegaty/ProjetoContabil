using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepository repositorio;

        public CategoriaController()
        {
            repositorio = new CategoriaRepository();
        }
        // GET: Categoria
        public ActionResult Index()
        {
            List<Categoria> categorias = repositorio.Obtertodos("");
            ViewBag.Categorias = categorias;
            return View();
        }

        public ActionResult Cadastro()
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categoria = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;
            return View();

        }

        public ActionResult Store(int idCategoria, string nome)
        {
            Categoria categoria = new Categoria();
            categoria.IdCategoria = idCategoria;
            categoria.nome = nome;
            repositorio.Inserir(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Categoria categoria = repositorio.ObterPeloId(id);
            ViewBag.Categoria = categoria;
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;
            return View();
        }

        public ActionResult Update(int id, string nome, int idCategoria)
        {
            Categoria categoria = new Categoria();
            categoria.Id = id;
            categoria.Nome = nome;
            categoria.IdCategoria = idCategoria;
            repositorio.Alterar(categoria);

            return RedirectToAction("Index");
        }

    }
}