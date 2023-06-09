﻿using MeLevaAi.Api.Domains;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using MeLevaAi.Api.Validations;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class VeiculoDto : Notifiable
    {
        public Guid Id { get; set; }

        public Guid? MotoristaId { get; set; }

        public string Placa { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public int Ano { get; set; }

        public string Cor { get; set; }

        public string FotoUrl { get; set; }

        public int QuantidadeDeLugares { get; set; }

        public Categoria Categoria { get; set; }
    }
}