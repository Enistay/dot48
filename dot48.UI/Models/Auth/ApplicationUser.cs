namespace dot48.Models.Auth
{
    using dot48.Application.Extensions;
    using System.Security.Claims;
    public class ApplicationUser : ClaimsPrincipal
    {
        public ApplicationUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public int IdUsuario
        {
            get
            {
                return this.ExtractClaim<int>(ApplicationClaimTypes.IdUsuario);
            }
        }

        public string CodigoOperador
        {
            get
            {
                return this.ExtractClaim<string>(ApplicationClaimTypes.CodigoOperador);
            }
        }
        public string Nome
        {
            get
            {
                return this.ExtractClaim<string>(ClaimTypes.Name);
            }
        }

        public string Email
        {
            get
            {
                return this.ExtractClaim<string>(ClaimTypes.Email);
            }
        }
        private TValue ExtractClaim<TValue>(string claimtype)
        {
            var claimValue = string.Empty;
            var claim = this.FindFirst(claimtype);

            if (claim != null)
            {
                claimValue = claim.Value;
            }

            return claimValue.ConverterValor<TValue>();
        }
    }
}