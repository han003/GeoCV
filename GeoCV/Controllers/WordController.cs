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
    public class WordController : BaseController
    {

        public ActionResult LastNed(int? Id)
        {

            // Hent informasjon om brukeren sin CV /////////////////////////////////
            CVVersjon BrukerCv = new CVVersjon();

            if (Id.Equals(null))
            {
                BrukerCv = GetBrukerCv(GetAspNetBrukerID());
            }
            else
            {
                BrukerCv = db.CVVersjon.Where(x => x.CVVersjonId == Id).FirstOrDefault();
            }

            string FileName = BrukerCv.Person.Fornavn + " " + BrukerCv.Person.Etternavn + " - CV";
            /////////////////////////////////////////////////////////

            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);


            // TITTEL
            Paragraph Tittel = doc.InsertParagraph();
            Tittel.Append("CURRICULUM VITAE").Font(new FontFamily("Times New Roman")).FontSize(14).Color(Color.Black).Bold();
            Tittel.Append("\n");
            Tittel.Alignment = Alignment.center;
            var a = FontFamily.Families;
            System.Diagnostics.Debug.Write(a);
            // GEOMATIKK addresse
            Paragraph Adresse = doc.InsertParagraph();
            Adresse.Append("GEOMATIKK IKT AS, Otto Nielsens vei 12, 7052 Trondheim").Font(new FontFamily("Times New Roman")).FontSize(12).Color(Color.Black);
            Adresse.Append("\n\n");
            Adresse.Alignment = Alignment.center;

            // NAVN
            Paragraph NavnEtikett = doc.InsertParagraph();
            NavnEtikett.Append("Navn: ").Font(new FontFamily("Times New Roman")).FontSize(11).Color(Color.Black).Bold();
            NavnEtikett.Append(BrukerCv.Person.Fornavn + " " + BrukerCv.Person.Etternavn);
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

            return File(stream.ToArray(), "application/octet-stream", FileName + ".docx");
        }
    }
}