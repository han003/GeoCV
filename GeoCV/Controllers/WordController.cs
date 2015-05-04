using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;
using Novacode;
using System.Drawing;

namespace GeoCV.Controllers
{
    public class WordController : Controller
    {
        public ActionResult LastNed()
        {
            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);

            Paragraph par = doc.InsertParagraph();
            par.Append("This is a dummy test").Font(new FontFamily("Times New Roman")).FontSize(32).Color(Color.Blue).Bold();

            doc.Save();

            return File(stream.ToArray(), "application/octet-stream", "FileName.docx");
        }
    }
}