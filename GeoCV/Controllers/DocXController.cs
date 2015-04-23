using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Novacode;

namespace GeoCV.Controllers
{
    public class DocXController : Controller
    {
        public ActionResult Download()
        {

            // SKRIV WORD KODE HER





            // Returnere bare 'null' midlertidig, men d e her du ska returner docx fila
            return null;

            // Sån her ser d ut når æ returnere en pdf fil, men vet ikke om d e d samme som word fila
            // return File(Bytes, "application/pdf", FileName + ".pdf");
        }
    }
}