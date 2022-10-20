using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pofront.Data;
using pofront.Models;
using pofront.Repositorio;

namespace pofront.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatorepositorio;
         
        private readonly BancoContext context;

        public ContatoController(IContatoRepositorio contatorepositorio, BancoContext context)
        {
            _contatorepositorio = contatorepositorio;
            this.context = context;
        }

        public IActionResult Index()
        {
            var contatos = _contatorepositorio.BuscarTodos();
           var consulta = context.Set<ContatoModel>().AsQueryable();
            return View(contatos);

        }
        public IActionResult Criar()
        {
            return View();

        }


        public IActionResult Editar(int id)
        {

           ContatoModel contato =  _contatorepositorio.ListarPorId(id);

            return View(contato);


        }

        public IActionResult Apagarconfirmacao(int id)
        {

            ContatoModel contato = _contatorepositorio.ListarPorId(id);
            return View(contato);


        }
        public IActionResult Apagar(int id)
        {
           try
            {

                bool apagado = _contatorepositorio.Apagar(id);

                if (apagado)
                {

                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                    


                }

                else
                {

                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu contato!";


                }

                return RedirectToAction("Index");

            }


            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu contato, mais detalhes do erro:{erro.Message}";

                return RedirectToAction("Index");


            }
        }



        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    _contatorepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");

                }

                return View(contato);

            }

            catch(System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos cadastrar seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");


            }

        }

        [HttpPost]

        public IActionResult Alterar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    _contatorepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!";
                    return RedirectToAction("Index");

                    return RedirectToAction("Index");

                }

                return View("Editar", contato);

            }


            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops não conseguimos atualizar seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");


            }



        }

    } 
    }