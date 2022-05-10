using And.ETicaret.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.ETicaret.UI.Web.Controllers.Base
{
    public class AndControllerBase:Controller
    {
        /// <summary>
        ///kullanıcı login kontrol 
        ///</summary>

        public bool IsLogin { get;private set; } //private set bunun sadece bu sayfadan değişmesini sağlar 
        //başka yerden veri atılmaması için
        /// <summary>
        /// giriş yapmış kişinin id si
        /// </summary>
        public int LoginUserId { get;private set; }
        /// <summary>
        /// Login User Entity
        /// </summary>
        public User LoginUserEntity { get;private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            //todo:üye giriş işlemleri sonrası değişecek.
            
            if (requestContext.HttpContext.Session["LoginUserId"]!=null)
            {
                //Kullanıcı giriş yapmışsa
                IsLogin = true;
                LoginUserId = (int)requestContext.HttpContext.Session["LoginUserId"];
                LoginUserEntity = (Core.Entity.User)requestContext.HttpContext.Session["LoginUser"];
                
            }
            base.Initialize(requestContext);
        }
    }
}