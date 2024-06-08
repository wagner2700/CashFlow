﻿using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMidleware
    {
        private readonly RequestDelegate _next;

        public CultureMidleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("pt-BR");

            if(string.IsNullOrEmpty(requestedCulture) == false
                && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture))) 
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
