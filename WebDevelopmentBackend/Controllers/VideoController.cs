using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDevelopmentBackend.Models;

namespace WebDevelopmentBackend.Controllers
{
    public class VideoController : Controller
    {
        // Video player
        public ActionResult Play(int ID)
        {
            // Get video record from DB
            VideosManager videosMgr = new VideosManager();
            VideoModel model = videosMgr.GetVideoByID(ID);

            return View(model);
        }

        // Video upload
        public ActionResult Upload()
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult PostVideo(FormCollection form, HttpPostedFileBase videoFile)
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                string Title = form.Get("Title");
                string Description = form.Get("Description");

                string UploaderEmail = Session["Email"].ToString();

                // TODO: Insert Video record in DB


                // TODO: Save file in folder




                TempData["Message"] = "Video uploaded successfully.";
                return RedirectToAction("Upload");

            }

            return RedirectToAction("Login", "Home");
        }


        // My videos
        public ActionResult MyVideos()
        {
            if (Session["IsUserLoggedIn"] != null)
            {
                // TODO: Get current user videos
            }

            return RedirectToAction("Login", "Home");
        }
    }
}