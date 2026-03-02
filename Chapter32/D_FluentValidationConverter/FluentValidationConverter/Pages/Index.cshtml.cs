using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FluentValidationConverter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IValidator<InputModel> _validator;

        [BindProperty]
        public InputModel Input { get; set; } = null!;
        public string[] Currencies { get; set; }
        public string? Results { get; set; }
        public IndexModel(ICurrencyProvider provider, IValidator<InputModel> validator)
        {
            Currencies = provider.GetCurrencies();
            _validator = validator;
        }

        public void OnGet()
        {
            Input = new InputModel
            {
                CurrencyFrom = "CAD",
                CurrencyTo = "USD",
                Quantity = 50
            };
        }
        
        public async Task OnPostAsync()
        {
            var validationResult = await _validator.ValidateAsync(Input);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                Results = "Please correct the errors";
                return;
            }

            Results = $"Converting {Input.Quantity} {Input.CurrencyFrom} to {Input.CurrencyTo}";
        }


        public class InputModel
        {
            public string CurrencyFrom { get; set; } = null!;
            public string CurrencyTo { get; set; } = null!;
            public decimal Quantity { get; set; }
        }
        
        
        public class InputValidator : AbstractValidator<InputModel>
        {
            private readonly string[] _allowedValues = { "GBP", "USD", "CAD", "EUR" };
            public InputValidator(ICurrencyProvider provider)
            {
                RuleFor(x => x.CurrencyFrom)
                    .NotEmpty()
                    .Length(3)
                    .Must(value => _allowedValues.Contains(value))
                    .WithMessage("Not a valid currency code");

                RuleFor(x => x.CurrencyTo)
                    .NotEmpty()
                    .Length(3)
                    .MustBeCurrencyCode(provider)
                    .Must((InputModel model, string currencyTo)
                        => currencyTo != model.CurrencyFrom)
                    .WithMessage("Cannot convert currency to itself");

                RuleFor(x => x.Quantity)
                    .NotNull()
                    .InclusiveBetween(1, 1000);
            }
        }
    }
}
