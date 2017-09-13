using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using myfhaemailadmin.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace myfhaemailadmin.Controllers
{
    public class HomeController : Controller
    {
        public myfhabackendEntities db = new myfhabackendEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "Email Templates";

            return View(db.template_client.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public static List<SelectListItem> Getclients()
        {
        myfhabackendEntities db = new myfhabackendEntities();
        List<SelectListItem> ls = new List<SelectListItem>();
            var names = from c in db.clients where c.currentflag==true select c;
            foreach (var name in names)
                {
                ls.Add(new SelectListItem() { Text = name.name, Value = name.id.ToString() });
                }
            return ls;
        }

        public static List<SelectListItem> Getcredit()
        {
            myfhabackendEntities db = new myfhabackendEntities();
            List<SelectListItem> types = new List<SelectListItem>();
  
            types.Add(new SelectListItem() { Text = "Excellent", Value = "Excellent" });
            types.Add(new SelectListItem() { Text = "Good", Value = "Good" });
            types.Add(new SelectListItem() { Text = "Some Credit Problems", Value = "Some Credit Problems" });
            types.Add(new SelectListItem() { Text = "Many Credit Problems", Value = "Many Credit Problems" });
            types.Add(new SelectListItem() { Text = "Special Circumstances", Value = "Special Circumstances" });

        return types;
        }

        public ActionResult Reports()
        {
            ViewBag.Message = "Email Reports";

            return View(db.report_view.ToList());
        }

        public ActionResult Reports2()
        {
            ViewBag.Message = "Client Reports";

            return View(db.lead_counts.ToList());
        }

        public ActionResult Edit(int id)
        {
            var gettemplate = from n in db.template_client where n.emailid == id select n;
            return View(gettemplate.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(email_templates utp)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "You must complete all fields.";
                return Edit(utp.emailid);
            }

            if (string.IsNullOrEmpty(utp.toemail))
            {
                if (!utp.enabled.Equals(true))
                { utp.enabled.Equals(false); }
                var gettemplate = db.email_templates.Find(utp.emailid);
                gettemplate.subject = utp.subject;
                gettemplate.html = utp.html;
                gettemplate.text = Regex.Replace(Regex.Replace(utp.html, "<br>", "\n"), @"<[^>]*>", String.Empty);
                gettemplate.last_modified = DateTime.Now;
                if (!gettemplate.enabled.Equals(utp.enabled))
                {
                    gettemplate.enabled = utp.enabled;
                    gettemplate.enable_modified = DateTime.Now;
                }
                db.Entry(gettemplate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            if (!string.IsNullOrEmpty(utp.toemail))
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(utp.toemail));  // replace with valid value 
                message.From = new MailAddress("webmaster@helicon7.org");  // replace with valid value
                message.Subject = utp.subject;
                message.Body = string.Format(utp.html);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "webmaster@helicon7.org",  // replace with valid value
                        Password = "Anthony1"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.helicon7.org";
                    smtp.Port = 25;
                    await smtp.SendMailAsync(message);
                }
            }

            return Edit(utp.emailid);
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Add Template";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(email_templates utp)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            utp.created_on = DateTime.Now;
            utp.enabled = false;
            db.email_templates.Add(utp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            email_templates etr = db.email_templates.Find(id);
            client cclient = db.clients.Find(etr.client_id);
            string client = cclient.name;
            if (etr.emailid == 0)
            {
                return HttpNotFound();
            }
            etr.client.name = client;
            return View(etr);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            email_templates etr = db.email_templates.Find(id);
            var cnt = from n in db.sentlogs where n.emailid == id select n;
            sentlog emlid = cnt.FirstOrDefault();

            if (cnt.Any())
                {
                ViewBag.ErrorMessage = "You cannot delete a template once it has been sent out, if you do not wish to use, you can disable it in the Edit screens.";
                return View(etr);
                }

            db.email_templates.Remove(etr);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SendTestEmail(email_templates utp, string toemail)
        {
            if (toemail != null)
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(toemail));  // replace with valid value 
                message.From = new MailAddress("webmaster@helicon7.org");  // replace with valid value
                message.Subject = utp.subject;
                message.Body = string.Format(utp.html);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "webmaster@helicon7.org",  // replace with valid value
                        Password = "Anthony1"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.helicon7.org";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
            }

            return new EmptyResult();
        }

        public ActionResult DrawChart()
        {
            DateTime today = DateTime.Now;
            DateTime lastweek = today.AddDays(-7);

            var ids = from n in db.sentlogs where n.day >= lastweek select n.num;
            var sums = from n in db.sentlogs where n.day >= lastweek select n.day;

            var chart = new Chart(width: 900, height: 300)
                .AddTitle("Template Counts")
                .AddSeries(
                            chartType: "column",
                            xValue: ids,
                            yValues: sums)
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }
    }
}
