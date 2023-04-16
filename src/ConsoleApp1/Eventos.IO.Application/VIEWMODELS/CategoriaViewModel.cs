using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eventos.IO.Application.VIEWMODELS
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public SelectList Categorias()
        {
            return new SelectList(ListarCategorias(), "Id", "Nome");
        }

        public List<CategoriaViewModel> ListarCategorias()
        {
            return new List<CategoriaViewModel>()
            {
                new CategoriaViewModel() {Id = new Guid("6b96e4dc-e29c-4aac-86c6-77236f514eaa"), Nome = "Congresso"},
                new CategoriaViewModel() {Id = new Guid("4fb70bd2-95a9-4c66-b224-c339175cce63"), Nome = "Metup"},
                new CategoriaViewModel() {Id = new Guid("8999fb1e-ce8a-42df-b730-adac44edfb3b"), Nome = "Workshop"}
            };
        }
    }
}