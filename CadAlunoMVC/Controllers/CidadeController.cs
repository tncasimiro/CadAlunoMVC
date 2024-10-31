using CadAlunoMVC.DAO;
using CadAlunoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace CadAlunoMVC.Controllers
{
    public class CidadeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var dao = new CidadeDAO();
                var lista = dao.Listagem();
                return View("index", lista);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                var cidade = new CidadeViewModel();
                var dao = new CidadeDAO();
                cidade.Id = dao.ProximoId();
                return View("Form", cidade);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }


        public IActionResult Salvar(CidadeViewModel c,
                                    string Operacao)
        {
            try
            {
                ValidaDados(c, Operacao);
                if (ModelState.IsValid)
                {
                    var dao = new CidadeDAO();
                    if (Operacao == "I")
                        dao.Insert(c);
                    else
                        dao.Update(c);

                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", c);
                }
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(CidadeViewModel c, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            var dao = new CidadeDAO();
            if (c.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(c.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(c.Id) == null)
                    ModelState.AddModelError("Id", "Código não existe.");
            }

            if (string.IsNullOrEmpty(c.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var dao = new CidadeDAO();
                dao.Delete(id);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }


        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                var dao = new CidadeDAO();
                var c = dao.Consulta(id);
                if (c == null)
                    return RedirectToAction("index");
                else
                    return View("Form", c);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
