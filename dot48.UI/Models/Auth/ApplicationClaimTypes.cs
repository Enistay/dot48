namespace dot48.Models.Auth
{
    public static class ApplicationClaimTypes
    {
        public static string IdUsuario
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/idUsuario"; }
        }

        public static string CodigoOperador
        {
            get { return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/codigoOperador"; }
        }
    }
}
