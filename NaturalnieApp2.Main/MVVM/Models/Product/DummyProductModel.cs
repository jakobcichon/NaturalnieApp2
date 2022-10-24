﻿namespace NaturalnieApp2.Main.MVVM.Models.Product
{
    using NaturalnieApp2.Common.Binding;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using static NaturalnieApp2.Common.Attributes.DisplayableModel.DisplayableModel;

    public record DummyProductModel: ValidatableBindableRecordBase
    {
        private string name;
        private double price;
        private int tax;

        [RegexStringValidator("*")]
        [StringLength(5)]
        [CanBeDisplayed]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        [CanBeDisplayed]
        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        [CanBeDisplayed]
        public int Tax
        {
            get { return tax; }
            set { SetProperty(ref tax, value); }
        }

    }
}
