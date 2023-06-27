namespace dot48.UI
{
    public static class RolesApplication
    {
        public const string ADMIN = "ADMIN";
        public const string COORDENADOR = "COORDENADOR";
        public const string OPERADOR = "OPERADOR";
        public const string ADMIN_OR_COORDENADOR = ADMIN + "," + COORDENADOR;
    }
}