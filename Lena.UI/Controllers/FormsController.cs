using Lena.Business;
using Lena.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lena.UI.Controllers
{
    [Authorize]
    public class FormsController : Controller
    {
        public ActionResult Index()
        {
            Lena.Business.FormService formService = new Business.FormService();
            var formList = formService.getAll().OrderByDescending(x => x.CreatedAt);
            return View(formList);
        }

        public ActionResult Form(int formId)
        {
            FormService formService = new FormService();
            var form = formService.get(formId);
            return View(form);
        }

        public PartialViewResult _CreateForm()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult CreateForm(FormDTO formDTO)
        {
            UserService userService = new UserService();
            var user = userService.getUserWithUsername(User.Identity.Name);
            formDTO.CreatedAt = DateTime.Now;
            formDTO.CreatedBy = user.Id;

            FormService formService = new FormService();
            var result = formService.add(formDTO);
            return Json(result);
        }


    }
}