﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tomy_Chimy.Web.Data.Entities
{
    public class Order
    {
        [Key]
        public int ID_Orden { get; set; }

        /*public string Descripción { get; set; }
        public string Cantidad { get; set; }*/

        [Display(Name = "Fecha de orden")]
        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm tt}", ApplyFormatInEditMode =true)]
        public DateTime DateOrden { get; set; }

        /*[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm tt}")]
        public DateTime DateLocal => DateTime.Now;*/

        [Display(Name = "Subtotal")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Subtotal { get; set; }

        [Display(Name = "Valor de impuesto")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal ValorImpuesto { get; set; }

        [Display(Name = "Total")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Total { get; set; }


        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Anotaciones { get; set; }

    }
}
