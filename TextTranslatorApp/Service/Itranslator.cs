using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextTranslatorApp.Service
{
    /// <summary>
    /// Iranslator intrface which contains method defination for translation 
    /// </summary>
    public interface Itranslator
    {
        Task<string> Trans(string inputText, string lngcod);
    }
}
