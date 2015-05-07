using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;
using Novacode;
using System.Drawing;
using GeoCV.Models;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;

namespace GeoCV.Controllers
{
    public class WordController : Controller
    {

        public ActionResult LastNed()
        {

//            CVVersjon BrukerCv = new CVVersjon();

//            if (Id.Equals(null))
//            {
//                BrukerCv = GetBrukerCv(GetAspNetBrukerID());
//            }
//            else
//            {
//                BrukerCv = db.CVVersjon.Where(x => x.CVVersjonId == Id).FirstOrDefault();
//            }

//            string FileName = BrukerCv.Person.Fornavn + " " + BrukerCv.Person.Etternavn + " - CV";

            //var FilePath = Path.Combine(Path.GetTempPath(), "Temp.docx");
            
            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);

            
            // TITTEL
            Paragraph tittel = doc.InsertParagraph();
            tittel.Append("CURRICULUM VITAE").Font(new FontFamily("Verdana")).FontSize(24).Color(Color.Black).Bold();
            tittel.Alignment = Alignment.center;

            // GEOMATIKK address
            Paragraph address = doc.InsertParagraph();
            address.Append("GEOMATIKK IKT AS, Otto Nielsens vei 12, 7052 Trondheim").Font(new FontFamily("Verdana")).FontSize(13).Color(Color.Black);
            address.Alignment = Alignment.center;

            // NAVN
            Paragraph NavnEtikett = doc.InsertParagraph();
            NavnEtikett.Append("\n\n Navn: ").Font(new FontFamily("Verdana")).FontSize(12).Color(Color.Black).Bold();
            NavnEtikett.Alignment = Alignment.left;

            // STILLING


            // NASJONALITET


            // ÅR ERFARING


            // SPRÅK


            // NØKKELKOMPETANSE


            // UTDANNELSE


            // ARBEIDSERFARING


            // PROSJEKTER


            doc.Save();

            return File(stream.ToArray(), "application/octet-stream", "FileName.docx");
        }
    }
}