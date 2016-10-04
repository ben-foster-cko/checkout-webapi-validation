using System;
using System.ComponentModel.DataAnnotations;

namespace APIValidationDemo
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int minValue;

        public MinValueAttribute(int minValue)
        {
            this.minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            return Convert.ToInt32(value) >= minValue;
        }
    }
}