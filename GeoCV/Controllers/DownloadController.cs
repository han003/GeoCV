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

namespace GeoCV.Controllers
{

    [Authorize]
    public class DownloadController : BaseController
    {
        public ActionResult Word()
        {

            // SKRIV WORD KODE HER




            // Returnere bare 'null' midlertidig, men d e her du ska returner docx fila
            return null;

            // Sån her ser d ut når æ returnere en pdf fil, men vet ikke om d e d samme som word fila
            // return File(Bytes, "application/pdf", FileName + ".pdf");
        }

        private Font NormalFont(int Tekststørrelse)
        {
            return FontFactory.GetFont("Verdana", Tekststørrelse);
        }

        private Font FetFont(int Tekststørrelse)
        {
            return FontFactory.GetFont("Verdana", Tekststørrelse, Font.BOLD);
        }

        public ActionResult Pdf()
        {
            CVVersjon Cv = GetUserCV();

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
            UserCv.Open();

            // TITTEL
            Paragraph Top = new Paragraph("CURRICULUM VITAE", FetFont(14));
            Top.Alignment = Element.ALIGN_CENTER;
            UserCv.Add(Top);
            UserCv.Add(Chunk.NEWLINE);

            // GEOMATIKK ADRESSE
            Paragraph Address = new Paragraph("GEOMATIKK IKT, Otto Nielsens vei 12, 7052 Trondheim");
            Address.Alignment = Element.ALIGN_CENTER;
            UserCv.Add(Address);
            UserCv.Add(Chunk.NEWLINE);

            // NAVN
            Paragraph NavnEtikett = new Paragraph("Navn", FetFont(11));
            Paragraph AnsattNavn = new Paragraph(Cv.Person.Fornavn + " " + Cv.Person.Mellomnavn + " " + Cv.Person.Etternavn, NormalFont(11));
            UserCv.Add(LeggTilTabell(NavnEtikett, AnsattNavn, 144));

            // STILLING
            UserCv = Stilling(UserCv, 144);

            // ÅR ERFARING
            Paragraph ÅrErfaringEtikett = new Paragraph("Antall år relevant erfaring", FetFont(11));
            Paragraph AnsattÅrErfaring = new Paragraph(Cv.Person.ÅrErfaring.ToString() + " år", NormalFont(11));
            UserCv.Add(LeggTilTabell(ÅrErfaringEtikett, AnsattÅrErfaring, 144));

            // SPRÅK
            UserCv.Add(LeggTilNøkkelkompetanse("Språk", 144));
            UserCv.Add(Chunk.NEWLINE);

            // NØKKELKOMPETANSE
            UserCv = Nøkkelkompetanse(UserCv);

            // UTDANNELSE
            UserCv = Utdannelse(UserCv);

            // ARBEIDSERFARING
            UserCv = Arbeidserfaring(UserCv);

            UserCv.Close();
            return UserCv;
        }

        private Document Nøkkelkompetanse(Document UserCv)
        {
            // Nøkkelkompetanse header
            Paragraph Header = new Paragraph("Nøkkelkompetanse", FetFont(11));
            UserCv.Add(LeggTilTabell(Header, null, 100));

            // Programmeringsspråk
            UserCv.Add(LeggTilNøkkelkompetanse("Programmeringsspråk", 144));

            // Rammeverk
            UserCv.Add(LeggTilNøkkelkompetanse("Rammeverk", 144));

            // WebTeknologier
            UserCv.Add(LeggTilNøkkelkompetanse("WebTeknologier", 144));

            // Databasesystemer
            UserCv.Add(LeggTilNøkkelkompetanse("Databasesystemer", 144));

            // Serverside
            UserCv.Add(LeggTilNøkkelkompetanse("Serverside", 144));

            // Operativsystemer
            UserCv.Add(LeggTilNøkkelkompetanse("Operativsystemer", 144));

            UserCv.Add(Chunk.NEWLINE);
            return UserCv;
        }

        private PdfPTable LeggTilNøkkelkompetanse(string Ekspertise, float Innrykk)
        {
            // Hent katalogen
            var KatalogElementer = from a in db.ListeKatalog
                                   select a;

            // Placeholders
            string Label = Ekspertise + ": ";
            string Innhold = "";

            // Hent ansatt info
            var Ansatt = from a in db.CVVersjon
                         select a;

            string AnsattEkspertise = "";

            switch (Ekspertise)
            {
                case "Programmeringsspråk":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Kompetanse.Programmeringsspråk;
                    break;

                case "Rammeverk":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Kompetanse.Rammeverk;
                    break;

                case "WebTeknologier":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Kompetanse.WebTeknologier;
                    break;

                case "Databasesystemer":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Kompetanse.Databasesystemer;
                    break;

                case "Serverside":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Kompetanse.Serverside;
                    break;

                case "Operativsystemer":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Kompetanse.Operativsystemer;
                    break;

                case "Språk":
                    AnsattEkspertise = Ansatt.FirstOrDefault().Person.Språk;
                    break;
            }

            // Legg IDer i en liste
            string[] IDSplit = AnsattEkspertise.Split(';');

            foreach (var ID in IDSplit)
            {
                foreach (var Item in KatalogElementer)
                {
                    if (Int32.Parse(ID).Equals(Item.ListeKatalogId))
                    {
                        Innhold += ", " + Item.Element;
                    }
                }
            }

            // Fjern overfløding i starten
            Innhold = Innhold.Substring(2);

            Paragraph EkspertiseParagraf = (Ekspertise.Equals("Språk")) ? new Paragraph(Ekspertise, FetFont(11)) : new Paragraph(Ekspertise, NormalFont(11));
            Paragraph InnholdsParagraf = new Paragraph(Innhold, NormalFont(11));
            return LeggTilTabell(EkspertiseParagraf, InnholdsParagraf, Innrykk);
        }

        private Document Utdannelse(Document UserCv)
        {
            // Utdannelse header
            Paragraph Header = new Paragraph("Utdannelse", FetFont(11));
            UserCv.Add(LeggTilTabell(Header, null, 100));

            var AnsattUtdannelse = from a in db.Utdannelse
                                   orderby a.Fra descending
                                   select a;

            foreach (var Item in AnsattUtdannelse)
            {
                string Etikett = Item.Fra + " - " + Item.Til;
                string Innhold = Item.Beskrivelse + ". " + Item.Studiested;

                Paragraph EtikettParagraf = new Paragraph(Etikett, FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph(Innhold, NormalFont(11));
                UserCv.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, 100));
            }

            UserCv.Add(Chunk.NEWLINE);
            return UserCv;
        }

        private Document Arbeidserfaring(Document UserCv)
        {
            // Arbeidserfaring header
            Paragraph Header = new Paragraph("Arbeidserfaring", FetFont(11));
            UserCv.Add(LeggTilTabell(Header, null, 100));

            var AnsattArbeidserfaring = from a in db.Arbeidserfaring
                                   orderby a.Fra descending
                                   select a;

            foreach (var Item in AnsattArbeidserfaring)
            {
                string Etikett;
                if (Item.Nåværende)
                {
                    Etikett = Item.Fra + " - " + Int16.Parse(DateTime.Now.Year.ToString());
                }
                else
                {
                    Etikett = Item.Fra + " - " + Item.Til;
                }

                string Innhold = "Rolle: " + Item.Stilling + "\n" + Item.Beskrivelse;

                Paragraph EtikettParagraf = new Paragraph(Etikett, FetFont(11));

                // Paragrafer som er stylet
                Paragraph ArbeidsplassParagraf = new Paragraph(Item.Arbeidsplass + ". ", FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph(Innhold, NormalFont(11));

                // Paragraf for å holde de to andre paragrafene som har forskjellig stil
                Paragraph TotalInnholdParagraf = new Paragraph();

                // Legg til
                TotalInnholdParagraf.Add(ArbeidsplassParagraf);
                TotalInnholdParagraf.Add(InnholdsParagraf);

                UserCv.Add(LeggTilTabell(EtikettParagraf, TotalInnholdParagraf, 100));
            }

            UserCv.Add(Chunk.NEWLINE);
            return UserCv;
        }

        private Document Stilling(Document UserCv, float Innrykk)
        {
            // Hent katalogen
            var KatalogElementer = from a in db.ListeKatalog
                                   select a;

            // Hent ansatt info
            var Ansatt = from a in db.Person
                         select a.Stilling;

            foreach (var Item in KatalogElementer)
            {
                if (Int32.Parse(Ansatt.FirstOrDefault().ToString()).Equals(Item.ListeKatalogId))
                {
                    Paragraph EtikettParagraf = new Paragraph("Stilling", FetFont(11));
                    Paragraph InnholdsParagraf = new Paragraph(Item.Element, NormalFont(11));
                    UserCv.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));
                }
            }

            return UserCv;
        }

        private PdfPTable LeggTilTabell(Paragraph Etikett, Paragraph Innhold, float Innrykk)
        {
            PdfPTable table = new PdfPTable(2);
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] CellWidth = new float[] { Innrykk + 23, 500 - Innrykk };
            table.SetTotalWidth(CellWidth);
            table.LockedWidth = true;
            table.AddCell(Etikett);
            table.AddCell(Innhold);

            return table;
        }
    }
}