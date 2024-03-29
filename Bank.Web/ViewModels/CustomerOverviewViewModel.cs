﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Web.ViewModels
{
    public class CustomerOverviewViewModel
    {       
        public bool CustomerChanged { get; set; }
        
        public int CustomerId { get; set; }
        public string UniqueId { get; set; }
        [Required]
        [StringLength(6)]
        public string Gender { get; set; }
        [Required]
        [StringLength(100)]
        public string Givenname { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [StringLength(100)]
        public string Streetaddress { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(15)]
        public string Zipcode { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; }
        [StringLength(10)]
        public string Telephonecountrycode { get; set; }
        [StringLength(25)]
        public string Telephonenumber { get; set; }
        [StringLength(100)]
        public string Emailaddress { get; set; }

        public decimal TotalAmount { get; set; }

        public List<int> Accounts { get; set; }
    }
}
