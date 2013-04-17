using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using UFC;
using UFC.Models;
using Helpers;
using UFC.Helpers;

namespace Rastreador.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                MembershipUserCollection Usuarios = Membership.FindUsersByEmail(model.Email);
                if (Usuarios.Count == 0) { 
                    ModelState["Email"].Errors.Add("* Este email não está cadastrado no sistema.");
                    return View(model);
                }
                else{
                    foreach (MembershipUser item in Usuarios)
                    {
                        string novasenha = item.ResetPassword();
                        ModelosEmail.EsqueciSenha(model.Email, novasenha);
                        return View("ForgotPasswordSuccess");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            TempData["message"] = "Você precisa estar logado para acessar esta página.";
            return RedirectToAction("Index","Home");
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    //ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    MembershipUser oUser = Membership.GetUser(model.UserName);
                    if (oUser != null && oUser.IsApproved == false)
                    {
                        TempData["message"] = "Este usuário ainda não foi ativado. verifique seu email e confirme sua conta.";
                    }
                    else
                    {
                        TempData["message"] = "Usuário ou senha incorreto.";
                    }
                    return RedirectToAction("Index", "Home", new { login = model.UserName });
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ContaCriada()
        {
            return View();
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult VerificaUsuario(Guid token)
        {
            MembershipUser userInfo = Membership.GetUser(token);
            userInfo.IsApproved = true;
            Membership.UpdateUser(userInfo);
            TempData["message"] = "Conta validada com sucesso. Agora você já pode fazer login no site.";
            return RedirectToAction("Index","Home");
        }

        public ActionResult RegisterSuccess(string email,Guid token)
        {
            string url = Request.Url.ToString().Replace("email=" + email + "&", "").Replace("RegisterSuccess", "VerificaUsuario");
            ModelosEmail.ConfirmaCadastro(email, url);
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            model.UserName = model.Email;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, false, null, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    MembershipUser currentUser = Membership.GetUser(model.UserName);
                    Usuario Usuario = new Usuario();
                    Usuario.Id = (Guid)currentUser.ProviderUserKey;
                    Usuario.Login = currentUser.UserName;
                    Usuario.Nome = model.Nome;
                    Usuario.DataCadastro = DateTime.Now;
                    IUsuarioRepository usuarioRepository = new UsuarioRepository();
                    usuarioRepository.InsertOrUpdate(Usuario);
                    usuarioRepository.Save();
                    Roles.AddUserToRole(model.UserName, "usuario");
                    //FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("RegisterSuccess", "Account", new { email = model.Email,token = (Guid)currentUser.ProviderUserKey });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Este usuário já está cadastrado. Escolha um nome de usuário diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Já existe um usuário cadastrado com esse email. Entre com um email diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
