﻿using Eventos.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos
{
    public class Categoria : Entity<Categoria>
    {
        public Categoria(Guid id)
        {
            Id = id;
        }

        public string Nome { get; private set; }

        //EF Propriedade de navegação
        public virtual ICollection<Evento> Eventos { get; set; }

        //Construtor EF
        protected Categoria() { }

        public override bool EhValido()
        {
            return true;
        }
    }
}
