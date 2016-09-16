using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujitsuChizai.Models
{
    interface ITranslator
    {
        List<string> SupportedLanguageCode { get; }

        string Translate(string text, string languageCodeForTranslate);
    }
}
