using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using DevelopmentStack.Domain;
using DevelopmentStack.Domain.Entities;
using DevelopmentStack.Tasks;
using DevelopmentStack.Web.Infrastructure.Facebook;
using SharpLite.Domain.DataInterfaces;

namespace DevelopmentStack.Web.Controllers
{
    public class AccountController : Controller
    {


        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;

        private readonly IRepository<User> _userRepository;
        private readonly UserCudTasks _userCudTasks;


        //public ILogger Logger { get; set; }

        public AccountController(IRepository<User> userRepository, UserCudTasks userCudTasks)
        {
            _clientId = FacebookSettings.Settings.ClientId;
            _clientSecret = FacebookSettings.Settings.ClientSecret;
            _redirectUri = FacebookSettings.Settings.RedirectUri;

            _userRepository = userRepository;
            _userCudTasks = userCudTasks;



        }

        public ActionResult Login()
        {
            const string url = "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope=email";
            return new RedirectResult(string.Format(url, _clientId, _redirectUri));
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            // if session does not exist, redirect to homepage
            if (string.IsNullOrWhiteSpace((string)Session["FacebookAccessToken"]))
                return RedirectToAction("Index", "Home");
            return Redirect("https://www.facebook.com/logout.php?next=http://localhost:60627/&access_token=" + Session["FacebookAccessToken"]);
        }

        public ActionResult Handshake(string code)
        {
            //error_reason=user_denied&error=access_denied
            if (string.IsNullOrWhiteSpace(code))
                code = Request.QueryString["error_description"];

            string url = "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}&scope=email";

            Session["FacebookAccessToken"] = FacebookRequest(string.Format(url, _clientId, _redirectUri, _clientSecret, code));

            if ((string)Session["FacebookAccessToken"] == "")
            {
                return RedirectToAction("Index", "Home");
            }

            url = "https://graph.facebook.com/me?access_token={0}&scope=email";
            FacebookUser fbUser = FacebookResponse(string.Format(url, Session["FacebookAccessToken"]));
            SaveUser(fbUser);
            return RedirectToAction("About", "Home");
        }


        private string FacebookRequest(string requestUrl)
        {
            string result = "";

            WebRequest request = WebRequest.Create(requestUrl);
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");

                if (stream != null)
                    using (var streamReader = new StreamReader(stream, encode))
                    {
                        result = streamReader.ReadToEnd().Replace("access_token=", "");
                    }
            }

            return result;
        }


        private FacebookUser FacebookResponse(string requestUrl)
        {
            var user = new FacebookUser();
            WebRequest request = WebRequest.Create(requestUrl);

            using (var response = request.GetResponse())
            {
                var stream = response.GetResponseStream();
                var dataContractJsonSerializer = new DataContractJsonSerializer(typeof(FacebookUser));
                if (stream != null) user = dataContractJsonSerializer.ReadObject(stream) as FacebookUser;
                if (user != null) FormsAuthentication.SetAuthCookie(user.email, true);
            }

            return user;
        }


        private void SaveUser(FacebookUser facebookUser)
        {

            var user = new User
            {
                Email = facebookUser.email,
                Password = "password",
                CreateDate = DateTime.Now,
                Avatar = "",
                AccountTypeId = 2,
                OtherID = facebookUser.id.ToString(),

                Name = new Name
                {
                    FirstName = facebookUser.first_name,
                    MiddleName = "",
                    LastName = facebookUser.last_name
                }
            };


            if (TryUpdateModel(user))
            {
                ActionConfirmation<User> confirmation = _userCudTasks.SaveOrUpdate(user);
            }
        }
    }
}
