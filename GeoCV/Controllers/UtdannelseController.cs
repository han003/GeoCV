using GeoCV.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GeoCV.Controllers
{

    [Authorize]
    public class UtdannelseController : BaseController
    {
        public ActionResult Index()
        {
            return View(GetBrukerCv(GetAspNetBrukerID()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeggTilUtdannelse(UtdannelseModel Model)
        {
            if (ModelState.IsValid)
            {

                CVVersjon Cv = GetBrukerCv(GetAspNetBrukerID());

                if (Model.Fra > Model.Til)
                {
                    int NyFra = Model.Til;
                    Model.Til = Model.Fra;
                    Model.Fra = NyFra;
                }

                Utdannelse NewItem = new Utdannelse();
                NewItem.Studiested = Model.Studiested;
                NewItem.Beskrivelse = Model.Beskrivelse;
                NewItem.Fra = Int16.Parse(Model.Fra.ToString());
                NewItem.Til = Int16.Parse(Model.Til.ToString());

                Cv.Utdannelse.Add(NewItem);

                db.SaveChanges();

            }

            return RedirectToAction("Index", "Utdannelse");
        }

        [HttpPost]
        public ActionResult SlettUtdannelse(SlettArbeidserfaringModel Model)
        {
            if (ModelState.IsValid)
            {
                db.Utdannelse.Remove(GetBrukerCv(GetAspNetBrukerID()).Utdannelse.Where(x => x.UtdannelseId.Equals(Model.Id)).FirstOrDefault());
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Utdannelse");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedigerUtdannelse(UtdannelseModel Model)
        {
            if (ModelState.IsValid)
            {
                var Utdannelse = GetBrukerCv(GetAspNetBrukerID()).Utdannelse.Where(x => x.UtdannelseId.Equals(Model.Id)).FirstOrDefault();

                Utdannelse.Studiested = Model.Studiested;
                Utdannelse.Beskrivelse = Model.Beskrivelse;
                Utdannelse.Fra = Int16.Parse(Model.Fra.ToString());
                Utdannelse.Til = Int16.Parse(Model.Til.ToString());

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Utdannelse");
        }
    }
}


