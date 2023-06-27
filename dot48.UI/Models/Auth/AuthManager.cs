using dot48.Application.Interfaces;
using dot48.Application.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace dot48.Models.Auth
{
    public sealed class AuthManager
    {
        private static IUserService UserService
        {
            get
            {
                return AuthManager.GetService<IUserService>();
            }
        }

        private static TService GetService<TService>()
        {
            return (TService)DependencyResolver.Current.GetService(typeof(TService));
        }

        private static AuthManager instance;
        private static IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        public static ApplicationUser CurrentApplicationUser
        {
            get
            {
                return new ApplicationUser(AuthenticationManager.User);
            }
        }
        public static AuthManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthManager();
                }

                return instance;
            }
        }
        public UserViewModel CurrentUser
        {
            get
            {
                return AuthManager.CreateCurrentUser();
            }
        }
        private static UserViewModel CreateCurrentUser()
        {
            UserViewModel usuarioAutenticado = null;
            var applicationUser = CurrentApplicationUser;

            if (applicationUser != null)
            {
                usuarioAutenticado = new UserViewModel
                {
                    IdUser = applicationUser.IdUsuario,
                    Name = applicationUser.Nome,
                    Email = applicationUser.Email,
                    CodeUser = applicationUser.CodigoOperador
                };
            }

            return usuarioAutenticado;
        }
        public void IdentitySignIn(UserViewModel user)
        {

            if (user != null)
            {
                var claims = new List<Claim>();
                var IdUsuario = user.IdUser.ToString(CultureInfo.CurrentCulture);

                claims.Add(new Claim(ClaimTypes.NameIdentifier, IdUsuario));
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

                foreach (var p in user.Profiles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, p.CodeProfile));
                }

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };
               
                AuthenticationManager.SignIn(authProperties, identity);
            }
        }
        public bool SignIn(string username, string password)
        {
            bool isUserAdValid = this.ValidateUserAd(username, password);

            UserViewModel usuario = isUserAdValid ? UserService.GetUserByCodeOperator(username) : UserService.GetUserByNameAndPassword(username, password);

            if (usuario != null)
            {
                this.IdentitySignIn(usuario);
            }

            return usuario != null;
        }

        public bool ValidateUserAd(string username, string password)
        {

            ContextType authenticationType = ContextType.Domain;

            PrincipalContext principalContext = new PrincipalContext(authenticationType);
            bool isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
           
            if (isAuthenticated && userPrincipal != null)
            {
                return !userPrincipal.IsAccountLockedOut()
                        && userPrincipal.Enabled.HasValue
                        && userPrincipal.Enabled.Value;
            }

            return false;

        }

        public bool SignInOperador(string codigoOperador)
        {
            var usuario = UserService.GetUserByCodeOperator(codigoOperador);

            if (usuario != null)
            {
                this.IdentitySignIn(usuario);
            }

            return usuario != null;
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }       

    }
}
