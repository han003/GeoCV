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
    public class PdfController : BaseController
    {

        private Font NormalFont(int Tekststørrelse)
        {
            return FontFactory.GetFont("Verdana", Tekststørrelse);
        }

        private Font FetFont(int Tekststørrelse)
        {
            return FontFactory.GetFont("Verdana", Tekststørrelse, Font.BOLD);
        }

        public ActionResult LastNed(int? Id)
        {
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
            int StartDato = (BrukerCv.Person.StartDato.Equals(null)) ? DateTime.Now.Year : BrukerCv.Person.StartDato.Value.Year;
            int ÅrErfaring = Int32.Parse(BrukerCv.Person.ÅrErfaring.ToString()) + DateTime.Now.Year - StartDato;

            Paragraph ÅrErfaringEtikett = new Paragraph("Antall år relevant erfaring", FetFont(11));
            Paragraph AnsattÅrErfaring = new Paragraph(ÅrErfaring + " år", NormalFont(11));
            CvPDF.Add(LeggTilTabell(ÅrErfaringEtikett, AnsattÅrErfaring, 144));

            // SPRÅK
            try
            {
                CvPDF.Add(LeggTilNøkkelkompetanse("Språk", 144, BrukerCv));
                CvPDF.Add(Chunk.NEWLINE);
            }
            catch (Exception)
            {
            }

            // NØKKELKOMPETANSE
            CvPDF = Nøkkelkompetanse(CvPDF, BrukerCv);

            // UTDANNELSE
            CvPDF = Utdannelse(CvPDF, BrukerCv);

            // ARBEIDSERFARING
            CvPDF = Arbeidserfaring(CvPDF, BrukerCv);

            // PROSJEKTER
            CvPDF = Prosjekter(CvPDF, BrukerCv);

            CvPDF.Close();
            return CvPDF;
        }

        private Document Nøkkelkompetanse(Document CvPDF, CVVersjon BrukerCv)
        {
            // Nøkkelkompetanse header
            Paragraph Header = new Paragraph("Nøkkelkompetanse", FetFont(11));
            CvPDF.Add(LeggTilTabell(Header, null, 100));

            List<string> KompetanseListe = new List<string>();
            KompetanseListe.Add("Programmeringsspråk");
            KompetanseListe.Add("Rammeverk");
            KompetanseListe.Add("Webteknologier");
            KompetanseListe.Add("Databasesystemer");
            KompetanseListe.Add("Serverside");
            KompetanseListe.Add("Operativsystemer");
            KompetanseListe.Add("Annet");

            foreach (var Kompetanse in KompetanseListe)
            {
                try
                {
                    CvPDF.Add(LeggTilNøkkelkompetanse(Kompetanse, 144, BrukerCv));
                }
                catch (Exception)
                {
                }
            }

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

                case "Webteknologier":
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

                case "Annet":
                    AnsattEkspertise = Ansatt.Kompetanse.Annet;
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
            catch (Exception)
            {
                return null;
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

            foreach (var Item in AnsattUtdannelse.OrderByDescending(x => x.Fra).ToList())
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

            var AnsattArbeidserfaring = BrukerCv.Arbeidserfaring.OrderByDescending(x => x.Fra).ToList();

            // Sjekk om nåværende eksisterer først
            foreach (var Item in AnsattArbeidserfaring.Where(x => x.Nåværende.Equals(true)))
            {
                string Etikett = Item.Fra + " - ";
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

            // Legg til alt annet under nåværende
            foreach (var Item in AnsattArbeidserfaring.Where(x => x.Nåværende.Equals(false)))
            {
                string Etikett = Item.Fra + " - " + Item.Til;
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

        private Document Prosjekter(Document CvPDF, CVVersjon BrukerCv)
        {
            // Arbeidserfaring header
            Paragraph Header = new Paragraph("Utvalgte prosjekter", FetFont(11));
            CvPDF.Add(LeggTilTabell(Header, null, 100));

            var MedlemIProsjekt = db.Medlem.Where(x => x.Person.PersonId.Equals(BrukerCv.Person.PersonId));

            // Legg til alle prosjekter
            foreach (var MedlemData in MedlemIProsjekt.OrderByDescending(x => x.Start))
            {
                string MedlemRolle = "";

                foreach (var Element in db.ListeKatalog)
                {
                    if (Element.ListeKatalogId.Equals(MedlemData.Rolle))
                    {
                        MedlemRolle = Element.Element;
                    }
                }

                string Etikett = MedlemData.Start.Year + " - " + MedlemData.Slutt.Year;
                string Innhold = MedlemData.Prosjekt.Kunde + ": " + MedlemData.Prosjekt.Navn + ". Rolle: " + MedlemRolle + ".";

                Paragraph EtikettParagraf = new Paragraph(Etikett, FetFont(11));

                // Paragrafer som er stylet
                Paragraph KundeParagraf = new Paragraph(MedlemData.Prosjekt.Kunde + ". ", FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph(Innhold, NormalFont(11));

                // Paragraf for å holde de to andre paragrafene som har forskjellig stil
                Paragraph TotalInnholdParagraf = new Paragraph();

                // Legg til tabell
                TotalInnholdParagraf.Add(KundeParagraf);
                TotalInnholdParagraf.Add(InnholdsParagraf);

                CvPDF.Add(LeggTilTabell(EtikettParagraf, TotalInnholdParagraf, 100));

                // Legg til liste med tekniske profiler
                List TekniskProfilListe = new List(List.UNORDERED, 10f);
                TekniskProfilListe.SetListSymbol("\u2022");
                TekniskProfilListe.IndentationLeft = 150f;

                // 29
                int? MedlemTekniskProfil = MedlemData.TekniskProfil;

                // "7,19,44,34"
                string TekniskProfilElementer = db.TekniskProfil.Where(x => x.TekniskProfilId == MedlemTekniskProfil).FirstOrDefault().Elementer;

                List<string> ElementListeString = TekniskProfilElementer.Split(';').ToList();
                List<int> ElementListeInt = new List<int>();

                foreach (var Element in ElementListeString)
                {
                    ElementListeInt.Add(int.Parse(Element));
                }

                var ListeKatalogData = from a in db.ListeKatalog
                                       where ElementListeInt.Contains(a.ListeKatalogId)
                                       select a;

                // Legg til i en liste
                string[] KatalogArray = new string[7] { "Programmeringsspråk", "WebTeknologier", "Rammeverk", "Serverside", "Databasesystemer", "Operativsystemer", "Annet" };

                foreach (var Katalog in KatalogArray)
                {
                    string KatalogString = "";
                    foreach (var Element in ListeKatalogData.Where(x => x.Katalog.Equals(Katalog)))
                    {
                        KatalogString += ", " + Element.Element;
                    }
                    // Fjern overfløding i starten
                    try
                    {
                        KatalogString = KatalogString.Substring(2);
                    }
                    catch (Exception)
                    {

                    }
                    TekniskProfilListe.Add(KatalogString);
                }

                CvPDF.Add(TekniskProfilListe);
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
            var Stilling = BrukerCv.Person.Stilling;

            if (Stilling == null)
            {
                Paragraph EtikettParagraf = new Paragraph("Stilling", FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph("", NormalFont(11));
                CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));

                return CvPDF;
            }

            foreach (var Item in KatalogElementer)
            {
                if (Stilling.Equals(Item.ListeKatalogId))
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
            var Nasjonalitet = BrukerCv.Person.Nasjonalitet;

            if (Nasjonalitet == null)
            {
                Paragraph EtikettParagraf = new Paragraph("Nasjonalitet", FetFont(11));
                Paragraph InnholdsParagraf = new Paragraph("", NormalFont(11));
                CvPDF.Add(LeggTilTabell(EtikettParagraf, InnholdsParagraf, Innrykk));

                return CvPDF;
            }

            foreach (var Item in KatalogElementer)
            {
                if (Nasjonalitet.Equals(Item.ListeKatalogId))
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