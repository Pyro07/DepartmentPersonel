using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Helper
{
    public static class FriendlyURLTitle
    {
        public static string FriendlyUrlTitle(this UrlHelper urlHelper, string inComingText)
        {
            if(inComingText != null)
            {
                inComingText = inComingText.Replace("ş", "s");
                inComingText = inComingText.Replace("Ş", "s");
                inComingText = inComingText.Replace("İ", "i");
                inComingText = inComingText.Replace("I", "i");
                inComingText = inComingText.Replace("ı", "i");
                inComingText = inComingText.Replace("ö", "o");
                inComingText = inComingText.Replace("Ö", "o");
                inComingText = inComingText.Replace("ü", "u");
                inComingText = inComingText.Replace("Ü", "u");
                inComingText = inComingText.Replace("Ç", "c");
                inComingText = inComingText.Replace("ç", "c");
                inComingText = inComingText.Replace("ğ", "g");
                inComingText = inComingText.Replace("Ğ", "g");
                inComingText = inComingText.Replace(" ", "-");
                inComingText = inComingText.Replace("---", "-");
                inComingText = inComingText.Replace("?", "");
                inComingText = inComingText.Replace("/", "");
                inComingText = inComingText.Replace(".", "");
                inComingText = inComingText.Replace("'", "");
                inComingText = inComingText.Replace("#", "");
                inComingText = inComingText.Replace("%", "");
                inComingText = inComingText.Replace("&", "");
                inComingText = inComingText.Replace("*", "");
                inComingText = inComingText.Replace("!", "");
                inComingText = inComingText.Replace("@", "");
                inComingText = inComingText.Replace("+", "");
                inComingText = inComingText.ToLower();
                inComingText = inComingText.Trim();

                string encodedUrl = (inComingText ?? "").ToLower();//tüm harfleri küçült
                encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");//& ile " " yer değiştir
                encodedUrl = encodedUrl.Replace("'", "");//" " karakterleri silme
                encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");//geçersiz karakterleri sil
                encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");//tekrar edenleri sil
                encodedUrl = encodedUrl.Trim('-');//karakterlerin arasına tire koy
                return encodedUrl;
            }
            else
            {
                return "";
            }

        }
    }
}