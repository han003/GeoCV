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
using System.Diagnostics;

namespace GeoCV.Controllers
{

    [Authorize]
    public class DownloadController : BaseController
    {
        

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
            CVVersjon BrukerCv = GetUserCV();

            string FileName = BrukerCv.Person.Fornavn + " " + BrukerCv.Person.Etternavn + " - CV";

            var FilePath = Path.Combine(Path.GetTempPath(), "Temp.pdf");

            Document CvPDF = new Document();
            PdfWriter.GetInstance(CvPDF, new FileStream(FilePath, FileMode.Create));


            CreateCv(CvPDF, BrukerCv);

            var fs = new FileStream(FilePath, FileMode.Open);
            var Bytes = new byte[fs.Length];
            fs.Read(Bytes, 0, (int)fs.Length);
            fs.Close();

            return File(Bytes, "application/pdf", FileName + ".pdf");
        }
        
        private Document CreateCv(Document CvPDF, CVVersjon BrukerCv)
        {
            CvPDF.Open();

            // TITTEL
            Paragraph Top = new Paragraph("CURRICULUM VITAE", FetFont(14));
            Top.Alignment = Element.ALIGN_CENTER;
            CvPDF.Add(Top);
            CvPDF.Add(Chunk.NEWLINE);

            // GEOMATIKK ADRESSE
            Paragraph Address = new Paragraph("GEOMATIKK IKT, Otto Nielsens vei 12, 7052 Trondheim");
            Address.Alignment = Element.ALIGN_CENTER;
            CvPDF.Add(Address);
            CvPDF.Add(Chunk.NEWLINE);

            // NAVN
            Paragraph NavnEtikett = new Paragraph("Navn", FetFont(11));
            Paragraph AnsattNavn = new Paragraph(BrukerCv.Person.Fornavn + " " + BrukerCv.Person.Mellomnavn + " " + BrukerCv.Person.Etternavn, NormalFont(11));
            CvPDF.Add(LeggTilTabell(NavnEtikett, AnsattNavn, 144));

            // STILLING
            CvPDF = Stilling(CvPDF, 144, BrukerCv);

            // Nasjonalitet
            CvPDF = Nasjonalitet(CvPDF, 144, BrukerCv);

            // ÅR ERFARING
            Paragraph ÅrErfaringEtikett = new Paragraph("Antall år relevant erfaring", FetFont(11));
            Paragraph AnsattÅrErfaring = new Paragraph(BrukerCv.Person.ÅrErfaring.ToString() + " år", NormalFont(11));
            CvPDF.Add(LeggTilTabell(ÅrErfaringEtikett, AnsattÅrErfaring, 144));

            // SPRÅK
            CvPDF.Add(LeggTilNøkkelkompetanse("Språk", 144, BrukerCv));
            CvPDF.Add(Chunk.NEWLINE);

            // NØKKELKOMPETANSE
            CvPDF = Nøkkelkompetanse(CvPDF, BrukerCv);

            // UTDANNELSE
            CvPDF = Utdannelse(CvPDF, BrukerCv);

            // ARBEIDSERFARING
            CvPDF = Arbeidserfaring(CvPDF, BrukerCv);

            CvPDF.Close();
            return CvPDF;
        }

        private Document Nøkkelkompetanse(Document CvPDF, CVVersjon BrukerCv)
        {
            // Nøkkelkompetanse header
            Paragraph Header = new Paragraph("Nøkkelkompetanse", FetFont(11));
            CvPDF.Add(LeggTilTabell(Header, null, 100));

            // Programmeringsspråk
            CvPDF.Add(LeggTilNøkkelkompetanse("Programmeringsspråk", 144, BrukerCv));

            // Rammeverk
            CvPDF.Add(LeggTilNøkkelkompetanse("Rammeverk", 144, BrukerCv));

            // WebTeknologier
            CvPDF.Add(LeggTilNøkkelkompetanse("WebTeknologier", 144, BrukerCv));

            // Databasesystemer
            CvPDF.Add(LeggTilNøkkelkompetanse("Databasesystemer", 144, BrukerCv));

            // Serverside
            CvPDF.Add(LeggTilNøkkelkompetanse("Serverside", 144, BrukerCv));

            // Operativsystemer
            CvPDF.Add(LeggTilNøkkelkompetanse("Operativsystemer", 144, BrukerCv));

            CvPDF.Add(Chunk.NEWLINE);
            return CvPDF;
        }

        private PdfPTable LeggTilNøkkelkompetanse(string Ekspertise, float Innrykk, CVVersjon BrukerCv)
        {
            // Hent katalogen
            var KatalogElementer = from a in db.ListeKatalog
                                   select a;

            // Placeholders
            string Label = Ekspertise + ": ";
            string Innhold = "";

            // Hent ansatt info
            var Ansatt = BrukerCv;

            string AnsattEkspertise = "";

            switch (Ekspertise)
            {
                case "Programmeringsspråk":
                    AnsattEkspertise = Ansatt.Kompetanse.Programmeringsspråk;
                    break;

                case "Rammeverk":
                    AnsattEkspertise = Ansatt.Kompetanse.Rammeverk;
                    break;

                case "WebTeknologier":
                    AnsattEkspertise = Ansatt.Kompetanse.WebTeknologier;
                    break;

                case "Databasesystemer":
                    AnsattEkspertise = Ansatt.Kompetanse.Databasesystemer;
                    break;

                case "Serverside":
                    AnsattEkspertise = Ansatt.Kompetanse.Serverside;
                    break;

                case "Operativsystemer":
                    AnsattEkspertise = Ansatt.Kompetanse.Operativsystemer;
                    break;

                case "Språk":
                    AnsattEkspertise = Ansatt.Person.Språk;
                    break;
            }

            // Prøv å legg IDer i en liste
            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            Paragraph EkspertiseParagraf = (Ekspertise.Equals("Språk")) ? new Paragraph(Ekspertise, FetFont(11)) : new Paragraph(Ekspertise, NormalFont(11));
            Paragraph InnholdsParagraf = new Paragraph(Innhold, NormalFont(11));
            return LeggTilTabell(EkspertiseParagraf, InnholdsParagraf, Innrykk);
        }

        private Document Utdannelse(Document CvPDF, CVVersjon BrukerCv)
        {
            // Utdannelse header
            Paragraph Header = new Paragraph("Utdannelse", FetFont(11));
            CvPDF.Add(LeggTilTabell(Header, null, 100));

            var AnsattUtdannelse = BrukerCv.Utdannelse;

            foreach (var Item in AnsattUtdannelse)
            {
                string Etikett = Item.Fra + " - " + Item.Til;
                string Innhold = Item.Beskrivelse + ". " + Item.Studiested;

                Paragraph EtikettParagraf = new Paragraph(Etikett, FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph(Innhold, NormalFont(11));
                CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, 100));
            }

            CvPDF.Add(Chunk.NEWLINE);
            return CvPDF;
        }

        private Document Arbeidserfaring(Document CvPDF, CVVersjon BrukerCv)
        {
            // Arbeidserfaring header
            Paragraph Header = new Paragraph("Arbeidserfaring", FetFont(11));
            CvPDF.Add(LeggTilTabell(Header, null, 100));

            var AnsattArbeidserfaring = BrukerCv.Arbeidserfaring;

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

                CvPDF.Add(LeggTilTabell(EtikettParagraf, TotalInnholdParagraf, 100));
            }

            CvPDF.Add(Chunk.NEWLINE);
            return CvPDF;
        }

        private Document Stilling(Document CvPDF, float Innrykk, CVVersjon BrukerCv)
        {
            // Hent katalogen
            var KatalogElementer = from a in db.ListeKatalog
                                   select a;

            // Hent ansatt info
            var Ansatt = BrukerCv.Person.Stilling;

            if (Ansatt == null)
            {
                Paragraph EtikettParagraf = new Paragraph("Stilling", FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph("", NormalFont(11));
                CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));

                return CvPDF;
            }

            foreach (var Item in KatalogElementer)
            {
                if (Int32.Parse(Ansatt.FirstOrDefault().ToString()).Equals(Item.ListeKatalogId))
                {
                    Paragraph EtikettParagraf = new Paragraph("Stilling", FetFont(11));
                    Paragraph InnholdsParagraf = new Paragraph(Item.Element, NormalFont(11));
                    CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));
                }
            }

            return CvPDF;
        }

        private Document Nasjonalitet(Document CvPDF, float Innrykk, CVVersjon BrukerCv)
        {
            // Hent katalogen
            var KatalogElementer = from a in db.ListeKatalog
                                   select a;

            // Hent ansatt info
            var Ansatt = BrukerCv.Person.Nasjonalitet;

            if (Ansatt == null)
            {
                Paragraph EtikettParagraf = new Paragraph("Nasjonalitet", FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph("", NormalFont(11));
                CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));

                return CvPDF;
            }

            foreach (var Item in KatalogElementer)
            {
                if (Int32.Parse(Ansatt.FirstOrDefault().ToString()).Equals(Item.ListeKatalogId))
                {
                    Paragraph EtikettParagraf = new Paragraph("Nasjonalitet", FetFont(11));
                    Paragraph InnholdsParagraf = new Paragraph(Item.Element, NormalFont(11));
                    CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));
                }
            }

            return CvPDF;
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