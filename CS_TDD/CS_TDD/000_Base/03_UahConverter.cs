using System;

namespace CS_TDD._000_Base
{
    public enum Currency
    {
        UAH,
        USD,
        RUR,
        EUR
    }

    public abstract class Converter
    {
        Currency inputCurrency;
        public Currency InputCurrency { get { return InputCurrency; } }

        public abstract Currency OutputCurrency { get; set; }
        public abstract double Value { get; set; }

        public Converter(Currency inputCurrency)
        {
            this.inputCurrency = inputCurrency;
        }
    }
    
    // Тестируемый класс
    public class UahConverter : Converter
    {
        readonly double rur, usd, eur;

        double uahValue;
        Currency outputCurrency;

        public UahConverter(double rur, double eur, double usd)
            : base(Currency.UAH)
        {
            // При попыте передать в конструктор значения меньше или равных нулю будет сгенерирована исключительная ситуация типа ArgumentOutOfRangeException
            if (rur <= 0 || eur <= 0 || usd <= 0)
            {
                throw new ArgumentException("Ctor");
            }

            this.rur = rur;
            this.usd = usd;
            this.eur = eur;
        }

        public override Currency OutputCurrency
        {
            get { return outputCurrency; }

            set
            {
                outputCurrency = value;
            }
        }

        public override double Value
        {
            get
            {
                switch (outputCurrency)
                {
                    case Currency.EUR:
                        return uahValue / eur;
                    case Currency.RUR:
                        return uahValue / rur;
                    case Currency.UAH:
                        return uahValue;
                    case Currency.USD:
                        return uahValue / usd;

                    default:
                        throw new Exception();
                }
            }

            set
            {
                if (value < 0)
                {
                    // При попытке передать свойству Value значение меньше нуля так же будет сгенерированна исключительная ситуация типа ArgumentOutOfRangeException
                    throw new ArgumentException("Value", "Value");
                }
                this.uahValue = value;
            }
        }
    }
}
