using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FujitsuChizai.Models
{
    public static class TranslationExt
    {
        // 言語コード → 翻訳結果
        private static Dictionary<string, Dictionary<string, string>> translationCache = new Dictionary<string, Dictionary<string, string>>();
        private static ITranslator translator = new MicrosoftTranslator();

        public static PlaceMark Translate(this PlaceMark source, string languageCode)
        {
            if (source.Name == null || languageCode == "ja")
            {
                return source;
            }
            source.Name = TranslateWithCache(source.Name, languageCode);
            return source;
        }

        public static List<PlaceMark> Translate(this List<PlaceMark> source, string languageCode)
        {
            if (languageCode == "ja")
            {
                return source;
            }

            foreach (var p in source.Where(x => !String.IsNullOrEmpty(x.Name)))
            {
                p.Name = TranslateWithCache(p.Name, languageCode);
            }
            return source;
        }

        private static string TranslateWithCache(string text, string languageCode)
        {
            if (!translator.SupportedLanguageCode.Contains(languageCode))
            {
                throw new NotSupportedException("Accept-Language:" + languageCode + " は翻訳非対応の言語コードです。対応している言語コードは以下です。" + String.Join(",", translator.SupportedLanguageCode));
            }

            // 初期化
            if (!translationCache.ContainsKey(languageCode))
            {
                translationCache.Add(languageCode, new Dictionary<string, string>());
            }

            // 翻訳済み
            if (translationCache[languageCode]?.ContainsKey(text) ?? false)
            {
                return translationCache[languageCode][text];
            }
            // 翻訳してキャッシュ
            else
            {
                string translatedText = translator.Translate(text, languageCode);
                translationCache[languageCode].Add(text, translatedText);
                return translatedText;
            }
        }
    }
}