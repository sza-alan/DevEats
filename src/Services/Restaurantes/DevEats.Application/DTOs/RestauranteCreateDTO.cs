﻿using System.ComponentModel.DataAnnotations;

namespace DevEats.Application.DTOs
{
    public class RestauranteCreateDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public string Cnpj { get; set; }
    }
}
