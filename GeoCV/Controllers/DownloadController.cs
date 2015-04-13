using GeoCV.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CV.Controllers
{

    [Authorize]
    public class DownloadController : Controller
    {
        private cvEntities db = new cvEntities();

        public ActionResult Pdf()
        {

            string UserId = (Session["ShadowUser"] != null) ? Session["ShadowUser"].ToString()  : User.Identity.GetUserId();

            var Item = from a in db.CVVersjon
                       where a.AspNetUserId.Equals(UserId)
                       select a;

            CVVersjon Cv = Item.FirstOrDefault();

            string FileName = Cv.Person.Fornavn + " " + Cv.Person.Etternavn + " - CV";

            var FilePath = Path.Combine(Path.GetTempPath(), "Temp.pdf");

            Document UserCv = new Document();
            PdfWriter.GetInstance(UserCv, new FileStream(FilePath, FileMode.Create));


            CreateCv(UserCv, Cv);

            
            var fs = new FileStream(FilePath, FileMode.Open);
            var Bytes = new byte[fs.Length];
            fs.Read(Bytes, 0, (int)fs.Length);
            fs.Close();

            return File(Bytes, "application/pdf", FileName + ".pdf");
        }
        
        private Document CreateCv(Document UserCv, CVVersjon Cv)
        {
            Font Normal = FontFactory.GetFont("Times-Roman", 12, Font.NORMAL);
            Font Bold = FontFactory.GetFont("Times-Roman", 12, Font.BOLD);
            
            UserCv.Open();

            // Top
            Paragraph Top = new Paragraph("CURRICULUM VITAE", Bold);
            Top.Alignment = Element.ALIGN_CENTER;
            UserCv.Add(Top);
            UserCv.Add(Chunk.NEWLINE);

            // Address
            Paragraph Address = new Paragraph("GEOMATIKK IKT, Otto Nielsens vei 12, 7052 Trondheim", Normal);
            Address.Alignment = Element.ALIGN_CENTER;
            UserCv.Add(Address);
            UserCv.Add(Chunk.NEWLINE);

            // Name
            UserCv.Add(new Phrase("Navn:", Bold));
            for (int i = 0; i < 6; i++) UserCv.Add(Chunk.TABBING);
            UserCv.Add(new Phrase(Cv.Person.Fornavn + " " + Cv.Person.Etternavn, Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Position
            UserCv.Add(new Phrase("Stilling:", Bold));
            for (int i = 0; i < 5; i++) UserCv.Add(Chunk.TABBING);
            UserCv.Add(new Phrase(Cv.Person.Stilling, Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Experience
            UserCv.Add(new Phrase("Antall år relevant erfaring:", Bold));
            for (int i = 0; i < 3; i++) UserCv.Add(Chunk.TABBING);
            UserCv.Add(new Phrase(Cv.Person.ÅrErfaring.ToString(), Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Languages
            UserCv.Add(new Phrase("Språk:", Bold));
            for (int i = 0; i < 6; i++) UserCv.Add(Chunk.TABBING);
            UserCv.Add(new Phrase(Split(Cv.Person.Språk), Normal));
            UserCv.Add(Chunk.NEWLINE);
            UserCv.Add(Chunk.NEWLINE);

            // KeyCompetences
            Paragraph KeyCompetences = new Paragraph("Nøkkelkompetanse:", Bold);
            UserCv.Add(KeyCompetences);
            UserCv.Add(Chunk.NEWLINE);

            // Programming Languages
            UserCv.Add(new Phrase("Programmeringsspråk: ", Normal));
            UserCv.Add(new Phrase(Split(Cv.Kompetanse.Programmeringsspråk), Normal));
            UserCv.Add(Chunk.NEWLINE);


            // Framework
            UserCv.Add(new Phrase("Rammeverk: ", Normal));
            UserCv.Add(new Phrase(Split(Cv.Kompetanse.Rammeverk), Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Web Technologies
            UserCv.Add(new Phrase("Web-Teknologier: ", Normal));
            UserCv.Add(new Phrase(Split(Cv.Kompetanse.WebTeknologier), Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Database Systems
            UserCv.Add(new Phrase("Databasesystemer: ", Normal));
            UserCv.Add(new Phrase(Split(Cv.Kompetanse.Databasesystemer), Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Serverside
            UserCv.Add(new Phrase("Serverside: ", Normal));
            UserCv.Add(new Phrase(Split(Cv.Kompetanse.Serverside), Normal));
            UserCv.Add(Chunk.NEWLINE);

            // Operating Systems
            UserCv.Add(new Phrase("Operativsystemer: ", Normal));
            UserCv.Add(new Phrase(Split(Cv.Kompetanse.Operativsystemer), Normal));
            
            UserCv.Close();
            return UserCv;
             
        }
        
        private string Split(string RandomString)
        {
            try
            {
                string[] StringElements = RandomString.Split(';');
                string Result = "";

                foreach (string Element in StringElements)
                {
                    Result = Result + Element + ", ";
                }

                Result = Result.Remove(Result.Length - 2);

                return Result;
            }
            catch (Exception)
            {
                
            }

            return "";
        }
    }
}