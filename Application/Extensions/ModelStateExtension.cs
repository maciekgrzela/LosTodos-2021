using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Extensions
{
    public static class ModelStateExtension
    {
        public static string GetErrors(this ModelStateDictionary dictionary)
        {
            return string.Join(",", dictionary.SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToList());
        }
    }
}