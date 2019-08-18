﻿using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class ProfitValidator : IValidator<Product>
    {
        public bool Validate(Product model)
        {
            return model.Price - model.Cost >= 0;
        }
    }

    public class ProductPriceValidator : IValidator<Product>
    {
        public bool Validate(Product model)
        {
            return model.Price > 0;
        }
    }

    [TestFixture()]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"validator1", new ProfitValidator()},
                {"validator2", new ProductPriceValidator()},
                {"model", new Product {Price = 100, Cost = 111}},
            };

            var validators = arguments.Values.JoeyOfType<IValidator<Product>>();

            var product = arguments.Values.JoeyOfType<Product>().Single();

            var isValid = validators.All(x => x.Validate(product));

            Assert.IsFalse(isValid);
            Assert.AreEqual(2, validators.Count());
        }
    }
}